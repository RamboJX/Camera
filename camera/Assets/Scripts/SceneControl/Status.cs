﻿using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {


	private static readonly Status instance = new Status();
	private Status()
	{
	}
	
	public static Status GetInstance()
	{
		return instance;
	}
	
	//Used to display the button status
	bool isOnLoading = false;
	public static bool IsOnLoading{
		get{
			return instance.isOnLoading;
		}
		set{
			instance.isOnLoading = value;
		}
	}
	
	//Used to display the button status
	bool isRecording = false;
	public static bool IsRecording{
		get{
			return instance.isRecording;
		}
		set{
			instance.isRecording = value;
		}
	}
	
	//Used to display Review button stauts
	bool isReviewing = false;
	public static bool IsReviewing{
		get{
			return instance.isReviewing;
		}
		set{
			instance.isReviewing = value;
		}
	}

	//used to get the button status of clear former recode;
	bool isClearRecord = false;
	public static bool IsClearRecord{
		get{
			return instance.isClearRecord;
		}
		set{
			instance.isClearRecord = value;
		}
	}

	//used to control the current display frame
	int currentFrameNum = 1;
	public static int CurrentFrameNum{
		get{
			return instance.currentFrameNum;
		}
		set{
			instance.currentFrameNum = value;
		}
	}

	//used to store the total frame number of the former recoded
	int totalFrameNum = 1;
	public static int TotalFrameNum{
		get{
			return instance.totalFrameNum;
		}
		set{
			instance.totalFrameNum = value;
		}
	}
}
