using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
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
		string[] fbxFiles = Directory.GetFiles(Application.dataPath + "/StreamingAssets/", "*.assetbundle");
	//	string[] fbxFiles = Directory.GetFiles("/SDCard/FbxBundles/", "*.assetbundle");

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
				LoadFbxFile(item.fileName);
		});
			//set this new button to be the child of panel
			newFbxFileBtn.transform.SetParent(fbxFileListScrollPanel);
		}
	}

	//will set this envent to the btnInfo::loadFbxFile variable;
	public void LoadFbxFile(string fileName){
		Debug.Log ("I will load file:" + fileName);
		string ObjectsPathURL = "file://" + fileName;
		StartCoroutine(LoadGameObject(ObjectsPathURL));

	}

	private IEnumerator LoadGameObject(string path)
	{
		print (path);
		WWW bundle = new WWW(path);
		yield return bundle;
		yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);
	}
}
