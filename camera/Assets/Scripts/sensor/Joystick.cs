#pragma strict

using UnityEngine;
using System.Collections;


struct Boundary
{
	public Vector2 min;
	public Vector2 max;
}

public class Joystick : MonoBehaviour {
	private static bool enumeratedJoysticks = false;
	private static Joystick joystick;
	private static float tapTimeDelta = 0.3f;		
	private bool touchPad;
	private Rect touchZone;
	private Vector2 deadZone = Vector2.zero;
	private bool normalize = false;

	//return the position of joystick move
	public Vector2 position;

	private int tapCount;

	private int lastFingerId = -1;
	float tapTimeWindow;

	Vector2 fingerDownPos;
	float fingerDownTime;
	float firstDeltaTime = -0.5f;

	private GUITexture gui;
	private Rect defaultRect;	// the initial joystick's rect
	Boundary guiBoundary;
	Vector2 guiTouchOffset;
	Vector2 guiCenter;

	void Awake(){
		gui = GetComponent <GUITexture>();
		defaultRect = gui.pixelInset;		
	}


	void Start () {
		//defaultRect.x += transform.position.x * Screen.width;
		//defaultRect.y += transform.position.y * Screen.height;

		guiTouchOffset.x = defaultRect.width * 0.5f;
		guiTouchOffset.y = defaultRect.height * 0.5f;

		//catch the center of the GUI, since it doesn't change
		guiCenter.x = defaultRect.x + guiTouchOffset.x;
		guiCenter.y = defaultRect.y + guiTouchOffset.y;

		//let's build the GUI boundary, so we can clamp joystick movement
		guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
		guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
		guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
		guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
	
	}

	void Disable(){
		gameObject.SetActive(false);
		//enumeratedJoysticks = false; 	
	}

	void ResetJoystick(){
		gui.pixelInset = defaultRect;		
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;
	}

	//
	bool IsFingerDown(){
		return (lastFingerId != -1);
	}

	void LatchedFinger(int fingerId){
		if (lastFingerId == fingerId)
			ResetJoystick ();
	}

	// Update is called once per frame
	void Update () {
		//how many touches this frame time
		int count = Input.touchCount;

		//adjust the tap time window while it still available
		if (tapTimeWindow > 0)
			tapTimeWindow -= Time.deltaTime;
		else
			tapCount = 0;		

		//get the touch position
		if (count == 0) 
		{		//if there is no touch envent occured 
				ResetJoystick ();		
		} else 
		{				//if touched the screeen
			for(int i = 0; i < count; i++)
			{
				Touch touch = Input.GetTouch(i);
					
				Vector2 guiTouchPos = touch.position - guiTouchOffset;	


				bool shouldLatchFinger = false;
				//test if it can latch the finger. 			
				if(gui.HitTest(touch.position))		//if the touch hit the joystick
				{
					shouldLatchFinger = true;	
				}
				//latch the finger if this is a new touch
				if(shouldLatchFinger &&(lastFingerId == -1 || lastFingerId != touch.fingerId))
				{
					lastFingerId = touch.fingerId;

					//Accumulate taps if it is within the time window
					if(tapTimeWindow > 0)
						tapCount++;
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}

					//Tell other joysticks we've latched this finger
					//foreach( Joystick j in joysticks )
					//{
					//	if(j != this)
					//		j.LatchedFinger(touch.fingerId);
					//}
				}

				if(lastFingerId == touch.fingerId)
				{
					//override tapcount because iphone's special reason.
					if(touch.tapCount > tapCount)
						tapCount = touch.tapCount;

					//change the loacation of the gui 
					// if guitouchpos.x larger than guiboundary max , return guiboundary max x 
					Rect temp = gui.pixelInset;
					temp.x = Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x );
					temp.y = Mathf.Clamp(guiTouchPos.y, guiBoundary.min.y, guiBoundary.max.y );
					gui.pixelInset = temp;

					if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						ResetJoystick();
				}
			}	//end of for loop	
		}//end of else

		// Get a value between -1 and 1 based on the joystick graphic location
		position.x = ( gui.pixelInset.x + guiTouchOffset.x - guiCenter.x ) / guiTouchOffset.x;
		position.y = ( gui.pixelInset.y + guiTouchOffset.y - guiCenter.y ) / guiTouchOffset.y;
	
		// Adjust for dead zone	
		float absoluteX = Mathf.Abs( position.x );
		float absoluteY = Mathf.Abs( position.y );
		/*
		if ( absoluteX < deadZone.x )
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.x = 0;
		}
		else if ( normalize )
		{
			// Rescale the output after taking the dead zone into account
			position.x = Mathf.Sign( position.x ) * ( absoluteX - deadZone.x ) / ( 1 - deadZone.x );
		}
		
		if ( absoluteY < deadZone.y )
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.y = 0;
		}
		else if ( normalize )
		{
			// Rescale the output after taking the dead zone into account
			position.y = Mathf.Sign( position.y ) * ( absoluteY - deadZone.y ) / ( 1 - deadZone.y );
		}
*/
	}//end of update()
}
