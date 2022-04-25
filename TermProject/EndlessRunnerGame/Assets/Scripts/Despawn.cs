using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private Transform curr_level;
    private Vector3 endPosition;
    // Update is called once per frame

    private void Awake()
    {
        endPosition = curr_level.Find("EndPosition").position;
        //spawnLevelPart();
    }

    void Update()
    {
        if ((Vector3.Distance(GameObject.Find("Player").transform.position, endPosition) > 30f) && (GameObject.Find("Player").transform.position.x > endPosition.x))
        {
            Destroy(this.gameObject);
        }
    }
}
