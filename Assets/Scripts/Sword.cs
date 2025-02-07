using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        // Check if the collided object is a cube
        if (collision.gameObject.tag == "SmallCube")
        {
            Debug.Log("Small Cube HIT !!!! on collision");
            CubeManager cubemngr = collision.gameObject.GetComponentInParent<CubeManager>();
            cubemngr.DestroyCube(collision.gameObject);
            
            // GameObject clusterCube = collision.gameObject.GetComponent<SmallCube>().clusterCube;
            // GameObject instantiatedCube = Instantiate(clusterCube, collision.transform.position, collision.transform.rotation);

            // foreach (Transform child in instantiatedCube.transform)
            // {   
            //     child.transform.SetParent(null);
            // }
            // Destroy(instantiatedCube);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        // Check if the triggered object is a cube
        if (other.tag == "SmallCube")
        {
            Debug.Log("Small Cube HIT !!!! on trigger");
            CubeManager cubemngr = other.gameObject.GetComponentInParent<CubeManager>();
            cubemngr.DestroyCube(other.gameObject);
            
            // GameObject clusterCube = other.gameObject.GetComponent<SmallCube>().clusterCube;
            // GameObject instantiatedCube = Instantiate(clusterCube, other.transform.position, other.transform.rotation);

            // foreach (Transform child in instantiatedCube.transform)
            // {   
            //     child.transform.SetParent(null);
            // }
            // Destroy(instantiatedCube);
        }
    }

}