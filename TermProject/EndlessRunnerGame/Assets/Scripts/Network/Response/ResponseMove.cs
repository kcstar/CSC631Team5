using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseMoveEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public float posX { get; set; }
	public float posY { get; set; }
	public float posZ { get; set; }
	public float velX { get; set; }
	public float velY { get; set; }
	public float velZ { get; set; }
	public float walkSpeed { get; set; }
	public float walkX { get; set; }
	public float walkZ { get; set; }
	public bool jumping { get; set; }

	public ResponseMoveEventArgs()
	{
		event_id = Constants.SMSG_MOVE;
	}
}

public class ResponseMove : NetworkResponse
{
	private int user_id;
	private float posX;
	private float posY;
	private float posZ;
	private float velX;
	private float velY;
	private float velZ;
	private float walkSpeed;
	private float walkX;
	private float walkZ;
	private bool jumping;

	public ResponseMove()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		posX = DataReader.ReadFloat(dataStream);
		posY = DataReader.ReadFloat(dataStream);
		posZ = DataReader.ReadFloat(dataStream);
		velX = DataReader.ReadFloat(dataStream);
		velY = DataReader.ReadFloat(dataStream);
		velZ = DataReader.ReadFloat(dataStream);
		walkSpeed = DataReader.ReadFloat(dataStream);
		walkX = DataReader.ReadFloat(dataStream);
		walkZ = DataReader.ReadFloat(dataStream);
		jumping = DataReader.ReadBool(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseMoveEventArgs args = new ResponseMoveEventArgs
		{
			user_id = user_id,
			posX = posX,
			posY = posY,
			posZ = posZ,
			velX = velX,
			velY = velY,
			velZ = velZ,
			walkSpeed = walkSpeed,
			walkX = walkX,
			walkZ = walkZ,
			jumping = jumping
		};

		return args;
	}
}
