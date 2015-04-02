using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FbxFileBtn : MonoBehaviour {
	public Button button;
	public Text name;
	public Text isLoaded;
//	public GameObject SystemController;


	//will set this envent to the btnInfo::loadFbxFile variable;
	public void LoadFbxFile(){
		//Debug.Log ("I will load file:" + name.text);
		//in windows
		//string ObjectsPathURL = "file://" + name.text;
		////in android
		string ObjectsPathURL = "file://" + name.text;

		GameObject.Find ("SystemControl").GetComponent<SystemControl> ().DisplayDebugInfo (ObjectsPathURL + "____This scene to load");
//		SystemController.GetComponent<SystemControl>().DisplayDebugInfo(ObjectsPathURL + "____This scene to load");


		StartCoroutine(LoadGameObject(ObjectsPathURL));
	}


	private IEnumerator LoadGameObject(string path)
	{
		//print (path);
	//	WWW bundle = new WWW(path);
	

		WWW bundle = WWW.LoadFromCacheOrDownload (path, 4);
		if (!bundle.isDone) {
			GameObject.Find ("SystemControl").GetComponent<SystemControl> ().DisplayDebugInfo (path + "load error");
		}
		yield return bundle;
		// Instantiate(object orginal, vector3 position, Quaternion rotation);

		yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);
	}
}
