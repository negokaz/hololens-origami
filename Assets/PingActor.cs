using System;
using UnityEngine;
using Akka.Actor;

public class PingActor: UntypedActor
{
	protected override void OnReceive(object message)
	{
		Debug.Log (DateTime.Now.ToLongTimeString() + " PingActor recieved: " + message.ToString ());
        System.Threading.Thread.Sleep(3000);
        Context.ActorSelection ("akka.tcp://systemB@localhost:8182/user/Pong").Tell(message);
	}
}

