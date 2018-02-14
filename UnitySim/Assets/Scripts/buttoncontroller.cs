using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttoncontroller : MonoBehaviour {


	bool topCameraEnabled = true;
	public GameObject topCamera;
	public GameObject robotCamera;
	// Use this for initialization
	void Start () {
		topCamera.SetActive(true);
		robotCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void exitGame(){
		Application.Quit();
	}
	public void changeCamera(){
		if(topCameraEnabled == true){
			topCamera.SetActive(false);
			robotCamera.SetActive(true);
			//Debug.Log("very strange");
			topCameraEnabled = false;

		}else{
			topCamera.SetActive(true);
			robotCamera.SetActive(false);
			topCameraEnabled = true;
		}
	}
}
