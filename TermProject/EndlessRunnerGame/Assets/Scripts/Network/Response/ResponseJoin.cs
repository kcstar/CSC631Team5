using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseJoinEventArgs : ExtendedEventArgs
{
	public short status { get; set; } // 0 = success
	public int user_id { get; set; }
	public bool is_current_client { get; set; } // is this me?

	public ResponseJoinEventArgs()
	{
		event_id = Constants.SMSG_JOIN;
	}
}

public class ResponseJoin : NetworkResponse
{
	private short status;
	private int user_id;
	private bool is_current_client;

	public ResponseJoin()
	{
	}

	public override void parse()
	{
		status = DataReader.ReadShort(dataStream);
		if (status == 0)
		{
			user_id = DataReader.ReadInt(dataStream);
			is_current_client = DataReader.ReadBool(dataStream);
		}
	}

	public override ExtendedEventArgs process()
	{
		ResponseJoinEventArgs args = new ResponseJoinEventArgs
		{
			status = status
		};
		if (status == 0)
		{
			args.user_id = user_id;
			args.is_current_client = is_current_client;
		}

		return args;
	}
}
