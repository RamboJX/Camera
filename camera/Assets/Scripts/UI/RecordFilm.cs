using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordFilm : MonoBehaviour {

	public Button reviewBtn; 

	// Use this for initialization
	void Awake () {
		Status.IsRecording = false;
		reviewBtn.interactable = false;
	}


	public void SwitchRecordingStatus(){
		if(Status.IsRecording){
			Status.IsRecording = false;
			reviewBtn.interactable = true;
		}
		else{
			Status.IsRecording = true;
			reviewBtn.interactable = false;
		}
			
	
	}
}
