using System;
using UnityEngine;
using Akka.Actor;

public class PongActor: UntypedActor
{
	protected override void OnReceive(object message)
	{
		Debug.Log (DateTime.Now.ToLongTimeString() + " PongActor recieved: " + message.ToString ());
        System.Threading.Thread.Sleep(3000);
		Sender.Tell (message);
	}
}

