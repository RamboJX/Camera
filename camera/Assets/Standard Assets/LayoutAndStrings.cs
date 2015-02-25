using UnityEngine;
using System.Collections;

public class LayoutAndStrings :MonoBehaviour  {

	/////////////////////////// GUI postion //////////////////////////////////////////
	public static float tenPercentsOfScreenWidth = Screen.width/15;
	public static float tenPercentsOfScreenHeight = Screen.height/15;
	public static float screenMargin = 30;
	public static float btnMargin  = 10;


	public static Rect windowRect = new Rect(tenPercentsOfScreenWidth * 2.0f, screenMargin, 11.0f * tenPercentsOfScreenWidth, 12.0f * tenPercentsOfScreenHeight);	//setting popup window
	public static Rect scrollViewRect = new Rect(tenPercentsOfScreenWidth * 2.0f + 5.0f, screenMargin + 10.0f, 8.0f * tenPercentsOfScreenWidth, 9.0f * tenPercentsOfScreenHeight);

//left side
	public static Rect setingBtnRect = new Rect(screenMargin, screenMargin, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect recordBtnRect = new Rect(screenMargin, screenMargin + btnMargin + tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect reviewBtnRect = new Rect(screenMargin, screenMargin + 2.0f*btnMargin + 2.0f*tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect loadSceneBtnRect = new Rect(screenMargin, screenMargin + 3.0f*btnMargin + 3.0f*tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect loadAnimBtnRect = new Rect(screenMargin, screenMargin + 4.0f*btnMargin + 4.0f*tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect clearBtnRect = new Rect(screenMargin, screenMargin + 5.0f*btnMargin + 5.0f*tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect quitBtnRect = new Rect(screenMargin, screenMargin + 6.0f*btnMargin + 6.0f*tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);

//down side
	public static Rect sliderRect = new Rect (screenMargin, Screen.height - tenPercentsOfScreenHeight, Screen.width - 60.0f, 20.0f);
	public static Rect frameDisplayInfoRect = new Rect (screenMargin, Screen.height - tenPercentsOfScreenHeight + 20.0f , tenPercentsOfScreenWidth * 5, 50);

//right side 
	public static Rect objViewBtnRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect deleteObjRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin + btnMargin + tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect focusOnObjRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin + 2.0f * btnMargin + 2.0f * tenPercentsOfScreenHeight, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);
	public static Rect zoominBtnRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin  + 6.0f * tenPercentsOfScreenHeight, tenPercentsOfScreenWidth / 2.0f, tenPercentsOfScreenHeight);
	public static Rect zoomoutBtnRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin + 3.0f + 7.0f * tenPercentsOfScreenHeight, tenPercentsOfScreenWidth / 2.0f, tenPercentsOfScreenHeight);
	public static Rect zoomLabelRect = new Rect (Screen.width - screenMargin - tenPercentsOfScreenWidth, screenMargin + 5.0f * tenPercentsOfScreenHeight - 5.0f, tenPercentsOfScreenWidth, tenPercentsOfScreenHeight);

	//public static Rect focusPopwindowRect = new Rect(tenPercentsOfScreenWidth * 6.0f, screenMargin, 7.0f * tenPercentsOfScreenWidth, 12.0f * tenPercentsOfScreenHeight);	
//	public static Rect focusScrollViewRect = new Rect(tenPercentsOfScreenWidth * 7.0f + 10.0f, screenMargin + 10.0f, 3.0f * tenPercentsOfScreenWidth, 9.0f * tenPercentsOfScreenHeight);
}
