using UnityEngine;
using System.Collections;

public class SceneBtn : MonoBehaviour {

	
	public GameObject scenePanel;
	
	void Awake(){
		
	}

	public void SwitchPanelStatus()
	{
		if (scenePanel.activeSelf)
			scenePanel.SetActive (false);
		else
			scenePanel.SetActive (true);
		
	}
}
