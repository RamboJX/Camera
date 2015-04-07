using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SystemControl : MonoBehaviour {
	public Text debugText;
	void Awake(){
	//	debugText = GameObject.Find ("SettingText");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void ReloadLevel(){
		Application.LoadLevel ("VirtualCam");
	}

	public void QuitApp(){
		Application.Quit();
	}

	public void DisplayDebugInfo(string info){
		debugText.text = info;
	}
}
