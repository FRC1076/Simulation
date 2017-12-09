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
	// Use this for initialization
	void Start () {
		Debug.Log("Recieve Messages Before");
		ReceiveMessages();
		Debug.Log("ReceiveMessages should have run");
	}
	
	// Update is called once per frame
	void Update () {

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
	  string receiveString = Encoding.ASCII.GetString(receiveBytes);

	  Console.WriteLine("Received: {0}", receiveString);
	  messageReceived = true;
	}

	public static void ReceiveMessages()
	{
      Debug.Log("Start of Receive Messages Function")
	  // Receive a message and write it to the console.
	  IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
	  UdpClient u = new UdpClient(e);

	  UdpState s = new UdpState(u,e);
	  

	  Console.WriteLine("listening for messages");
	  u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

	  // Do some work while we wait for a message. For this example,
	  // we'll just sleep
	  while (!messageReceived)
	  {
	    Thread.Sleep(100);
	  }

	}
	
}
