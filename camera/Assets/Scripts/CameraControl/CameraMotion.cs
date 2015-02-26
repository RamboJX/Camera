using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMotion : MonoBehaviour {

	public Scrollbar Zoom;

	private string cameraAnimFile;
	private string cameraMotionKeyframe;
	private float fps = 24;					//default fps is 24

	//camera motion vector variables
	private Vector3 cameraTranslateVector = Vector3.zero;

	//gyroscope ration fix
	private bool gyroEnable;

	//gyroscope value
	private Gyroscope gyro;
	private Quaternion rotFix;
	bool isGyro;

	private Quaternion camRot;

	float FOV;

	char [] splitIdentifier = {';'};
	string [] brokenString;
	//joysticks
	Joystick [] joysticks;

	
	int moveSpeed;
	//bool IsFirstReviewingFrame = true;					//this store the current frame number of the movie

	//this array list is used to store the camera trace keyframes;
	ArrayList cameraFrameData = new ArrayList ();
	ArrayList cameraCurrentFrameInfo = new ArrayList();

	void Awake(){
		cameraAnimFile = Setting.AnimFile;
		cameraMotionKeyframe = null;
		fps = Setting.Fps;
		gyroEnable = true;
	}

	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 1.0f / fps;
		Input.gyro.enabled = true;

		isGyro = Input.isGyroAvailable;
	
		if(isGyro)
		{

			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				transform.eulerAngles = new Vector3 (90, 0, 0);
			} 
			else if (Screen.orientation == ScreenOrientation.Portrait) {
				transform.eulerAngles = new Vector3(180, 0, 0);		
			}

			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				rotFix = new Quaternion(0, 0, 0.7071f, 0);		//I dont know why this value?		
			}
			else if(Screen.orientation == ScreenOrientation.Portrait){
				rotFix = new Quaternion(0, 1.0f, 0, 0);
			}
		}
		else{
			print ("NO gyro");
		}

		//get joysticks
		joysticks = FindObjectsOfType (typeof(Joystick)) as Joystick[];

		moveSpeed = Setting.CameraMotionSpeed;
	}


	void OnGUI()
	{
		string fovInfo = "FOV:";
		FOV = transform.camera.fieldOfView;

		fovInfo = fovInfo + FOV;

		GUI.Label (LayoutAndStrings.zoomLabelRect, fovInfo);



		if(GUI.Button(LayoutAndStrings.zoominBtnRect, "ZOOM+"))//A 2D Rectangle defined by x, y position and width, height
		{	


			FOV = FOV + 5;
		}

		if(GUI.Button(LayoutAndStrings.zoomoutBtnRect, "ZOOM-"))//A 2D Rectangle defined by x, y position and width, height
		{	


			FOV = FOV - 5;
		}
		//this is the vfov, and the hfov is depend on aspect ratio.
		transform.camera.fieldOfView = FOV;
	}

	void FixedUpdate(){
		if (Status.IsReviewing){
			//this option is when review the camera trace
			//TODO:first get the current frame camera motion, then translate to there.
			//print ("reviewing frame is :"
		
			if(Status.CurrentFrameNum <= Status.TotalFrameNum){

				cameraMotionKeyframe = cameraFrameData[Status.CurrentFrameNum - 1].ToString();

				//parse cameraMotionKeyframe
				brokenString = cameraMotionKeyframe.Split(splitIdentifier);

				//print ("brokenString is "+  brokenString[0] + brokenString[1] + brokenString[2]);
				//print ("num" +Status.CurrentFrameNum + "camera motion info" + cameraMotionKeyframe);
				transform.position = new Vector3(System.Convert.ToSingle(brokenString[2]), 
												System.Convert.ToSingle(brokenString[3]), 
				                                System.Convert.ToSingle(brokenString[4]));
				transform.rotation = new Quaternion(System.Convert.ToSingle(brokenString[5]), 
				                                     System.Convert.ToSingle(brokenString[6]), 
				                                     System.Convert.ToSingle(brokenString[7]),
				                                     System.Convert.ToSingle(brokenString[8]));
				transform.camera.fieldOfView = System.Convert.ToSingle(brokenString[9]);

				Status.CurrentFrameNum = Status.CurrentFrameNum + 1;
			}
		}
		else {
			//print ("I am here");
			//use the gyroscope and joystick to control the camera motion
			if(isGyro){
				camRot = Input.gyro.attitude * rotFix;
				transform.localRotation = camRot;
			}

			//joysticks[0] is the left joystick
			cameraTranslateVector.x = joysticks[0].position.x;		//left&right
			cameraTranslateVector.z = joysticks[0].position.y;		//front back
			cameraTranslateVector.y = joysticks[1].position.y;		//up down
			
			//zoomInAndOut = joysticks[1].position.x;
			
			transform.Translate (cameraTranslateVector * moveSpeed * Time.deltaTime, Camera.main.transform);
			
			//this is the vfov, and the hfov is depend on aspect ratio.
			//transform.camera.fieldOfView = ((zoomInAndOut + 1.0f) * 90.0f);

			//then recoding the data.
			//TODO: not like maya, in Unity3d the coordinate is left-hand.
			//TODO: if we drag the slider, we should reset the frame  

			if(Status.IsRecording){
				//frameNum = Status.CurrentFrameNum;

				cameraMotionKeyframe = "camera" + ";" + Status.CurrentFrameNum + ";" +																	//frame number
					transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" +									//position
					transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z + ";" + transform.rotation.w + ";" +		//rotation
					transform.camera.fieldOfView + "\n";

				if(Status.CurrentFrameNum <= Status.TotalFrameNum)
				{	//if the frame number is small than the total number , replace the arrylist
					cameraFrameData[Status.CurrentFrameNum - 1] = cameraMotionKeyframe;
				}else{	//else add to the arraylist 
					cameraFrameData.Add(cameraMotionKeyframe);

				}
			//	print (cameraMotionKeyframe);
				//zoom in and zoom out
				//Status.CurrentFrameNum
				Status.CurrentFrameNum = Status.CurrentFrameNum + 1;
			//	print(Status.CurrentFrameNum);
			
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
	
}
