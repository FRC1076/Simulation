using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_input : MonoBehaviour {
	
    public float speed = 10f;
    //public Transform robot;
    robot_move sn;
    //public float movetime;
	// Use this for initialization
	void Start () {
		sn = gameObject.GetComponent<robot_move>();
		
	}
	public void checkInput(){
		Vector3 pos = transform.position;

        if (Input.GetKey("w") )
        {
            
            //pos.y += panSpeed * Time.deltaTime;
            //Debug.Log(Time.deltaTime);
            //moveUp(speed);
            
            sn.moveUp(speed);
            
        }
        if (Input.GetKey("s"))
        {
            //pos.y -= panSpeed * Time.deltaTime;
           
            sn.moveDown(speed);
        }
        if (Input.GetKey("d"))
        {
            //pos.x += panSpeed * Time.deltaTime;
            
            sn.moveRight(speed);
        }
        if (Input.GetKey("a"))
        {
        	
            sn.moveLeft(speed);
            //pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("m")){
        	
        	//sn.forceMove();
            Debug.Log("DEPRECIATED");
        }
	}
	// Update is called once per frame
	void Update () {
		checkInput();
	}
	
}
