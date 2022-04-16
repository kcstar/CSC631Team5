using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Dictionary<int, GameObject> players;
    private NetworkManager networkManager;
    public GameObject playerPrefab;
    private int garbageVar = 1;
    private int userId;

    // Start is called before the first frame update
    void Start()
    {

        players = new Dictionary<int, GameObject>();
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();

        MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();

        msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
        msgQueue.AddCallback(Constants.SMSG_LEAVE, OnResponseLeave);
        msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnResponseJoin(ExtendedEventArgs eventArgs) {
        ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;
        /*
        if (args.user_id == Constants.OP_ID) {
            
        }*/
        Vector3 spawnPos = new Vector3(0 + garbageVar, 16, (float)-53.34);
        if (!args.is_current_client) {
            GameObject otherPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            garbageVar++;
            players.Add(args.user_id, otherPlayer);
        } else {
            userId = args.user_id;
        }
    }

    public void OnResponseLeave(ExtendedEventArgs eventArgs) {
        ResponseLeaveEventArgs args = eventArgs as ResponseLeaveEventArgs;
        GameObject playerObject = players[args.user_id];
        if (playerObject != null) {
            Destroy(playerObject);
        }
    }

    public void OnResponseMove(ExtendedEventArgs eventArgs) {
        ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
        Debug.Log(args.walkSpeed + " " + args.walkDir + " " + args.jumping);
        if (args.user_id != userId) {
            GameObject playerObject = players[args.user_id];
            if (playerObject != null) {
                // Move the player
                Player playerScript = playerObject.GetComponent<Player>();
                playerScript.position = new Vector3(args.posX, args.posY, args.posZ);
                playerScript.velocity = new Vector3(args.velX, args.velY, args.velZ);
                playerScript.walkSpeed = args.walkSpeed;
                playerScript.MoveDirection = new Vector3(args.walkDir, 0, 0);
                playerScript.jumping = args.jumping;
            }
        }
    }
}
