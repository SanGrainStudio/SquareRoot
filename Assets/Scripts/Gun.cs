using UnityEngine;

public class Gun : MonoBehaviour
{


    public GameObject fpsCam ;

    public GameObject projectile;

    public Transform barrelEnd;

    public float projectileVelocity = 500;
    public float range = 100;
    public float damage = 50;

    public bool RaycastOnly = true;

    


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ShootRay();
            if(!RaycastOnly)
            {
                shootProjectile();
            }
            
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
                if(RaycastOnly)
                {
                    target.GetComponent<Rigidbody>().AddForce(transform.forward * projectileVelocity,ForceMode.Impulse);
                }
                
            }

        }
    }

        void shootProjectile()
    {
        GameObject ball = Instantiate(projectile, barrelEnd.transform.position, transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(barrelEnd.transform.forward * projectileVelocity,ForceMode.Impulse);
    }


}
