using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Threading;


public class GameObjectImportBtn : MonoBehaviour {

	public Text fbxFilePath;
	private string ObjectsPathURL ;


	void Awake(){
	
	}

	private IEnumerator LoadGameObject(string path)
	{
		print (path);
		WWW bundle = new WWW(path);
		yield return bundle;
		yield return Instantiate(bundle.assetBundle.mainAsset);
		bundle.assetBundle.Unload(false);
	}

	public void LoadGameObjectsFromFile(){
		//ObjectsPathURL = "E:\\VirtualCamera\\camera\\Assets\\StreamingAssets\\" + fbxFilePath.text;

		//test on windows
		ObjectsPathURL = "file://" + Application.dataPath + "/StreamingAssets/" + "obj3fortest.assetbundle";
		//print (ObjectsPathURL + "obj3fortest.assetbundle");
		StartCoroutine(LoadGameObject(ObjectsPathURL));
	}
}
