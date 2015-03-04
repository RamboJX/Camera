using UnityEngine;
using System.Collections;

public class ObjectCtrlBtn : MonoBehaviour {

	public GameObject ObjectCtrlPanel;
	
	void Awake(){
		
	}
	
	public void SwitchPanelStatus()
	{
		if (ObjectCtrlPanel.activeSelf)
			ObjectCtrlPanel.SetActive (false);
		else
			ObjectCtrlPanel.SetActive (true);
		
	}
}
