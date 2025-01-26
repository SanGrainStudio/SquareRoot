using System.Collections.Generic;
using UnityEngine;

public class BigCube : MonoBehaviour
{
    public void CheckConnectivity()
    {
        // Get all child cubes
        List<Transform> allCubes = new List<Transform>();
        foreach (Transform child in transform)
        {
            var cubeScript = child.GetComponent<SmallCube>();
            if (cubeScript != null && !cubeScript.isDestroyed)
            {
                allCubes.Add(child);
            }
        }

        // Check connectivity using BFS
        List<List<Transform>> groups = GetConnectedGroups(allCubes);

        // If there are multiple groups, split them
        if (groups.Count > 1)
        {
            SplitGroups(groups);
        }
    }

    private List<List<Transform>> GetConnectedGroups(List<Transform> cubes)
    {
        List<List<Transform>> groups = new List<List<Transform>>();
        HashSet<Transform> visited = new HashSet<Transform>();

        foreach (var cube in cubes)
        {
            if (!visited.Contains(cube))
            {
                List<Transform> group = new List<Transform>();
                Queue<Transform> queue = new Queue<Transform>();
                queue.Enqueue(cube);
                visited.Add(cube);

                while (queue.Count > 0)
                {
                    Transform current = queue.Dequeue();
                    group.Add(current);

                    foreach (var neighbor in GetNeighbors(current, cubes))
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                groups.Add(group);
            }
        }

        return groups;
    }

private List<Transform> GetNeighbors(Transform cube, List<Transform> cubes)
{
    List<Transform> neighbors = new List<Transform>();
    Bounds cubeBounds = cube.GetComponent<Collider>().bounds;

    foreach (Transform otherCube in cubes)
    {
        if (otherCube == cube) continue;

        Bounds otherBounds = otherCube.GetComponent<Collider>().bounds;

        // Check if their bounds are intersecting
        if (cubeBounds.Intersects(otherBounds))
        {
            neighbors.Add(otherCube);
        }
    }

    return neighbors;
}

    private void SplitGroups(List<List<Transform>> groups)
    {
        foreach (var group in groups)
        {
            GameObject newParent = new GameObject("SubCubeGroup");
            foreach (var cube in group)
            {
                cube.SetParent(newParent.transform);
            }
        }

        // Destroy the original parent if it has no more children
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}