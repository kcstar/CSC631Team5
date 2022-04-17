using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;
    public float camHeight;

    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = target.transform.position + new Vector3(0, 15.08f, -6) - new Vector3(0, target.transform.position.y, 0);
        Vector3 newPos = target.transform.position + offset;
        newPos = newPos + new Vector3(0, camHeight, 0) - new Vector3(0, newPos.y, 0);
        gameObject.transform.position = newPos;
    }
}
