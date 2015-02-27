using UnityEngine;
using System.Collections;

public class FilmProgressBar : MonoBehaviour {
	private string frameDisplayInfo;
	private Rect sliderRect = new Rect (30f, Screen.height - Screen.height/15f , Screen.width - 60.0f, 20.0f);
	private Rect frameDisplayInfoRect = new Rect (30f, Screen.height - Screen.height/15f + 20.0f , Screen.width/2f, 50f);

	void OnGUI()
	{
		//build the slider
		Status.CurrentFrameNum = Mathf.CeilToInt(GUI.HorizontalSlider (sliderRect, Status.CurrentFrameNum, 1.0f, Status.TotalFrameNum));
		frameDisplayInfo = "Current frame is: " + Status.CurrentFrameNum + " | " + "Total frame is: " + Status.TotalFrameNum;
		GUI.Label (LayoutAndStrings.frameDisplayInfoRect, frameDisplayInfo);
	}
}
