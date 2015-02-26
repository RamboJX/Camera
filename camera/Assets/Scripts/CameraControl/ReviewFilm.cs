using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReviewFilm : MonoBehaviour {
	public Button recordBtn;

	public void SwitchReviewStatus(){
		if(Status.IsReviewing){
			recordBtn.interactable = true;
			Status.IsReviewing = false;

		}
		else{
			recordBtn.interactable = false;
			Status.IsReviewing = true;
		}
	}
}
