using UnityEngine;

public class SwordDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a cube
        SmallCube cubeScript = collision.gameObject.GetComponent<SmallCube>();
        if (cubeScript != null)
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
        SmallCube cubeScript = other.GetComponent<SmallCube>();
        if (cubeScript != null)
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