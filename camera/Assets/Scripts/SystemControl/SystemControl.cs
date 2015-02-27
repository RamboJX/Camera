using UnityEngine;
using System.Collections;

public class SystemControl : MonoBehaviour {

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
}
