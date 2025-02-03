using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    // The maximum distance for the raycast
    public float raycastDistance = 100f;
    public float shotVelocity = 250;
    public float delayBetweenObjects = 0.01f; // Delay in seconds

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ProcessRaycastHits());
        }
    }

    IEnumerator ProcessRaycastHits()
    {
        // Perform RaycastAll
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raycastDistance);

        if (hits.Length > 0)
        {
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            System.Array.Reverse(hits);
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log("Hit object: " + hit.collider.name + " at distance: " + hit.distance);
                if (hit.collider.gameObject.tag == "SmallCube")
                {
                    CubeDestroyer destroyer = GetComponent<CubeDestroyer>();
                    if (destroyer != null)
                    {
                        destroyer.DestroyCube(hit.collider.gameObject);
                    }
                }

                yield return new WaitForSeconds(delayBetweenObjects);
            }
        }

        Debug.Log("Finished processing all hits.");
    }
}