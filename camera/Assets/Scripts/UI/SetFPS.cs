using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetFPS : MonoBehaviour {
	private Text fpsString;

	void Awake(){
		fpsString = GetComponent<Text> ();
	}

	public void SetParamFps(){
		float fps;
		fps = System.Convert.ToInt32 (fpsString.text);
		Setting.Fps = fps;
		Time.fixedDeltaTime = 1.0f / fps;
	}
}
