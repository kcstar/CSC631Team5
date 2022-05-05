using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    public float timeToSpawn;

    private float currentTimeToSpawn;

    public float timer;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = objectToSpawn.GetComponent<Rigidbody>();
        timeToSpawn = Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn = Random.Range(2, 5);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (currentTimeToSpawn > 0)
            {
                currentTimeToSpawn -= Time.deltaTime;
            }
            else
            {
                SpawnObject();
                currentTimeToSpawn = timeToSpawn;
            }
        }
        
    }

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    
}
