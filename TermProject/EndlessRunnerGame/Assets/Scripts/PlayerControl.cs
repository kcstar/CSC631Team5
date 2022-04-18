using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControl : MonoBehaviour
{
    private Vector3 spawnPos;
    private float horizontalInput;
    private int superJumpsRemaining;
	private NetworkManager networkManager;
    // Start is called before the first frame update
    private Player humanoid;

    private double lastMoveRequest = 0;
    private double MOVE_REQUEST_FREQUENCY = 0.2;
    private bool movementChanged = false;
    public float respawnHeight;

    void Start()
    {
        spawnPos = gameObject.transform.position;
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        humanoid = gameObject.GetComponent<Player>();
		DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < respawnHeight) {
            humanoid.position = spawnPos;
            humanoid.velocity = new Vector3();
        }

        float walkSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 8.0f : 4.0f;
        float jumpPower = (Input.GetKey(KeyCode.LeftShift)) ? 18 : 13;
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        bool jumping = Input.GetKey(KeyCode.Space);

        if (walkSpeed != humanoid.walkSpeed || jumpPower != humanoid.jumpPower || moveDirection != humanoid.MoveDirection || jumping != humanoid.jumping) {
            movementChanged = true;
        }

        humanoid.walkSpeed = walkSpeed;
        humanoid.jumpPower = jumpPower;
        humanoid.MoveDirection = moveDirection;
        humanoid.jumping = jumping;

        double currentTime = Time.realtimeSinceStartup;
        if (currentTime - lastMoveRequest > MOVE_REQUEST_FREQUENCY || movementChanged) {
		    networkManager.SendMoveRequest(humanoid.position.x, humanoid.position.y, humanoid.position.z, humanoid.velocity.x, humanoid.velocity.y, humanoid.velocity.z, humanoid.walkSpeed, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), humanoid.jumping);
            lastMoveRequest = currentTime;
            movementChanged = false;
        }
    }

    private void onTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }

    public void OnCollisionEnter(Collision node)
    {

        if (node.gameObject.tag == "Coin")
        {
            Destroy(node.gameObject);
        }
    }
}
