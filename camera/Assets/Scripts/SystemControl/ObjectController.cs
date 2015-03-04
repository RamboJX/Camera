using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectInfo{
	public string objectName;
	public bool isLoaded;
	//	public Button.ButtonClickedEvent loadFbxFile;
}

public class ObjectController : MonoBehaviour {

	private List<ObjectInfo> objectList;
	public GameObject objectToggle;
	public Transform objectScrollPanel;

	void Awake(){
		objectList = new List<ObjectInfo> ();
	}

	void OnEnable(){
		PopulateObjectList ();
		CreateToggleInPanel ();
		Debug.Log("I am here");
	}

	//find all the gameobject in the scene which has mesh
	void PopulateObjectList(){
		Debug.Log("I am here 2");
		Object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach(Object loadedObj in obj){
			GameObject temp = (GameObject) loadedObj;
			if(temp.GetComponent<MeshFilter>() != null){

				//add this object to List
				ObjectInfo tmpInfo = new ObjectInfo();
				tmpInfo.objectName = temp.name;
				tmpInfo.isLoaded = true;
				objectList.Add(tmpInfo);
			
				Debug.Log(temp.name);
			}
		}
	}
	//This function is list the files 
	void CreateToggleInPanel(){
		foreach (var item in objectList) {

		//	Debug.Log(item.objectName);
			GameObject newObjToggle = Instantiate(objectToggle) as GameObject;	
			ObjectToggle objToggle = newObjToggle.GetComponent<ObjectToggle>();
			objToggle.name.text = item.objectName;
			if(item.isLoaded){
				objToggle.thisToggle.isOn = true;
			}
			else{
				objToggle.thisToggle.isOn = false;
			}
			// add an function as button click event
		//	fbxBtn.button.onClick.AddListener(delegate {
		//		fbxBtn.LoadFbxFile();
		//	});
			//set this new button to be the child of panel
			newObjToggle.transform.SetParent(objectScrollPanel);
		}
	}
}
