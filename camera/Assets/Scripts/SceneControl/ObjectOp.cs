using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectOp : MonoBehaviour {

	private bool objPopWindowBool;
	private bool foucsOnObjBool;
	private bool deleteObjBool;

	private Vector2 scrollViewPos = new Vector2(0, 0);
	private Vector2 focusScrollViewPos = new Vector2 (0, 0);

	private string objectNames = ""; 
	private string objName = "";
	private ArrayList objectNameList = new ArrayList();

	List<bool> objectNameBool = new List<bool>();

	char splitIdentifier = ' ';
	string delObjectNames = "";
	string [] delObjectNameList;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void objPopWindow(int windowID)
	{
		float onePieceOfWidth = LayoutAndStrings.windowRect.width / 3.0f;
		float onePieceOfHeight = LayoutAndStrings.windowRect.height / 12.0f;

		GUI.Label(new Rect(10, 5, onePieceOfWidth, onePieceOfHeight), "The Object List:");


		//find all the GameObject in the scene

		GUI.TextArea (new Rect (10, 10 + onePieceOfHeight, 3f * onePieceOfWidth - 20, 6.0f * onePieceOfHeight), objectNames, 10000);


		GUI.Label(new Rect(10, 15 + 7 * onePieceOfHeight, 3f * onePieceOfWidth, onePieceOfHeight), "Input Delete Object Names, Use Space To Split:");
		delObjectNames = GUI.TextField (new Rect (10, 20 + 8 * onePieceOfHeight, 3f * onePieceOfWidth - 20, onePieceOfHeight), delObjectNames, 200);

		if(GUI.Button(new Rect(onePieceOfWidth, 30 + 9*onePieceOfHeight, onePieceOfWidth, onePieceOfHeight), "DELETE"))//A 2D Rectangle defined by x, y position and width, height
		{	
				//TODO: delete object operation
			delObjectNameList = delObjectNames.Split(splitIdentifier);
			foreach(string obj in delObjectNameList){
				Status.RemoveObjectFromList(obj);
				//TODO:Delete this game object
				GameObject gObj = GameObject.Find(obj);
				Destroy(gObj);
			}
			//update the objectNames
			objectNameList = Status.ObjectNames;
			int objectCount = objectNameList.Count;
			objectNames = "";
			for (int i=0; i<objectCount; i++) {
				objectNames = objectNames + " " + objectNameList[i].ToString();		
			}
		}
	}

	void DeleteObjPopWindow(int windowID)
	{

		float onePieceOfWidth = LayoutAndStrings.windowRect.width / 5.0f;
		float onePieceOfHeight = LayoutAndStrings.windowRect.height / 12.0f;

		focusScrollViewPos = GUI.BeginScrollView (LayoutAndStrings.scrollViewRect , focusScrollViewPos, new Rect(0, 0, 300, 3000));
		GUI.backgroundColor = Color.red;
		GUI.color = Color.yellow;
		//display every object as a toggle
	
		int objectNum = objectNameList.Count;
		for (int i = 0; i < objectNum; i++) {
			objectNameBool[i] = GUI.Toggle(new Rect(10, 30*i, 250, 28), objectNameBool[i], objectNameList[i].ToString());
		}
		GUI.EndScrollView ();

		if(GUI.Button(new Rect(LayoutAndStrings.windowRect.width - onePieceOfWidth - 20.0f, LayoutAndStrings.windowRect.height - onePieceOfHeight - 10.0f, onePieceOfWidth, onePieceOfHeight), "DELETE"))//A 2D Rectangle defined by x, y position and width, height
		{	
			int index;
			while((index = objectNameBool.IndexOf(true)) > -1 )	//if found some object to delete
			{
				objectNameBool.RemoveAt(index);
				objName = Status.ObjectNames[index].ToString();
				Status.RemoveObjectFromList(objName);
				GameObject gObj = GameObject.Find(objName);
				Destroy(gObj);
			}

			//update the objectNames
	
		}
	}

	public void FocusToPoint(Vector3 point){
		Vector3 direction = point - transform.position;
		transform.LookAt (direction);
	}


	void FocusPopWindow(int windowID)
	{
		
		float onePieceOfWidth = LayoutAndStrings.windowRect.width / 5.0f;
		float onePieceOfHeight = LayoutAndStrings.windowRect.height / 12.0f;
		
		focusScrollViewPos = GUI.BeginScrollView (LayoutAndStrings.scrollViewRect , focusScrollViewPos, new Rect(0, 0, 300, 3000));
		GUI.backgroundColor = Color.red;
		GUI.color = Color.yellow;
		//display every object as a toggle
		int objectNum = objectNameList.Count;
		for (int i = 0; i < objectNum; i++) {
			objectNameBool[i] = GUI.Toggle(new Rect(10, 30*i, 250, 28), objectNameBool[i], objectNameList[i].ToString());
		}
		GUI.EndScrollView ();

		if(GUI.Button(new Rect(LayoutAndStrings.windowRect.width - onePieceOfWidth - 20.0f, LayoutAndStrings.windowRect.height - onePieceOfHeight - 10.0f, onePieceOfWidth, onePieceOfHeight), "focus"))//A 2D Rectangle defined by x, y position and width, height
		{	
			//TODO: focus on the object
			int index;
			if((index = objectNameBool.IndexOf(true)) != -1 )	//if found some object to delete
			{
				objName = Status.ObjectNames[index].ToString();
				GameObject gObj = GameObject.Find(objName);		//find the game object
				Vector3 gObjCenter = gObj.GetComponent<MeshFilter>().mesh.bounds.center;
				FocusToPoint( gObjCenter );
			}
		}

	}



	void OnGUI()
	{

		//popup Setting window
		if(GUI.Button(LayoutAndStrings.objViewBtnRect, "Delete Obj"))
		{
			if(objPopWindowBool == false)
				objPopWindowBool = true;
			else
				objPopWindowBool = false;
		}
		if(objPopWindowBool)
		{
			objectNameList = Status.ObjectNames;
			int objectCount = objectNameList.Count;
			objectNames = "";
			for (int i=0; i<objectCount; i++) {
				objectNames = objectNames + " " + objectNameList[i].ToString();		
			}
			GUI.Window(0, LayoutAndStrings.windowRect, objPopWindow, "");
		}


		//TODO:popup delete window
		if(GUI.Button (LayoutAndStrings.deleteObjRect, "DelObj"))
		{
			if(deleteObjBool == false)
				deleteObjBool = true;
			else
				deleteObjBool = false;
		}
		if(deleteObjBool)
		{
			objectNameList = Status.ObjectNames;
			int objectCount = objectNameList.Count;
			objectNames = "";
			for (int i=0; i<objectCount; i++) {
				objectNameBool.Add(false);
				//objectNames = objectNames + " " + objectNameList[i].ToString();		
			}
			
			GUI.Window(0, LayoutAndStrings.windowRect, DeleteObjPopWindow, "delete object");
		}


		//TODO:popup focus window
		if(GUI.Button(LayoutAndStrings.focusOnObjRect, "focusObj"))
		{
			if(foucsOnObjBool == false)
				foucsOnObjBool = true;
			else
				foucsOnObjBool = false;
		}
		if(foucsOnObjBool)
		{
			objectNameList = Status.ObjectNames;
			int objectCount = objectNameList.Count;
			objectNames = "";
			for (int i=0; i<objectCount; i++) {
				objectNameBool.Add(false);
			}
			

			GUI.Window(0, LayoutAndStrings.windowRect, FocusPopWindow, "Chose an object you want focus on");
		}



	
	}


}
