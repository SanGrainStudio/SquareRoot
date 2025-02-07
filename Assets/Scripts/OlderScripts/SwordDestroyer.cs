using UnityEngine;

public class SwordDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        // Check if the collided object is a cube
        if (collision.gameObject.tag == "SmallCube")
        {
            GameObject clusterCube = collision.gameObject.GetComponent<SmallCube>().clusterCube;
            GameObject instantiatedCube = Instantiate(clusterCube, collision.transform.position, collision.transform.rotation);

            foreach (Transform child in instantiatedCube.transform)
            {   
                child.transform.SetParent(null);
            }
            Destroy(instantiatedCube);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        // Check if the triggered object is a cube
        if (other.tag == "SmallCube")
        {
            GameObject clusterCube = other.gameObject.GetComponent<SmallCube>().clusterCube;
            GameObject instantiatedCube = Instantiate(clusterCube, other.transform.position, other.transform.rotation);

            foreach (Transform child in instantiatedCube.transform)
            {   
                child.transform.SetParent(null);
            }
            Destroy(instantiatedCube);
        }
    }

}