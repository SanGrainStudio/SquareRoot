using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    // The maximum distance for the raycast
    public float raycastDistance = 100f;
    public float shotVelocity = 250;
    public float delayBetweenObjects = 0f; // Delay in seconds

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
                    CubeManager cubemngr = hit.collider.GetComponentInParent<CubeManager>();
                    cubemngr.DestroyCube(hit.collider.gameObject);
                    
                    GameObject clusterCube = hit.collider.GetComponent<SmallCube>().clusterCube;
                    GameObject instantiatedCube = Instantiate(clusterCube, hit.transform.position, hit.transform.rotation);

                    foreach (Transform child in instantiatedCube.transform)
                    {   
                        child.transform.SetParent(null);
                        Rigidbody rb = child.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.AddForce(Camera.main.transform.forward* shotVelocity,ForceMode.Impulse);
                        }
                    
                    }
                    Destroy(instantiatedCube);
                    
                }

                if (hit.collider.gameObject.tag == "MicroCube")
                {
                    hit.collider.GetComponent<Rigidbody>().AddForce(transform.forward* shotVelocity,ForceMode.Impulse);
                }

                yield return new WaitForSeconds(delayBetweenObjects);
            }
        }

        Debug.Log("Finished processing all hits.");
    }
}