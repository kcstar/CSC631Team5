using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private int superJumpsRemaining;
	private NetworkManager networkManager;
    // Start is called before the first frame update
    private Player humanoid;

    void Start()
    {
        humanoid = gameObject.GetComponent<Player>();
		DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        humanoid.walkSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 5.0f : 3.0f;
        humanoid.MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }
    
    private void FixedUpdate()
    {
            /*
            float jumpPower = 6f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            */

        humanoid.jumping = Input.GetKey(KeyCode.Space);
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
        Debug.Log("Collision Object: " + node.gameObject.tag);

        if (node.gameObject.tag == "Coin")
        {
            Destroy(node.gameObject);
        }
    }
}
