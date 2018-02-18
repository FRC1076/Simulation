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
	public GameObject robot;
	public UdpState udp_state;
	public robot_move sn;
	CRoot root;
	// Use this for initialization
	void Start (){
		root = new CRoot();
		ReceiveMessages();
		Debug.Log("Networking Start done");
		
    	//Debug.Log("the robot should have rotated");
		
	}

	void CheckType(){
		if (root.message == "transform") {
		    sn = gameObject.GetComponent<robot_move>();
		    float xpos = (float)root.position[0];
		    float ypos = (float)root.position[1];
		    float zpos = (float)root.position[2];
		    
		    Vector3 pos = new Vector3(xpos, ypos, zpos);
			
		    //Vector3 testpos = new Vector3(20,5,1);
    		sn.forceMove(pos);
    		while(sn.finished == false){
    			Debug.Log("1");
    		}
    		NextThing();
    		//Debug.Log(pos);
		}else if (root.message == "rotate") {
			sn = gameObject.GetComponent<robot_move>();
			Debug.Log("This Works");
			Debug.Log(root.orientation[0]);
			Debug.Log(root.orientation[1]);
			sn = gameObject.GetComponent<robot_move>();
			float angleOrient = (float)root.orientation[0];
		    int timeOrient = (int)root.orientation[1];

			StartCoroutine(sn.RotateRobot(angleOrient, timeOrient));
			Debug.Log("This also Works");
			while(sn.finished == false){
    			Debug.Log("1");
    		}
    		NextThing();
		}

	}
	

	void NextThing(){
		if (root.message == "transform") {
		    sn = gameObject.GetComponent<robot_move>();
		    float xpos = (float)root.position[0];
		    float ypos = (float)root.position[1];
		    float zpos = (float)root.position[2];
		    
		    Vector3 pos = new Vector3(xpos, ypos, zpos);
			
		    //Vector3 testpos = new Vector3(20,5,1);
    		sn.forceMove(pos);
    		while(sn.finished == false){
    			Debug.Log("1")
    		}
    		NextThing();
    		//Debug.Log(pos);
		}else if (root.message == "rotate") {
			sn = gameObject.GetComponent<robot_move>();
			Debug.Log("This Works");
			Debug.Log(root.orientation[0]);
			Debug.Log(root.orientation[1]);
			sn = gameObject.GetComponent<robot_move>();
			float angleOrient = (float)root.orientation[0];
		    int timeOrient = (int)root.orientation[1];

			StartCoroutine(sn.RotateRobot(angleOrient, timeOrient));
			Debug.Log("This also Works");
			while(sn.finished == false){
    			Debug.Log("1")
    		}
    		NextThing();
	}
	// Update is called once per frame
	void Update () {
		if (messageReceived) {
			// Parse the string into object
			// SimulatorMessage sm = createObjectFromJson(receiveString);


			Debug.Log ("Networking.Update() messageReceived");
			Debug.Log (receiveString);
			
			

			//CRoot root = JsonUtility.FromJsonOverwrite<CRoot>(receiveString);
			JsonUtility.FromJsonOverwrite(receiveString, root);
			
			CheckType();

			
			//root.message = "transform";
			//root.position = (20,5,1);
			
			
			
			

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



public class CRoot
{
	public string receiver;
	public string sender;
	public string message;
	public List<double> position;
	public List<double> orientation; //Orientaition: (angle, speed)


}