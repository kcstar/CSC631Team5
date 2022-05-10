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

        Instantiate(Resources.Load("Escape Menu"));
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
        Debug.Log("Joined! " + args.is_current_client + " " + args.user_id);
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
            players.Remove(args.user_id);
        }
    }

    public void OnResponseMove(ExtendedEventArgs eventArgs) {
        ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
        if (args.user_id != userId) {
            if (players.ContainsKey(args.user_id)) {
                GameObject playerObject = players[args.user_id];
                // Move the player
                Player playerScript = playerObject.GetComponent<Player>();
                playerScript.position = new Vector3(args.posX, args.posY, playerScript.position.z);
                playerScript.velocity = new Vector3(args.velX, args.velY, playerScript.velocity.z);
                playerScript.inputX = args.inputX;
                playerScript.jumping = args.jumping;
            } else {
                Debug.Log("Couldn't find the other players!");
            }
        }
    }
}
