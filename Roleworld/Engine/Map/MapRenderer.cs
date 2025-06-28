using System.Numerics;
using Roleworld.Engine.Map;
using Silk.NET.OpenGL;

namespace Roleworld.Engine.Rendering;

public class MapRenderer
{
    private readonly GL _gl;
    private uint _vao,
        _vbo,
        _ebo;
    private int _indexCount;

    public MapRenderer(GL gl)
    {
        _gl = gl;
    }

    public unsafe void Build(MapData map)
    {
        List<float> vertices = new();
        List<uint> indices = new();

        uint indexOffset = 0;

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Vector3 color = map.BiomeMap[x, y].GetColor();

                // quad 1x1 at position x, y
                vertices.AddRange(
                    new float[]
                    {
                        x,
                        y,
                        color.X,
                        color.Y,
                        color.Z, // Top-left
                        x + 1,
                        y,
                        color.X,
                        color.Y,
                        color.Z, // Top-right
                        x + 1,
                        y + 1,
                        color.X,
                        color.Y,
                        color.Z, // Bottom-right
                        x,
                        y + 1,
                        color.X,
                        color.Y,
                        color.Z, // Bottom-left
                    }
                );

                indices.AddRange(
                    new uint[]
                    {
                        indexOffset,
                        indexOffset + 1,
                        indexOffset + 2,
                        indexOffset,
                        indexOffset + 2,
                        indexOffset + 3,
                    }
                );

                indexOffset += 4;
                Console.WriteLine($"({x},{y}) = {map.BiomeMap[x, y]}");
            }
        }

        _indexCount = indices.Count;

        // generate VAO / VBO / EBO
        _vao = _gl.GenVertexArray();
        _vbo = _gl.GenBuffer();
        _ebo = _gl.GenBuffer();

        _gl.BindVertexArray(_vao);

        fixed (float* v = vertices.ToArray())
        {
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
            _gl.BufferData(
                BufferTargetARB.ArrayBuffer,
                (nuint)(vertices.Count * sizeof(float)),
                v,
                BufferUsageARB.StaticDraw
            );
        }

        fixed (uint* i = indices.ToArray())
        {
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
            _gl.BufferData(
                BufferTargetARB.ElementArrayBuffer,
                (nuint)(indices.Count * sizeof(uint)),
                i,
                BufferUsageARB.StaticDraw
            );
        }

        const int stride = 5 * sizeof(float);

        _gl.EnableVertexAttribArray(0); // position
        _gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, (uint)stride, (void*)0);

        _gl.EnableVertexAttribArray(1); // color
        _gl.VertexAttribPointer(
            1,
            3,
            VertexAttribPointerType.Float,
            false,
            (uint)stride,
            (void*)(2 * sizeof(float))
        );

        _gl.BindVertexArray(0);
    }

    public unsafe void Render()
    {
        _gl.BindVertexArray(_vao);
        _gl.DrawElements(
            PrimitiveType.Triangles,
            (uint)_indexCount,
            DrawElementsType.UnsignedInt,
            null
        );
    }
}
