using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidBodyComponent;
    // Start is called before the first frame update
    public Vector3 velocity {
        get { return rigidBodyComponent.velocity; }
        set { rigidBodyComponent.velocity = value; }
    }
    public Vector3 position {
        get { return rigidBodyComponent.position; }
        set { rigidBodyComponent.position = value; }
    }
    public bool jumping;
    private Vector3 moveDirection = new Vector3();
    public Vector3 MoveDirection {
        get { return moveDirection; }
        set { value.Scale(new Vector3(1, 0, 1)); moveDirection = value; }
    }
    public float walkSpeed = 0;

    public float jumpPower = 15;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -40.0F, 0);
        
		//DontDestroyOnLoad(gameObject);
    }
    
    private void Update()
    {
        rigidBodyComponent.velocity = (new Vector3(0, rigidBodyComponent.velocity.y, 0)) + moveDirection * walkSpeed;

        SphereCollider collider = transform.Find("GroundContact").gameObject.GetComponent<SphereCollider>();
        Vector3 colliderExtents = collider.bounds.extents;
        Vector3 colliderOffset = Vector3.Scale(collider.center, transform.localScale);
        Vector3 spherePosition = collider.transform.position + colliderOffset;
        float sphereRadius = colliderExtents.x + 0.02f;

        GameObject debugSphere = transform.Find("DebugSphere").gameObject;
        debugSphere.transform.position = spherePosition;
        debugSphere.transform.localScale = new Vector3(sphereRadius * 2, sphereRadius * 2, sphereRadius * 2) * 2;

        if (Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Default")).Length == 0)
        {
            return;
        }
        if (jumping)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, jumpPower, rigidBodyComponent.velocity.z);
        }
    }
}
