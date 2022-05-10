using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Canvas UI;
    private GameplayUI gameplayUI;
    private Vector3 spawnPos;
    private float horizontalInput;
    private int superJumpsRemaining;
	private NetworkManager networkManager;
    // Start is called before the first frame update
    private Player humanoid;
    private double lastMoveRequest = 0;
    private double MOVE_REQUEST_FREQUENCY = 0.5;
    private bool movementChanged = false;
    private int coinCount = 0;
    private float distance = 0f;
    private bool coinCollected = false;
    public float respawnHeight;
    private int requestNumber = 0;

    private int health = 3;
    private bool noHealth = false;

    void Start()
    {
        spawnPos = gameObject.transform.position;
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        humanoid = gameObject.GetComponent<Player>();
        gameplayUI = UI.GetComponent<GameplayUI>();
        //DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y < respawnHeight) {
            Debug.Log($"Distance is {distance}");
            Debug.Log($"Died with {coinCount} coins");
            coinCount = 0;
            humanoid.position = spawnPos;
            humanoid.velocity = new Vector3();
            SceneManager.LoadScene("GameOver");
        }

        if (noHealth)
        {
            SceneManager.LoadScene("GameOver");
        }


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //float walkSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 10.0f : 4.0f;
        //float jumpPower = (Input.GetKey(KeyCode.LeftShift)) ? 18 : 13;
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        bool jumping = Input.GetKey(KeyCode.Space);

        if (horizontal != humanoid.inputX || jumping != humanoid.jumping) {
            movementChanged = true;
        }

        //humanoid.walkSpeed = walkSpeed;
        //humanoid.jumpPower = jumpPower;
        humanoid.inputX = horizontal;
        humanoid.jumping = jumping;

        double currentTime = Time.realtimeSinceStartup;
        if (currentTime - lastMoveRequest > MOVE_REQUEST_FREQUENCY || movementChanged) {
            Debug.Log("Sent move " + requestNumber);
		    networkManager.SendMoveRequest(humanoid.position.x, humanoid.position.y, humanoid.velocity.x, humanoid.velocity.y, horizontal, humanoid.jumping);
            lastMoveRequest = currentTime;
            movementChanged = false;
            requestNumber++;
        }
        distance = Mathf.Abs(startPosition.position.x - GameObject.Find("Player").transform.position.x);
        gameplayUI.updateCoin(coinCount);
        gameplayUI.updateDistance(distance);
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
        if(node.gameObject.tag == "Grenade")
            {
            Debug.Log("COLLIDED WITH GRENADE");
            health -= 1;
            if (health <= 0)
            {
                noHealth = true;
            }
            print(health);
        }
        if ((node.gameObject.tag == "Coin") && (coinCollected == false))
        {
            Debug.Log("COLLIDED WITH COIN");
            AkSoundEngine.PostEvent("Play_SFX_Coin", this.gameObject);
            coinCollected = true;
            coinCount += 1;
            Destroy(node.gameObject);
            coinCollected = false;
        }
    }
    /*
    public void OnCollisionExit(Collision node)
    {
        if ((node.gameObject.tag == "Coin") && (coinCollected == true))
        {
            coinCollected = false;
        }
    }
    */
}
