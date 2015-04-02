using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour {
	public InputField IPInputField;
	public InputField FpsInputField;
	public InputField MoveSpeedInputField;
	public GameObject SystemControl;

	void Awake(){
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
}
