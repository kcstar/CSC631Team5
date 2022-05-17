using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float zPosition; //-52.64

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl controlScript = other.gameObject.GetComponent<PlayerControl>();

        if (controlScript)
        {
            Debug.Log("TRIGGERED WITH SPAWN");
            controlScript.spawn = gameObject;
        }
    }
}
