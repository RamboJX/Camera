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
			ObjectsPathURL = "file:///mnt" + name.text;
		}
		else{
			ObjectsPathURL = "file://" + name.text;
		}

		GameObject.FindGameObjectWithTag("SystemControl").GetComponent<SystemControl>().DisplayDebugInfo("load gameobject path:" + ObjectsPathURL);
		StartCoroutine(LoadGameObject(ObjectsPathURL));
	}

	/*
	private IEnumerator LoadGameObject(string path)
	{
		//WWW bundle = new WWW(path);
		WWW www = WWW.LoadFromCacheOrDownload (path, 4);
		yield return www;
		if (www.error != null) {
			throw new UnityException("bundle download had an error" + bundle.error);		
		}
		yield return Instantiate(www.assetBundle.mainAsset);
		www.assetBundle.Unload(false);
	}
	*/

	IEnumerator LoadGameObject (string path){
		// Wait for the Caching system to be ready
		while (!Caching.ready)
			yield return null;
		//For android (assuming there is an asset bundle called Cube.unity3d under /sdcard/
		string bundleUrl = path;
		// Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
		using(WWW www = WWW.LoadFromCacheOrDownload (bundleUrl, 4)){
			yield return www;
			if (www.error != null)
				throw new UnityException("WWW download had an error:" + www.error);
			AssetBundle bundle = www.assetBundle;
			

		//	if (AssetName == "")		//this can choose which asset to load . it is very useful, but right now it is no use.
			yield return (GameObject)Instantiate(bundle.mainAsset);
		//	else
		//		go = (GameObject)Instantiate(bundle.Load(AssetName));
			//TODO: right now I just want this be the parent, if I can not load , I will try to make it be a child of some object
			//optional code to re-parent the assetBundle
			//and set the local positioning
			/*
			if (go != null) {
				go.transform.parent = mTransform;
				go.transform.localPosition = new Vector3(0, 0, 0);//set your desired local position here
				go.transform.localRotation = Quaternion.identity;//set your desired local rotation here
				go.transform.localScale = new Vector3(1,1,1);//set your desired local scale here
			}
			*/
			// Unload the AssetBundles compressed contents to conserve memory			
			bundle.Unload(false);
		}
	}
}





/*
public class CachingLoader : MonoBehaviour {
	
	public string AssetName;
	public int version;
	private Transform mTransform;
	void Start() {
		mTransform = GetComponent<Transform>();
		StartCoroutine (DownloadAndCache());
	}

	IEnumerator DownloadAndCache (){
		// Wait for the Caching system to be ready
		while (!Caching.ready)
			yield return null;
		//For android (assuming there is an asset bundle called Cube.unity3d under /sdcard/
		string bundleUrl = "file:///mnt/sdcard/Cube.unity3d";
		// Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
		using(WWW www = WWW.LoadFromCacheOrDownload (bundleUrl, version)){
			yield return www;
			if (www.error != null)
				throw new UnityException("WWW download had an error:" + www.error);
			AssetBundle bundle = www.assetBundle;

			GameObject go = null;
			if (AssetName == "")
				go = (GameObject)Instantiate(bundle.mainAsset);
			else
				go = (GameObject)Instantiate(bundle.Load(AssetName));

			//optional code to re-parent the assetBundle
			//and set the local positioning
			if (go != null) {
				go.transform.parent = mTransform;
				go.transform.localPosition = new Vector3(0, 0, 0);//set your desired local position here
				go.transform.localRotation = Quaternion.identity;//set your desired local rotation here
				go.transform.localScale = new Vector3(1,1,1);//set your desired local scale here
			}

			// Unload the AssetBundles compressed contents to conserve memory			
			bundle.Unload(false);
			
		}
		
	}
	
}
*/