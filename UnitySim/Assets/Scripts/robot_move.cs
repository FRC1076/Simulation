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
	

	public void forceMove(Vector3 newLoc){

		
        StartCoroutine(MoveToPosition(robot, newLoc, movetime));

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

	public IEnumerator MoveToPosition(Transform transform, Vector3 offset, float timeToMove)
    {
      finished = false;
      var currentPos = transform.position;
      var t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, currentPos + offset, t);
             yield return null;
      }

      finished = true;
      //transform.position = currentPos + offset;
    }

    public IEnumerator RotateRobot(float angle, int time){
		finished = false;
		//Vector3 robotRotation = new Vector3(0, angle * 90, 0);
		//transform.Rotate(robotRotation * (time * Time.deltaTime));
		var t = 0f;
		Quaternion targetRotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);
		while(t < 1)
		{
			t += Time.deltaTime / time;
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation , t);
			yield return null;
		}
		finished = true;
    }
}