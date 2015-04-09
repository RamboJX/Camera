using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pathes(){
		GameObject.Find("SystemControl").GetComponent<SystemControl>().DisplayDebugInfo("Application PersistentPath:" + Application.persistentDataPath
		                                                                                +"  Application DataPath:" + Application.dataPath
		                                                                                +"  Application stream AssetsPath:" + Application.streamingAssetsPath
		                                                                                );
	//	GameObject.Find("SystemControl").GetComponent<SystemControl>().DisplayDebugInfo("Application DataPath:" + Application.dataPath);
	
	}
}
