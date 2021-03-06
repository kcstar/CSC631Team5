using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private ConnectionManager cManager;
	private NetworkConfiguration networkConfig;

	void Awake()
	{
		//DontDestroyOnLoad(gameObject);

		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();

		NetworkRequestTable.init();
		NetworkResponseTable.init();
	}

	// Start is called before the first frame update
	void Start()
    {
		cManager = GetComponent<ConnectionManager>();
		networkConfig = GameObject.Find("Network Configuration").GetComponent<NetworkConfiguration>();

		cManager.SetHostIP(networkConfig.hostIP);
		cManager.SetHostPort(networkConfig.hostPort);

		if (cManager)
		{
			cManager.setupSocket();
			
			SendJoinRequest();	// FIND A BETTER PLACE FOR THIS!

			StartCoroutine(RequestHeartbeat(0.1f));//0.1f
		}
	}

	void OnDestroy() {
		SendLeaveRequest();
	}

	public bool SendJoinRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestJoin request = new RequestJoin();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendLeaveRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestLeave request = new RequestLeave();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendMoveRequest(float posX, float posY, float velX, float velY, float inputX, bool jumping)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestMove request = new RequestMove();
			request.send(posX, posY, velX, velY, inputX, jumping);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public IEnumerator RequestHeartbeat(float time)
	{
		yield return new WaitForSeconds(time);

		if (cManager)
		{
			RequestHeartbeat request = new RequestHeartbeat();
			request.send();
			cManager.send(request);
		}

		StartCoroutine(RequestHeartbeat(time));
	}
}
