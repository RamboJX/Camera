using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net.Sockets;
using TCPConnector;

public class SystemControl : MonoBehaviour {
	//public Text debugText;
	public TCPClienter netClient =  new TCPClienter();
	public GameObject infoPannel;

	void Awake(){
	//	debugText = GameObject.Find ("SettingText");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ConnectToServer(){
		if(netClient.fnConnect (Setting.serverIpAddress, Setting.portNum)){
			//setting the text to connected
			infoPannel.GetComponent<MessageController>().printConnectInfo("CONNECTED");
			Setting.connected = true;
		}
		else{
			//setting the text to disconnected
			infoPannel.GetComponent<MessageController>().printConnectInfo("DISCONNECTED");
			Setting.connected = false;
		}
	}

	public void DisconnectToServer(){
		if (netClient.fnClose ()) {
			infoPannel.GetComponent<MessageController>().printConnectInfo("DISCONNECTED");
			Setting.connected = false;		
		}
		else{
			//setting the text to disconnected
			infoPannel.GetComponent<MessageController>().printConnectInfo("CONNECTED");
			Setting.connected = true;
		}
	}

	public void ReloadLevel(){
		Application.LoadLevel ("VirtualCam");
	}

	public void QuitApp(){
		Application.Quit();
	}

	public void DisplayDebugInfo(string info){
		infoPannel.GetComponent<MessageController>().printDebugInfo(info);
	}
}
