using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FbxFileBtn : MonoBehaviour {
	public Button button;
	public Text name;
	public Text isLoaded;


	//will set this envent to the btnInfo::loadFbxFile variable;
	public void LoadFbxFile(){
		string ObjectsPathURL = "";
		if (Application.platform == RuntimePlatform.Android) {
			ObjectsPathURL = "jar:file://" + name.text;
		}
		else{
			ObjectsPathURL = "file://" + name.text;
		}

		GameObject.FindGameObjectWithTag("SystemControl").GetComponent<SystemControl>().DisplayDebugInfo("load gameobject path:" + ObjectsPathURL);
		StartCoroutine(LoadGameObject(ObjectsPathURL));
	}


	private IEnumerator LoadGameObject(string path)
	{
		WWW bundle = new WWW(path);
		//WWW bundle = WWW.LoadFromCacheOrDownload (path, 4);
		yield return bundle;
		yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);
	}
}
