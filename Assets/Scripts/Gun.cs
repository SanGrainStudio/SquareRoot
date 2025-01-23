using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 50;
    public float range = 100;
    public float projectileVelocity = 400;
    public Camera fpsCam ;
    public GameObject projectile;
    private bool isShootingRay = false;
    public bool Skewer = false;
    Ray ray;
    RaycastHit[] hits = new RaycastHit[10];


    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShootRay();
            isShootingRay = true;
        }
    }



    void LateUpdate()
    {if(isShootingRay)
        {
            if (Skewer)
            {
                shootThrough();
            }
            else ShootRay();
            isShootingRay = false;
            shootProjectile();
        }
        
    }
    

    void ShootRay()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

        }
    }


    void shootThrough()
    {
        ray = new Ray(transform.position, transform.forward);
        int numHits = Physics.RaycastNonAlloc(ray, hits);
        if (numHits > 0)
        {
            for (int i =0; i <numHits; i++)
            {
                Debug.Log(hits[i].collider.gameObject.name);
                Target target = hits[i].transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (i == numHits-1)
                {
                    Debug.Log($"Last Hit, with parent {hits[i].transform.parent.name}");
                    hits[i].transform.parent.GetComponent<FixedCubeRaycast>().TouchCheck();
                }

            }
        }
    }


    void shootProjectile()
    {
        GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * projectileVelocity,ForceMode.Impulse);
    }
}
