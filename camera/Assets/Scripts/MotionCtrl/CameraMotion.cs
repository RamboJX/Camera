using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CameraMotion : MonoBehaviour {

	public Scrollbar Zoom;
	public Scrollbar UpAndDown;
	public Text FovText;			//display the fov
	public Text CameraInfoText;			//display the camera informations
	public GameObject systemController;	//get the system controller
	//public string filePath = "/sdcard/keyframe.txt";

	private TCPConnector.TCPClienter client;

	//default fps is 24
	private float fps = 24;					

	//camera motion vector variables
	private Vector3 cameraTranslateVector = Vector3.zero;

	//////////////Motion Controller
	//gyroscope
	private bool isGyro;
	private Gyroscope gyro;
	private Quaternion rotFix;

	//field of view
	private float FOV;
	
	//joysticks
	private Joystick joystick;
	
	///	temperary variables/// 
	private char [] splitIdentifier = {';'};
	private string [] brokenString;
	private int moveSpeed;
	//this array list is used to store the camera trace keyframes;
//	private ArrayList cameraFrameData = new ArrayList ();
	//we should use list 
	private List<string> cameraFrameData = new List<string> ();
	
	//camera motion keyframe string data
	private string cameraMotionKeyframe;
	private string cameraMotionKeyframeEuler;
	
	void Awake(){
		//initial the joystick
		client = systemController.GetComponent<SystemControl> ().netClient;
		joystick = GameObject.FindGameObjectWithTag (Tags.joystick).GetComponent<Joystick> ();
		cameraMotionKeyframe = null;
		fps = Setting.fps;
		moveSpeed = Setting.cameraMotionSpeed;
	}

	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 1.0f / fps;
		Input.gyro.enabled = true;
		isGyro = Input.isGyroAvailable;
		gyro = Input.gyro;
	
		//make a parent transform node for this camera
		Transform originalParent = transform.parent;		//if this camera has a parent
		GameObject camParent = new GameObject("camParent");
		camParent.transform.position = transform.position;	//let the parent node's position is the same to this camera
		transform.parent = camParent.transform; 	//make the new camParent to this camera
		camParent.transform.parent = originalParent;

		if(isGyro)
		{
			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				camParent.transform.eulerAngles = new Vector3(90f,0f,0f);
			} else if (Screen.orientation == ScreenOrientation.Portrait) {
				camParent.transform.eulerAngles = new Vector3(180f,0f,0f);
			}
			
			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				rotFix = new Quaternion(0, 0, 0.7071f, 0);
			} else if (Screen.orientation == ScreenOrientation.Portrait) {
				rotFix = new Quaternion(0, 0, 1f, 0);
			}
		}
		else{
			print ("NO gyro");
		}
	}

	public void ResetUpAndDown(){
		UpAndDown.value = 0.5f;
	}


	//Use the bar to set FOV
	public void SetFov(){
		FOV = 80f + 140f * (Zoom.value - 0.5f);
		transform.GetComponent<Camera>().fieldOfView = FOV;
		FovText.text = "FOV:" + FOV.ToString ();

	}



	void FixedUpdate(){
		if (Status.IsReviewing){
			//this option is when review the camera trace
			//TODO:first get the current frame camera motion, then translate to there.
			if(Status.CurrentFrameNum <= Status.TotalFrameNum){

				//cameraMotionKeyframe = cameraFrameData[Status.CurrentFrameNum - 1].ToString();
				cameraMotionKeyframe = cameraFrameData[Status.CurrentFrameNum - 1];
				//display the camera motion key frame data;
				CameraInfoText.text = cameraMotionKeyframe;
				//parse cameraMotionKeyframe
				brokenString = cameraMotionKeyframe.Split(splitIdentifier);

			
				transform.position = new Vector3(System.Convert.ToSingle(brokenString[2]), 
												System.Convert.ToSingle(brokenString[3]), 
				                                System.Convert.ToSingle(brokenString[4]));
				transform.rotation = new Quaternion(System.Convert.ToSingle(brokenString[5]), 
				                                     System.Convert.ToSingle(brokenString[6]), 
				                                     System.Convert.ToSingle(brokenString[7]),
				                                     System.Convert.ToSingle(brokenString[8]));
				transform.GetComponent<Camera>().fieldOfView = System.Convert.ToSingle(brokenString[9]);
				Status.CurrentFrameNum = Status.CurrentFrameNum + 1;
			}
		}
		else {
			//use the gyroscope and joystick to control the camera motion
			//camera rotation
			if(isGyro){
				Quaternion camRot = gyro.attitude * rotFix;
				transform.localRotation = camRot ;
			}


			cameraTranslateVector.x = joystick.position.x;		//left&right
			cameraTranslateVector.z = joystick.position.y;		//front back
			cameraTranslateVector.y = (UpAndDown.value - 0.5f)*2f;		//up down

			transform.Translate (cameraTranslateVector * moveSpeed * Time.fixedDeltaTime, Space.Self);
			
	
			//then recoding the data.
			//TODO: not like maya, in Unity3d the coordinate is left-hand.
			//TODO: if we drag the slider, we should reset the frame  
			cameraMotionKeyframe = "camera" + ";" + Status.CurrentFrameNum + ";" +														//frame number
					transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" +									//position
					transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z + ";" + transform.rotation.w + ";" +		//rotation
					transform.GetComponent<Camera>().fieldOfView + "\n";


			//display current camera information on the screen
			CameraInfoText.text = cameraMotionKeyframe;
			//if is connected to server send the camera info text to server synchronized
			if(Setting.connected && Setting.isSendCameraKeyFrame){
				client.SendData(cameraMotionKeyframe);
			}

			if(Status.IsRecording){
				if(Status.CurrentFrameNum <= Status.TotalFrameNum)
				{	//if the frame number is small than the total number , replace the arrylist
					cameraFrameData[Status.CurrentFrameNum - 1] = cameraMotionKeyframe;
				}else{	//else add to the arraylist 
					cameraFrameData.Add(cameraMotionKeyframe);

				}
				Status.CurrentFrameNum = Status.CurrentFrameNum + 1;
			}else{
				Status.TotalFrameNum = cameraFrameData.Count;
			}

			if(Status.IsClearRecord){
				//TODO: if clear the former recorded data
				//ResetRecord();
				cameraFrameData.Clear();
			}
		}
	}

	public void FocusToObject(GameObject obj)
	{
		//direction from camera to the object
		Vector3 direction = obj.renderer.bounds.center - transform.position;
		//rotate the camera to lookAt the object;
		transform.LookAt (direction);
	}

	public float GetFov(){
		return FOV;
	}


	/*
	 * this function is store the keyframe data to the file
	 */

	public void OutputKeyframes(){
		//////METHOD2. Send to MAYA and KEY
		for(int index = 0; index < cameraFrameData.Count; index++){
			cameraMotionKeyframe = "key" + cameraFrameData[index];
			client.SendData(cameraMotionKeyframe);
		}

		//////METHOD1.store the keyframes to the file 
		string keyframeFilePath = "";

		if(Application.platform == RuntimePlatform.Android)
		{
			keyframeFilePath = "/sdcard/keyframes/" + Setting.keyframeFilePath;
		}
		else if(Application.platform == RuntimePlatform.WindowsPlayer){
			keyframeFilePath = "E:/keyframeData/" + Setting.keyframeFilePath;
		}
		else if(Application.platform == RuntimePlatform.WindowsEditor)
		{
			keyframeFilePath = "E:/keyframeData/" + Setting.keyframeFilePath;
		}
/*

#if UNITY_ANDROID
			keyframeFilePath = "/sdcard/keyframes/" + Setting.keyframeFilePath;
#elif UNITY_EDITOR
			keyframeFilePath = "E:/keyframeData/" + Setting.keyframeFilePath;
#else
			keyframeFilePath = "E:/keyframeData/" + Setting.keyframeFilePath;
#endif
*/
		FileInfo cameraKeyframeFile = new FileInfo (keyframeFilePath);
		if (!cameraKeyframeFile.Exists) {	
			using(StreamWriter keyframeFileSw = cameraKeyframeFile.CreateText()){
				for(int index = 0; index < cameraFrameData.Count; index++){
					cameraMotionKeyframe = cameraFrameData[index];
					keyframeFileSw.WriteLine(cameraMotionKeyframe);
				}
				keyframeFileSw.Flush();
				keyframeFileSw.Close();
				cameraFrameData.Clear();
			}
		}
		else//if there is a file
		{
			systemController.GetComponent<SystemControl>().DisplayDebugInfo ("There is already have this file");
		}
	}
}
