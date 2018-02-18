using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class robot_move : MonoBehaviour {
	//public Vector3 newLoc;
    public float speed = 10f;
    public Transform robot;
    public float movetime;
    public float smooth = 1f;
    public bool finished = false;
    void Start(){

    }
	// Update is called once per frame
	void Update () {
	}
	

	public void MoveToPosition(Vector3 offset){

		
       
        var currentPos = transform.position;
        transform.position = currentPos + offset;
	}
	public void moveUp(float upspeed){
		transform.Translate(new Vector3(0,0,upspeed * Time.deltaTime));
	}
	public void moveDown(float downspeed){
		transform.Translate(new Vector3(0,0,-downspeed * Time.deltaTime));
	}
	public void moveLeft(float leftspeed){
		transform.Translate(new Vector3(-leftspeed * Time.deltaTime,0,0));
	}
	public void moveRight(float rightspeed){
		transform.Translate(new Vector3(rightspeed * Time.deltaTime,0,0));
	}

	
	public void RotateRobot(float angle, int time){
		transform.eulerAngles = new Vector3(
			transform.eulerAngles.x,
			transform.eulerAngles.y + angle,
			transform.eulerAngles.z
		);
	}
 
}