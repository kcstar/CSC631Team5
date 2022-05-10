using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update
    

    public float delay = 0.1f;
    public float radius = 5f;
    public float force = 1000f;
    

    public GameObject explosionEffect;

    bool isTrigger = false;

    bool hasExploded = false;
    // Update is called once per frame
    void Update()
    {
        if(isTrigger == true)
        {
            Explode();

        }
    }

    void Explode()
    {

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddExplosionForce(force, explosionPos, radius, 3.0F, ForceMode.Force);
            }
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
        AkSoundEngine.PostEvent("Play_SFX_Explosion", this.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "对象005" || other.name == "对象006" || other.name == "对象007")
        {
            
            isTrigger = true;
        }
    }
     private void OnCollisionEnter(Collision collision)
    {
        
        isTrigger = true;
    }


}
