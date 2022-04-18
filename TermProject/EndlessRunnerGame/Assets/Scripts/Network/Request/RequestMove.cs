﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMove : NetworkRequest
{
	public RequestMove()
	{
		request_id = Constants.CMSG_MOVE;
	}

	public void send(float posX, float posY, float posZ, float velX, float velY, float velZ, float walkSpeed, float walkX, float walkZ, bool jumping)
	{
		packet = new GamePacket(request_id);
		packet.addFloat32(posX);
		packet.addFloat32(posY);
		packet.addFloat32(posZ);
		packet.addFloat32(velX);
		packet.addFloat32(velY);
		packet.addFloat32(velZ);
		packet.addFloat32(walkSpeed);
		packet.addFloat32(walkX);
		packet.addFloat32(walkZ);
		packet.addBool(jumping);
	}
}
