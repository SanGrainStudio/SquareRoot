using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CubeManager : MonoBehaviour
{
    private List<GameObject> cubes = new List<GameObject>(); // List to store all cubes

    
    void Start()
    {   
        // Initialize the cubes list with all child cubes
        foreach (Transform child in transform)
        {
            cubes.Add(child.gameObject);
        }
    }

    public void DestroyCube(GameObject cubeToDestroy)
    {
        // Remove the cube from the list
        cubes.Remove(cubeToDestroy);

        // Destroy the cube
        Destroy(cubeToDestroy);

        // Check connectivity and separate disconnected cubes
        CheckAndSeparateCubes();
    }

    void CheckAndSeparateCubes()
    {
        // Create a list to store connected cubes
        List<GameObject> connectedCubes = new List<GameObject>();

        // Start DFS from the first cube
        if (cubes.Count > 0)
        {
            DFS(cubes[0], connectedCubes);
        }

        // If not all cubes are connected, separate the disconnected ones
        if (connectedCubes.Count < cubes.Count)
        {
            SeparateDisconnectedCubes(connectedCubes);
        }
    }

    void DFS(GameObject cube, List<GameObject> connectedCubes)
    {
        // Mark the cube as connected
        connectedCubes.Add(cube);

        // Check all neighboring cubes
        foreach (GameObject otherCube in cubes)
        {
            if (!connectedCubes.Contains(otherCube) && AreCubesTouching(cube, otherCube))
            {
                DFS(otherCube, connectedCubes);
            }
        }
    }

    bool AreCubesTouching(GameObject cube1, GameObject cube2)
    {
        // Get the bounds of both cubes
        Bounds bounds1 = cube1.GetComponent<Renderer>().bounds;
        Bounds bounds2 = cube2.GetComponent<Renderer>().bounds;

        // Check if the bounds intersect
        return bounds1.Intersects(bounds2);
    }

    void SeparateDisconnectedCubes(List<GameObject> connectedCubes)
    {
        // Create a new parent for the disconnected cubes
        GameObject newParent = new GameObject("DisconnectedCubes");
        newParent.transform.position = Vector3.zero;

        // Move disconnected cubes to the new parent
        foreach (GameObject cube in cubes.ToArray())
        {
            if (!connectedCubes.Contains(cube))
            {
                cube.transform.parent = newParent.transform;
                cubes.Remove(cube); // Remove from the original list
            }
        }
        newParent.AddComponent<CubeManager>();

    }
}