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

	// Use this for initialization
	void Start () {
		ReceiveMessages();
		Debug.Log("Networking Start done");
	}
	
	// Update is called once per frame
	void Update () {
		if (messageReceived)
		{
			// Parse the string into object
			// SimulatorMessage sm = createObjectFromJson(receiveString);

			messageReceived = false;

			// if ((sm.sender == "frcsim") && (sm.message == "move-to")) {
			//       send sm.position to the UnityRobot model
			// }
		}
	}
	public class SimulatorMessage
	{
		public string sender;
		public string message;
		// add more fields to the message (to guide json parse)
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

	public static void ReceiveMessages()
	{
		Debug.Log ("Networking.ReceiveMessages Start");
	  	// Receive a message and write it to the console.
	  	IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
	  	UdpClient u = new UdpClient(e);

	  	UdpState s = new UdpState(u,e);
	  

	  	Console.WriteLine("listening for messages");
	  	u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

	}
	
}