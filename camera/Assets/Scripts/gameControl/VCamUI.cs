using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Threading;




public class VCamUI : MonoBehaviour {

	//public static ObjImporter sceneImp;
	private static bool displaySettingPopWindow = false;
	private string frameDisplayInfo;
	private Rect settingWindowRect;

	//path URL of different environment
	private static string PathURL ;



	//GUIStyle recordBtnStyle;
	/*
	 * Use this function for initialization
	 */ 
	void Start () {
		Status.IsUIOn = false;
		Status.IsOnLoading = false;
		Status.IsRecording = false;
		displaySettingPopWindow = false;

		PathURL = 	"file://" + Application.dataPath + "/StreamingAssets/";
/*	#if UNITY_ANDROID
		"/sdcard/asset/";
		#elif UNITY_IPHONE
		Application.dataPath + "/Raw/";
		#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
		"file://" + Application.dataPath + "/StreamingAssets/";
		#else
		string.Empty;
		#endif
*/
		
	}

	/*
	 * Update is called once per frame
	 * 
	 */
	void Update () {
	}
	

	void settingWindow(int windowID)
	{
		float onePieceOfWidth = settingWindowRect.width / 3.0f;
		float onePieceOfHeight = settingWindowRect.height / 12.0f;

		//Rect is (x, y, width, height), upleft is the original point, and x is the horizonal
		GUI.Label(new Rect(10, 10, onePieceOfWidth, onePieceOfHeight), "Input IP Addr:");
		Setting.ServerIpAddress = GUI.TextField(new Rect(10 + onePieceOfWidth, 10, onePieceOfWidth, onePieceOfHeight), Setting.ServerIpAddress, 25);
		//print(CtoS.server_adr);
		
		GUI.Label(new Rect(10, 10 + onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), "Motion Speed:");
		string strSpeed = "" + Setting.CameraMotionSpeed;
		strSpeed = GUI.TextField(new Rect(10 + onePieceOfWidth, 10 + onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), strSpeed, 25);
		Setting.CameraMotionSpeed = int.Parse(strSpeed);
		
		GUI.Label(new Rect(10, 10 + onePieceOfHeight + onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), "Input Obj Path:");
		Setting.ObjFile = GUI.TextField(new Rect(10 + onePieceOfWidth, 10 + onePieceOfHeight + onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), Setting.ObjFile, 200);
		//print(CtoS.FilePath);
		
		GUI.Label(new Rect(10,10 + (onePieceOfHeight * 3), onePieceOfWidth, onePieceOfHeight), "Input Anim Path:");
		Setting.AnimFile = GUI.TextField(new Rect(10 + onePieceOfWidth, 10 + (onePieceOfHeight * 3), onePieceOfWidth, onePieceOfHeight), Setting.AnimFile, 200);

		GUI.Label(new Rect(10, 10 + (onePieceOfHeight*4), onePieceOfWidth, onePieceOfHeight), "FPS:");
		strSpeed = "" + Setting.Fps;
		Setting.Fps = int.Parse (GUI.TextField (new Rect (10 + onePieceOfWidth, 10 + 4*onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), strSpeed));
		
	}


	private IEnumerator LoadGameObject(string path)
	{
		WWW bundle = new WWW(path);
		yield return bundle;
		yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);
	}

	void OnGUI()
	{
		//popup Setting window
		if(GUI.Button(LayoutAndStrings.setingBtnRect, "Setting"))
		{
			if(displaySettingPopWindow == false)
				displaySettingPopWindow = true;
			else
				displaySettingPopWindow = false;
		}
		if(displaySettingPopWindow)
		{
			settingWindowRect =	GUI.Window(0,LayoutAndStrings.windowRect, settingWindow, "");
		}

		//load scene
		if(GUI.Button(LayoutAndStrings.loadSceneBtnRect, "load scene"))//A 2D Rectangle defined by x, y position and width, height
		{	
			StartCoroutine(LoadGameObject(PathURL + "obj3fortest.assetbundle"));
		}


		if(GUI.Button(LayoutAndStrings.clearBtnRect,"clear scene"))
		{
			Application.LoadLevel("VirtualCam");
		}

		//recording button
		if(!Status.IsReviewing){
			if (GUI.Button(LayoutAndStrings.recordBtnRect, "Record")) {	//25frames per second   
				if(Status.IsRecording)
					Status.IsRecording = false;
				else
					Status.IsRecording = true;
			}
		}

		//reviewing button
		if(!Status.IsRecording){
			if (GUI.Button (LayoutAndStrings.reviewBtnRect, "Review")) {
				if(Status.IsReviewing)
					Status.IsReviewing = false;
				else
					Status.IsReviewing = true;
			}
		}

		//button of back, add by bubble, no use
		if(GUI.Button(LayoutAndStrings.quitBtnRect, "Quit"))
		{	
			Application.Quit();
		}


		Status.CurrentFrameNum = Mathf.CeilToInt(GUI.HorizontalSlider (LayoutAndStrings.sliderRect, Status.CurrentFrameNum, 1.0f, Status.TotalFrameNum));

		frameDisplayInfo = "Current frame is: " + Status.CurrentFrameNum + " | " + "Total frame is: " + Status.TotalFrameNum;
		GUI.Label (LayoutAndStrings.frameDisplayInfoRect, frameDisplayInfo);


	}
}
