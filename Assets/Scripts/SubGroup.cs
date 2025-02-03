using UnityEngine;

public class SubGroup : MonoBehaviour
{
    void Update()
    {
        if( transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
