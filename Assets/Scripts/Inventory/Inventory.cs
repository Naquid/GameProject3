using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum EItem {
	Apple,
	Pear
};

public class ItemStruct
{
	public EItem itemType;
	public Texture itemTexture;
	public AudioClip soundOnPickup;
	public AudioClip soundOnUse;
	public GameObject onDragParticle;
	public GameObject onUseParticle;
	public GameObject onPickupParticle;
	public int numberOfItemsInStack;

}

public class Inventory : MonoBehaviour {

	int numberOfItems = 0;
	const int MAX_ITEMS = 6;
	public ItemStruct[] itemArray = new ItemStruct[MAX_ITEMS];
	public InventoryItemButton[] buttonScripts;

	public int CurrentSelectedItem = -1;
	public AudioClip soundUnableToPickup;

	public static Inventory myInv;

	bool particlePlaying = false;
	ParticleSystem pS;

	public bool AddItem(ItemStruct newItem, Vector3 location)
	{
		bool itemInserted = false;

		//fist look if we already have an entry of the same type
		for( int index = 0; index < MAX_ITEMS; ++index)
		{
			if( itemArray[index] != null )
			{
				if( itemArray[index].itemType == newItem.itemType )
				{
					Instantiate( newItem.onPickupParticle, location, Quaternion.identity );
					itemArray[index].numberOfItemsInStack++;
					itemInserted = true;
					break;
				}
			}
		}

		//if we can insert the item as a new index
		if ( numberOfItems < MAX_ITEMS && itemInserted == false) 
		{
			for( int index = 0; index < MAX_ITEMS; ++index)
			{
				if( itemArray[index] == null )
				{
					itemArray[index] = newItem;
					itemInserted = true;
					numberOfItems++;

					break;
				}
			}

			if(newItem.soundOnPickup != null)
			{
				AudioSource.PlayClipAtPoint( newItem.soundOnPickup, location );
			}

			if(newItem.onPickupParticle != null)
			{
				Instantiate( newItem.onPickupParticle, location, Quaternion.identity );
			}

		}
		else
		{
			AudioSource.PlayClipAtPoint( soundUnableToPickup, location );
		}

		SortArray();

		GetAllButtons();

		return itemInserted;
	}

	bool RemoveItem(int index)
	{
		if (itemArray[index].numberOfItemsInStack > 1) {
			itemArray[index].numberOfItemsInStack--;
		} 
		else
		{
			itemArray[index] = null;
			--numberOfItems;
		}

		SortArray();

		GetAllButtons();

		return true;
	}

	public bool GetItemFromIndex(int index, ref ItemStruct itemRef )
	{
		if ( itemArray [index] != null ) 
		{
			itemRef = itemArray [index];

			return true;
		}

		else 
		{

			return false;
		}

	}

	void Awake () 
	{
		if (!myInv) {
			myInv = this;
			DontDestroyOnLoad (gameObject);

		} else
		{
			Destroy (gameObject);
		}
	}

	void GetAllButtons()
	{
		buttonScripts = GameObject.FindObjectsOfType<InventoryItemButton> ();
	}

	public void SelectItem(int index)
	{
		if (itemArray [index] != null) 
		{
			CurrentSelectedItem = index;

			if ( itemArray [CurrentSelectedItem].onDragParticle != null && particlePlaying == false) 
			{
				StartDragParticle();
			}

		}
	}

	public void DeselectItem( )
	{

		CurrentSelectedItem = -1;
		StopDragParticle ();

	}

	public bool CheckIfItemExist( int index )
	{
		if (itemArray [index] != null) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	public void TryUseItemOnActor(GameObject actorToUseOn, int itemToUse )
	{
		
		if( itemArray[itemToUse] != null )
		{
			IUseItem canUse = actorToUseOn.GetComponent<IUseItem> ();

			if (canUse != null) 
			{
				bool itemUsed = canUse.UseItemOnObject ( itemArray[itemToUse].itemType );

				if(itemArray[itemToUse].soundOnUse != null)
				{
					AudioSource.PlayClipAtPoint( itemArray[itemToUse].soundOnUse, Vector3.zero );
				}

				if(itemArray[itemToUse].onUseParticle != null)
				{
					Instantiate( itemArray[itemToUse].onUseParticle , actorToUseOn.transform.position, Quaternion.identity );
				}

				if (itemUsed)
				{

					RemoveItem(itemToUse);

					GetAllButtons ();

				}
		
			}

		}

		//stop particle always
		if (particlePlaying == true)
		{
			StopDragParticle();
		}

		CurrentSelectedItem = -1;

	}

	void StartDragParticle()
	{

		Ray location = Camera.main.ScreenPointToRay (Input.mousePosition);

		GameObject particleObject = (GameObject)Instantiate( itemArray [CurrentSelectedItem].onDragParticle, location.GetPoint (3.0f) , Quaternion.identity );
		pS = particleObject.gameObject.GetComponent<ParticleSystem> ();

		particlePlaying = true;

	}

	void StopDragParticle()
	{
		if (pS != null) 
		{
			pS.enableEmission = false;
			pS.Stop ();
		}

		particlePlaying = false;
	}
	

	void Update()
	{
		if (particlePlaying == true)
		{

			Ray location = Camera.main.ScreenPointToRay (Input.mousePosition);

			pS.transform.position = location.GetPoint(3.0f);
		}

	}

	void SortArray()
	{
		for ( int x = 0; x < MAX_ITEMS; ++x ) 
		{
			if( itemArray[x] == null)
			{
				bool itemFound = false;
				int index = 0;

				for(int y = x; y < MAX_ITEMS; y++)
				{
					if( itemArray[y] != null)
					{
						index = y;
						itemFound = true;
						break;
					}
				}

				if(itemFound == true)
				{
					itemArray[x] = itemArray[index];
					itemArray[index] = null;
				}

			}
		}
	}

}


