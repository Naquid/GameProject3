using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class CreateInventory : MonoBehaviour {
	
	public GameObject buttonPrefab;

	// Use this for initialization
	void Start () {

		int length = Inventory.myInv.itemArray.Length;

		for (int x = 0; x < length; ++x) 
		{
			GameObject newButton = (GameObject) Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
			InventoryItemButton buttonScript = newButton.GetComponent<InventoryItemButton>();

			newButton.transform.SetParent(gameObject.transform);
			newButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			newButton.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
			newButton.transform.rotation = new Quaternion(0.0f,0.0f,0.0f,0.0f);
			buttonScript.SetIndex(x);

		}

	}


}
