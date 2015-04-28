using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryOpenButton : MonoBehaviour {

	GameObject panel;

	bool active = false;

	// Use this for initialization
	void Start () {

		panel = GameObject.FindGameObjectWithTag("MainPanel");

		panel.SetActive (false);
	
	}

	public void OnClick()
	{

		if (active) 
		{
			panel.SetActive (false);
			active = false;

		} 
		else 
		{
			panel.SetActive (true);
			active = true;
		
		}

	}

	void OnGUI()
	{
		GUI.enabled = false;
	}

	public void OnPointerEnter()
	{

	}

	public void OnPointerLeaving()
	{

	}

}
