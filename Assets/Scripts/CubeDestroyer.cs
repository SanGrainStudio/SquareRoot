using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{   

    public void DestroyCube(GameObject cube)
    {
        var cubeScript = cube.GetComponent<SmallCube>();
        if (cubeScript != null)
        {
            cubeScript.isDestroyed = true;
            Destroy(cube);
            GameObject instantiatedCube = Instantiate(cubeScript.clusterCube, cube.transform.position, cube.transform.rotation);


            var shotVel = gameObject.GetComponent<Shoot>().shotVelocity;
            foreach (Transform child in instantiatedCube.transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(child.transform.forward * shotVel,ForceMode.Impulse);
                    //StartCoroutine(DelayedMove(0.1f,rb,child,shotVel));
                }
            
            }
            // Notify the parent to check connectivity
            BigCube bigCube = cube.transform.parent.GetComponent<BigCube>();
            if (bigCube != null)
            {
                bigCube.CheckConnectivity();
            }
        }
    }

     private IEnumerator DelayedMove(float delayTime, Rigidbody rb, Transform child, float shotVel)
    {
        // Wait for the delay
        yield return new WaitForSeconds(delayTime);
        rb.AddForce(child.transform.forward * shotVel,ForceMode.Impulse);
        Debug.Log("Object moved after " + delayTime + " seconds!");
    }
}