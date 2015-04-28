using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public EItem itemType;
	public Texture itemTexture;
	public AudioClip soundOnPickup;
	public AudioClip soundOnUse;
	public GameObject onDragParticle;
	public GameObject onUseParticle;
	public GameObject onPickupParticle;
	public int numberOfItemsInStack = 1;


	public ItemStruct itemData = new ItemStruct();


	// Use this for initialization
	public void Start () {

		itemData.itemTexture = itemTexture;
		itemData.itemType = itemType;
		itemData.soundOnPickup = soundOnPickup;
		itemData.soundOnUse = soundOnUse;
		itemData.numberOfItemsInStack = numberOfItemsInStack;
		itemData.onDragParticle = onDragParticle;
		itemData.onUseParticle = onUseParticle;
		itemData.onPickupParticle = onPickupParticle;

	}

	void OnTriggerEnter (Collider col )
	{
		if ( col.CompareTag("Player") )
		{

			if (Inventory.myInv.AddItem (itemData, transform.position))
			{
				Destroy (gameObject);
			} 
			else
			{
				//error message
			}
		}
	}
}
