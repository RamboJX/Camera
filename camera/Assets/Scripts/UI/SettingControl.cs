using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;

public class SettingControl : MonoBehaviour {
	public InputField IPInputField;
	public InputField FpsInputField;
	public InputField MoveSpeedInputField;
	public InputField KeyframeFilePathInputField;

	public GameObject SystemControl;
	public Text settingInfoText;

	void Awake(){
		settingInfoText.text = "Setting:" + " IP:" + Setting.serverIpAddress
			+ ", FPS:" + Setting.fps.ToString ()
				+ ", MoveSpeed:" + Setting.cameraMotionSpeed.ToString () + ", keyframes file:" + Setting.keyframeFilePath ;
	}

	void OnEnable(){
		IPInputField.text = Setting.serverIpAddress;
		FpsInputField.text = Setting.fps.ToString ();
		MoveSpeedInputField.text = Setting.cameraMotionSpeed.ToString ();
		KeyframeFilePathInputField.text = Setting.keyframeFilePath;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void SettingApply(){
		Setting.cameraMotionSpeed = System.Convert.ToInt32(MoveSpeedInputField.text);
		Setting.fps= (float)System.Convert.ToDouble(FpsInputField.text);
		Setting.serverIpAddress = IPInputField.text;
		Setting.keyframeFilePath = KeyframeFilePathInputField.text;
		settingInfoText.text = "Setting:" + " IP:" + IPInputField.text + ", FPS:" + FpsInputField.text +
			", MoveSpeed:" + MoveSpeedInputField.text + ", keyframes file:" + KeyframeFilePathInputField.text;
	}
}
