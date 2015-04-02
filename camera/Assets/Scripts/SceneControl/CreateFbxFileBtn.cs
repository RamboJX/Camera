using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//[System.Serializable]
public class btnInfo{
	public string fileName;
	public bool isLoaded;
//	public Button.ButtonClickedEvent loadFbxFile;
}

public class CreateFbxFileBtn : MonoBehaviour {
	public GameObject fbxFileBtn;
	private List<btnInfo> itemList;
	public Transform fbxFileListScrollPanel;
	public static string fbxFilesDir;

	void Awake(){
		itemList = new List<btnInfo> ();
	}

	void Start(){
	}

	//this function is called everytime the panel is actived.
	void OnEnable(){
		FindFbxFilesInDirectory ();
		ClearOldButtons ();
		CreateButtonsInPanel ();

	}
	void OnDisable(){
		itemList.Clear ();
	}

	void FindFbxFilesInDirectory(){
	//	#if UNITY_ANDROID
		//string[] fbxFiles = Directory.GetFiles("/sdcard" + "/StreamingAssets/", "*.assetbundle");
	//	#endif

	//	#if UNITY_EDITOR_WIN
	//	string[] fbxFiles = Directory.GetFiles(Application.dataPath + "/StreamingAssets/", "*.assetbundle");
	//	string[] fbxFiles = Directory.GetFiles("/SDCard/FbxBundles/", "*.assetbundle");
	//	#endif

		//if in android environment
		string[] fbxFiles = System.IO.Directory.GetFiles("/sdcard/scene/", "*.assetbundle");

		for (int i = 0; i < fbxFiles.Length; i++) {
			btnInfo temp = new btnInfo();
			temp.fileName = fbxFiles[i];
			temp.isLoaded = false;
			itemList.Add(temp);
		}
	}

	void ClearOldButtons(){
		int i = fbxFileListScrollPanel.childCount;
		//delete all the child of the fbxfilelistscrollpanel
		for (int j = 0; j < i; j++) {
			GameObject.Destroy( fbxFileListScrollPanel.GetChild(j).gameObject);
		}


	}

	//This function is list the files 
	void CreateButtonsInPanel(){
		foreach (var item in itemList) {
			GameObject newFbxFileBtn = Instantiate(fbxFileBtn) as GameObject;	
			FbxFileBtn fbxBtn = newFbxFileBtn.GetComponent<FbxFileBtn>();
			fbxBtn.name.text = item.fileName;
			if(item.isLoaded){
				fbxBtn.isLoaded.text = "Y";
			}
			else{
				fbxBtn.isLoaded.text = "N";
			}
			// add an function as button click event
			fbxBtn.button.onClick.AddListener(delegate {
				fbxBtn.LoadFbxFile();
		});
			//set this new button to be the child of panel
			newFbxFileBtn.transform.SetParent(fbxFileListScrollPanel);
		}
	}
}
