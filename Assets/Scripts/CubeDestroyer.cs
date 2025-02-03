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
            Instantiate(cubeScript.clusterCube, transform.position, transform.rotation);

            // Notify the parent to check connectivity
            BigCube bigCube = cube.transform.parent.GetComponent<BigCube>();
            if (bigCube != null)
            {
                bigCube.CheckConnectivity();
            }
        }
    }
}