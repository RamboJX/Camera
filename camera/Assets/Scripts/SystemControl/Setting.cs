using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	public static string serverIpAddress = "127.0.0.1";
	//display FPS
	public static float fps = 24;
	//camera motion speed
	public static int cameraMotionSpeed = 108;

	public string GetIpAddress(){
		return Setting.serverIpAddress;
	}
	public void SetIpAddress(string ip){
		Setting.serverIpAddress = ip;
	}

	public float GetFps(){
		return Setting.fps;
	}
	public void SetFps(float fps){
		Setting.fps = fps;
	}

	public int GetCameraMotionSpeed(){
		return Setting.cameraMotionSpeed;
	}
	public void SetCameraMotionSpeed( int cameraMotionSpeed){
		Setting.cameraMotionSpeed = cameraMotionSpeed;
	}
}
	

