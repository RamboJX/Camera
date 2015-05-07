using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {
	public static string serverIpAddress = "192.168.1.5";
	public static int portNum = 3000;
	//display FPS
	public static float fps = 24;
	//camera motion speed
	public static int cameraMotionSpeed = 108;
	public static string keyframeFilePath = "keyframeFile.txt";

	public static bool connected = false;
	public static bool isSendCameraKeyFrame = false;			//this will send the key frame info like "camera;1;0;0;..." to make the maya synchronize monitor the client

}
	

