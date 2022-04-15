using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Dictionary<int, GameObject> players;
    private NetworkManager networkManager;
    public GameObject playerPrefab;
    private int garbageVar = 1;

    // Start is called before the first frame update
    void Start()
    {

        players = new Dictionary<int, GameObject>();
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();

        MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();

        msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
        msgQueue.AddCallback(Constants.SMSG_LEAVE, OnResponseLeave);

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
        if (!args.is_current_client) {
            GameObject otherPlayer = Instantiate(playerPrefab, new Vector3(0 + garbageVar, 1, (float)-61.34), Quaternion.identity);
            garbageVar++;
            players.Add(args.user_id, otherPlayer);
        }
    }

    public void OnResponseLeave(ExtendedEventArgs eventArgs) {
        ResponseLeaveEventArgs args = eventArgs as ResponseLeaveEventArgs;
        GameObject playerObject = players[args.user_id];
        if (playerObject != null) {
            Destroy(playerObject);
        }
    }
}
