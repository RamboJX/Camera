using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageController : MonoBehaviour {
	public Text settingInfo;
	public Text connectInfo;
	public Text cameraInfo;
	public Text debugInfo;
	
	// Use this for initialization
	void Start () {
	
	}

	public void printSettingInfo(string info){
		settingInfo.text = info;
	}

	public void printConnectInfo(string info){
		connectInfo.text = info;
	}

	public void printCameraInfo(string info){
		cameraInfo.text = info;
	}

	public void printDebugInfo(string info){
		debugInfo.text = info;
	}

}
