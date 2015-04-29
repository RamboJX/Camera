using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ObjectInfo{
	public string objectName;
	public bool isSelected;
	//	public Button.ButtonClickedEvent loadFbxFile;
}

public class ObjectController : MonoBehaviour {

	private List<ObjectInfo> objectList;
	public GameObject objectToggle;
	public GameObject objectScrollPanel;
	public GameObject camera;

	void Awake(){
		objectList = new List<ObjectInfo> ();
	}

	void OnEnable(){
		PopulateObjectList ();
		CreateToggleInPanel ();
		Debug.Log("I am here");
	}

	void OnDisable(){
		objectList.Clear ();
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
				tmpInfo.isSelected = false;
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
			newObjToggle.GetComponent<Toggle>().group = objectScrollPanel.GetComponent<ToggleGroup>();
			ObjectToggle objToggle = newObjToggle.GetComponent<ObjectToggle>();
			objToggle.name.text = item.objectName;
			if(item.isSelected){
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
			newObjToggle.transform.SetParent(objectScrollPanel.transform);
		}
	}

	public void DeleteSelectedObject(){
		//if no object is selected 
		//if selected one thing
		Toggle selectedToggle = objectScrollPanel.GetComponent<ToggleGroup> ().ActiveToggles ().FirstOrDefault();

		if (selectedToggle != null) {
			string selectedObjName = selectedToggle.GetComponent<ObjectToggle>().name.text;
			GameObject.Destroy (GameObject.Find (selectedObjName));
			GameObject.Destroy (selectedToggle.gameObject);
		}
	}

	public void FocusToObject(){
		//if selected one thing
		Toggle selectedToggle = objectScrollPanel.GetComponent<ToggleGroup> ().ActiveToggles ().FirstOrDefault();
		
		if (selectedToggle != null) {
			string selectedObjName = selectedToggle.GetComponent<ObjectToggle>().name.text;
			camera.GetComponent<CameraMotion>().FocusToObject(GameObject.Find (selectedObjName));
		}
	}
}
