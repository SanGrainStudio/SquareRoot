
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCubeOld : MonoBehaviour
{
    public void OnCubeDestroyed()
    {
        StartCoroutine(CheckAfterCubeDestroyed());
    }

    private IEnumerator CheckAfterCubeDestroyed()
    {
        yield return null; // Wait a frame to let Unity update child count

        if (transform.childCount == 0)
        {
            Destroy(gameObject); // Destroy the group if no children are left

        }

        // Otherwise, check if the remaining cubes need to split
        TrySplit();
    }

    private void TrySplit()
    {
        List<List<Transform>> groups = FindDisconnectedGroups();

        if (groups.Count > 1)
        {
            SplitGroups(groups);
        }
    }

    private List<List<Transform>> FindDisconnectedGroups()
    {
        List<List<Transform>> groups = new List<List<Transform>>();
        HashSet<Transform> visited = new HashSet<Transform>();

        foreach (Transform cube in transform)
        {
            if (!visited.Contains(cube))
            {
                List<Transform> newGroup = new List<Transform>();
                ExploreConnectedCubes(cube, newGroup, visited);
                groups.Add(newGroup);
            }
        }

        return groups;
    }

    private void ExploreConnectedCubes(Transform startCube, List<Transform> group, HashSet<Transform> visited)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(startCube);
        visited.Add(startCube);

        while (queue.Count > 0)
        {
            Transform cube = queue.Dequeue();
            group.Add(cube);

            foreach (Transform neighbor in GetNeighborCubes(cube))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    private List<Transform> GetNeighborCubes(Transform cube)
    {
        List<Transform> neighbors = new List<Transform>();

        foreach (Transform other in transform)
        {
            if (other != cube && Vector3.Distance(other.position, cube.position) < 1.1f) // Adjust for your cube size
            {
                neighbors.Add(other);
            }
        }

        return neighbors;
    }

    private void SplitGroups(List<List<Transform>> groups)
    {
        foreach (var group in groups)
        {
            GameObject newParent = new GameObject("SubCubeGroup");
            newParent.AddComponent<BigCube>(); // So it can split again

            foreach (var cube in group)
            {
                cube.SetParent(newParent.transform);
            }

            StartCoroutine(DestroyIfEmpty(newParent)); // Clean up empty groups
        }

        StartCoroutine(DestroyIfEmpty(gameObject)); // Clean up this group if empty
    }

    private IEnumerator DestroyIfEmpty(GameObject parent)
    {
        yield return null; // Wait for the next frame

        if (parent.transform.childCount == 0)
        {
            Destroy(parent); // Destroy the empty group
        }
    }
}
