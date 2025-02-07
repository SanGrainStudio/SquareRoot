using UnityEngine;

public class collision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Log the name of the object that collided with this object
        Debug.Log("Collided with: " + collision.gameObject.name);

        // Log the contact point of the collision
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            Debug.Log("Contact point: " + contact.point);
        }

        // Change the color of the object on collision
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }
}

