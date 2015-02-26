using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingBtn : MonoBehaviour {

	public GameObject settingPanel;

	void Awake(){

	}

	public void SwitchPanelStatus()
	{
		if (settingPanel.activeSelf)
						settingPanel.SetActive (false);
				else
						settingPanel.SetActive (true);
		
	}
}
