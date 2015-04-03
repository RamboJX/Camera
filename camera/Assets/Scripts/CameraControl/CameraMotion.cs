using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMotion : MonoBehaviour {

	public Scrollbar Zoom;
	public Scrollbar UpAndDown;
	public Text FovText;			//display the fov
	public Text CameraInfoText;			//display the camera informations
	public GameObject systemController;	//get the system controller

	//default fps is 24
	private float fps = 24;					

	//camera motion vector variables
	private Vector3 cameraTranslateVector = Vector3.zero;

	//////////////Motion Controller
	//gyroscope
	private bool isGyro;
	private Vector3 initialOrientation;

	//field of view
	private float FOV;
	
	//joysticks
	private Joystick joystick;



	///	temperary variables/// 
	private char [] splitIdentifier = {';'};
	private string [] brokenString;
	private int moveSpeed;
	//this array list is used to store the camera trace keyframes;
	private ArrayList cameraFrameData = new ArrayList ();
	private ArrayList cameraCurrentFrameInfo = new ArrayList();
	//camera motion keyframe string data
	private string cameraMotionKeyframe;





	void Awake(){
		//initial the joystick
		joystick = GameObject.FindGameObjectWithTag (Tags.joystick).GetComponent<Joystick> ();
		cameraMotionKeyframe = null;
		fps = systemController.GetComponent<Setting>().GetFps();
		moveSpeed = systemController.GetComponent<Setting>().GetCameraMotionSpeed();
	}

	// Use this for initialization
	void Start () {
		Time.fixedDeltaTime = 1.0f / fps;
		Input.gyro.enabled = true;
		isGyro = Input.isGyroAvailable;
	
		if(isGyro)
		{
			initialOrientation = Input.gyro.rotationRateUnbiased;
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
			//print ("reviewing frame is :"
		
			if(Status.CurrentFrameNum <= Status.TotalFrameNum){

				cameraMotionKeyframe = cameraFrameData[Status.CurrentFrameNum - 1].ToString();
				//display the camera motion key frame data;
				CameraInfoText.text = cameraMotionKeyframe;
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
				transform.GetComponent<Camera>().fieldOfView = System.Convert.ToSingle(brokenString[9]);

				Status.CurrentFrameNum = Status.CurrentFrameNum + 1;
			}
		}
		else {
			//print ("I am here");
			//use the gyroscope and joystick to control the camera motion
			if(isGyro){
				//camRot = Input.gyro.attitude * rotFix;
				transform.Rotate(initialOrientation - Input.gyro.rotationRateUnbiased);
			}

			//joysticks[0] is the left joystick
			cameraTranslateVector.x = joystick.position.x;		//left&right
			//cameraTranslateVector.z = joystick.position.y;		//front back
			cameraTranslateVector.z = joystick.position.y;
			cameraTranslateVector.y = (UpAndDown.value - 0.5f)*2f;		//up down
			transform.Translate (cameraTranslateVector * moveSpeed * Time.fixedDeltaTime, Space.World);
			
	
			//then recoding the data.
			//TODO: not like maya, in Unity3d the coordinate is left-hand.
			//TODO: if we drag the slider, we should reset the frame  
			cameraMotionKeyframe = "camera" + ";" + Status.CurrentFrameNum + ";" +														//frame number
					transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" +									//position
					transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z + ";" + transform.rotation.w + ";" +		//rotation
					transform.GetComponent<Camera>().fieldOfView + "\n";
			//display current camera information on the screen
			CameraInfoText.text = cameraMotionKeyframe;

			if(Status.IsRecording){
				//frameNum = Status.CurrentFrameNum;
		/*
				cameraMotionKeyframe = "camera" + ";" + Status.CurrentFrameNum + ";" +														//frame number
					transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" +									//position
					transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z + ";" + transform.rotation.w + ";" +		//rotation
					transform.GetComponent<Camera>().fieldOfView + "\n";
		*/
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
	
}
