using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;

public class SettingControl : MonoBehaviour {
	public InputField IPInputField;
	public InputField FpsInputField;
	public InputField MoveSpeedInputField;
	public GameObject SystemControl;
	public Text settingInfoText;

	void Awake(){
		settingInfoText.text = "Setting:" + " IP:" + SystemControl.GetComponent<Setting> ().GetIpAddress() 
				+ ", FPS:" + SystemControl.GetComponent<Setting> ().GetFps ().ToString()
				+ ", MoveSpeed:" + SystemControl.GetComponent<Setting> ().GetCameraMotionSpeed ().ToString ();
	}

	void OnEnable(){
		IPInputField.text = SystemControl.GetComponent<Setting> ().GetIpAddress();
		FpsInputField.text = SystemControl.GetComponent<Setting> ().GetFps ().ToString();
		MoveSpeedInputField.text = SystemControl.GetComponent<Setting> ().GetCameraMotionSpeed ().ToString ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void SettingApply(){
		SystemControl.GetComponent<Setting> ().SetCameraMotionSpeed (System.Convert.ToInt32(MoveSpeedInputField.text));
		SystemControl.GetComponent<Setting> ().SetFps ((float)System.Convert.ToDouble(FpsInputField.text));
		SystemControl.GetComponent<Setting> ().SetIpAddress (IPInputField.text);
		settingInfoText.text = "Setting:" + " IP:" + IPInputField.text + ", FPS:" + FpsInputField.text + ", MoveSpeed:" + MoveSpeedInputField.text;
	}
}
