using UnityEngine;

public class SwordDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a cube
        if (collision.gameObject.tag == "SmallCube")
        {
            // Destroy the cube using CubeDestroyer
            CubeDestroyer destroyer = GetComponent<CubeDestroyer>();
            if (destroyer != null)
            {
                destroyer.DestroyCube(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggered object is a cube
        if (other.tag == "SmallCube")
        {
            // Destroy the cube using CubeDestroyer
            CubeDestroyer destroyer = GetComponent<CubeDestroyer>();
            if (destroyer != null)
            {
                destroyer.DestroyCube(other.gameObject);
            }
        }
    }

}