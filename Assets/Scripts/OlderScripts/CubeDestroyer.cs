using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{   
    public Camera fpsCamera;
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
                    rb.AddForce(fpsCamera.transform.forward* shotVel,ForceMode.Impulse);
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


}