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
    public float inputX;
    public float walkSpeed;

    public float jumpPower;
    private float maxJumpTime = 0.25f;   // This is how long the spacebar is considered when applying jump velocity
    private float jumpBegan = 0;
    private float playerInfluence = 0.3f;
    private Boolean grounded = false;
    private Vector3 previousPosition;
    private float stickiness = 10f;

    private PlayerController stickmanAnimator;

    void Start()
    {
        previousPosition = transform.position;
        rigidBodyComponent = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -40.0F, 0);
        stickmanAnimator = gameObject.transform.Find("stickman").GetComponent<PlayerController>();

		//DontDestroyOnLoad(gameObject);
    }
    
    private void FixedUpdate()
    {
        stickmanAnimator.SetSpeed(ApplyStickiness(stickmanAnimator.GetSpeed(), rigidBodyComponent.velocity.x));
        Vector3 deltaPosition = (transform.position - previousPosition) / Time.deltaTime;
        //rigidBodyComponent.velocity = ApplyStickiness(rigidBodyComponent.velocity, new Vector3((1.0f + playerInfluence * inputX) * walkSpeed, rigidBodyComponent.velocity.y, 0));
        rigidBodyComponent.velocity = new Vector3((1.0f + playerInfluence * inputX) * walkSpeed, rigidBodyComponent.velocity.y, 0);

        SphereCollider collider = transform.Find("GroundContact").gameObject.GetComponent<SphereCollider>();
        Vector3 colliderExtents = collider.bounds.extents;
        Vector3 colliderOffset = Vector3.Scale(collider.center, transform.localScale);
        Vector3 spherePosition = collider.transform.position + colliderOffset;
        float sphereRadius = colliderExtents.x + 0.02f;

        /*
        GameObject debugSphere = transform.Find("DebugSphere").gameObject;
        debugSphere.transform.position = spherePosition;
        debugSphere.transform.localScale = new Vector3(sphereRadius * 2, sphereRadius * 2, sphereRadius * 2) * 2;
        */

        grounded = Physics.OverlapSphere(spherePosition, sphereRadius, LayerMask.GetMask("Default")).Length > 0;

        Boolean canJump = grounded || (Time.time - jumpBegan) <= maxJumpTime;

        if (jumping && canJump)
        {
            if (grounded) {
                jumpBegan = Time.time;
            }
            if ((Time.time - jumpBegan) > maxJumpTime) {
                jumpBegan = 0;
            }
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, jumpPower, rigidBodyComponent.velocity.z);
        } else {
            jumpBegan = 0;
        }

        previousPosition = transform.position;
    }

    private Vector3 ApplyStickiness(Vector3 from, Vector3 to) {
        float adjustedStickiness = Mathf.Min(stickiness * Time.deltaTime, 1);
        return Vector3.Lerp(from, to, adjustedStickiness);
    }
    private float ApplyStickiness(float from, float to) {
        float adjustedStickiness = Mathf.Min(stickiness * Time.deltaTime, 1);
        return from * (1 - adjustedStickiness) + to * adjustedStickiness;
    }


    
}
