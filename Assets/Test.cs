using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akka.Actor;
using Akka.Configuration;

// https://getakka.net/articles/actors/receive-actor-api.html
public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var configA = ConfigurationFactory.ParseString (@"
			akka {
			   actor.provider = cluster
			    remote {
			        dot-netty.tcp {
			            port = 8181
			            hostname = localhost
			        }
			    }
			    cluster {
			       seed-nodes = [""akka.tcp://ClusterSystem@localhost:8181""]
			    }
			}
		");
		var actorSystemA = ActorSystem.Create("systemA", configA);
		var configB = ConfigurationFactory.ParseString (@"
			akka {
			   actor.provider = cluster
			    remote {
			        dot-netty.tcp {
			            port = 8182
			            hostname = localhost
			        }
			    }
			    cluster {
			       seed-nodes = [""akka.tcp://ClusterSystem@localhost:8081""]
			    }
			}
		");
		var actorSystemB = ActorSystem.Create("systemB", configB);

		var pingProps = Props.Create (typeof(PingActor));
		var pingActor = actorSystemA.ActorOf (pingProps, "Ping");
		pingActor.Tell ("hello");

		var pongProps = Props.Create (typeof(PongActor));
		var pongActor = actorSystemB.ActorOf (pongProps, "Pong");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
