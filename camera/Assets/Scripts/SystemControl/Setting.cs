using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	private static readonly Setting instance = new Setting();
	private Setting()
	{
	}

	public static Setting GetInstance()
	{
		return instance;
	}
	
	//private...right, can only access use instance
	string serverIpAddress = "127.0.0.1";
	//so make a method to get the value
	public static string ServerIpAddress{
		get{
			return instance.serverIpAddress;
		}
		set{
			instance.serverIpAddress = value;
		}
	}

	
	string objFile = "e:/scene/oneCubeTwoBall.obj";
	public static string ObjFile{
		get{
			return instance.objFile;
		}
		set
		{
			instance.objFile = value;
		}
	}

	string animFile = "E:/scene/";
	public static string AnimFile{
		get{
			return instance.animFile;
		}
		set{
			instance.animFile = value;
		}
	}


	//camera trace store file
	string cameraTraceFile = "E://camera.anim";
	public static string CameraTraceFile{
		get{
			return instance.cameraTraceFile;
		}
		set{
			instance.cameraTraceFile = value;
		}
	}

	//display FPS
	float fps = 24;
	public static float Fps{
		get{
			return instance.fps;
		}
		set{
			instance.fps = value;
		}
	}

	//camera motion speed
	int cameraMotionSpeed = 108;
	public static int CameraMotionSpeed{
		get{
			return instance.cameraMotionSpeed;
		}
		set{
			instance.cameraMotionSpeed = value;
		}
	}
}
	

