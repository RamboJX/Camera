using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FbxFileBtn : MonoBehaviour {
	public Button button;
	public Text name;
	public Text isLoaded;


	//will set this envent to the btnInfo::loadFbxFile variable;
	public void LoadFbxFile(){
		Debug.Log ("I will load file:" + name.text);
		string ObjectsPathURL = "file://" + name.text;
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
