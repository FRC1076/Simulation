using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class Networking : MonoBehaviour {

	public static int listenPort = 8000;
	public static bool messageReceived = false;
	public static string receiveString;
	public UdpState udp_state;

	// Use this for initialization
	void Start () {
		ReceiveMessages();
		Debug.Log("Networking Start done");
	}
	
	// Update is called once per frame
	void Update () {
		if (messageReceived) {
			// Parse the string into object
			// SimulatorMessage sm = createObjectFromJson(receiveString);


			Debug.Log ("Networking.Update() messageReceived");
			Debug.Log (receiveString);
			CRoot root = JsonUtility.FromJson<CRoot>(receiveString);

			if (root.message == "transform") {
			    robot_move sn = gameObject.GetComponent<robot_move>();
        		sn.forceMove(root.position);
			}

			// Set up to receive the next message
			this.ReceiveMoreMessages();
		}
	}
	public class SimulatorMessage
	{
		public string sender;
		public string message;
		//  add the others here as well
	}
	public class UdpState
	{
	    public UdpClient client;
	    public IPEndPoint endpoint;
	    public UdpState(UdpClient c, IPEndPoint iep)
	    {
	        this.client = c;
	        this.endpoint = iep;
	    }
	}
	public static void ReceiveCallback(IAsyncResult ar)
	{
	  UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).client;
	  IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).endpoint;

	  Byte[] receiveBytes = u.EndReceive(ar, ref e);
	  receiveString = Encoding.ASCII.GetString(receiveBytes);

	  Console.WriteLine("Received: {0}", receiveString);
	  messageReceived = true;
	}

	public void ReceiveMessages()
	{
		Debug.Log ("Networking.ReceiveMessages Start");
	  	// Receive a message and write it to the console.
	  	IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
	  	UdpClient u = new UdpClient(e);

	  	this.udp_state = new UdpState(u,e);

	  	Console.WriteLine("listening for messages");

	  	// clear the received state
	  	messageReceived = false;

	  	// install the callback for the next message
	  	u.BeginReceive(new AsyncCallback(ReceiveCallback), this.udp_state);
	}

	public void ReceiveMoreMessages()
	{
		messageReceived = false;
		this.udp_state.client.BeginReceive(new AsyncCallback(ReceiveCallback), this.udp_state);
	}
	
}


[System.Serializable]
public class CRoot
{
	public string receiver;
	public string sender;
	public string message;
	public Vector3 position;
	public Vector2 orientation;


}