using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CustomPlane : MonoBehaviour
{
    public int width = 1;  // Width of the plane
    public int height = 1; // Height of the plane

    void Start()
    {
        GeneratePlane();
    }

    void GeneratePlane()
    {
        // Create a new Mesh
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Define vertices
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0), // Bottom-left
            new Vector3(0, 0, height), // Top-left
            new Vector3(width, 0, 0), // Bottom-right
            new Vector3(width, 0, height) // Top-right
        };

        // Define UVs (texture coordinates)
        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0), // Bottom-left
            new Vector2(0, 1), // Top-left
            new Vector2(1, 0), // Bottom-right
            new Vector2(1, 1)  // Top-right
        };

        // Define triangles (indices for vertices)
        int[] triangles = new int[]
        {
            0, 1, 2, // First triangle (bottom-left)
            2, 1, 3  // Second triangle (top-right)
        };

        // Assign data to the mesh
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();
    }
}