using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItemButton : MonoBehaviour {

	public int index;
	RawImage imageComp;
	Text textComp;

	Animator anim;

	bool isDragged = false;
	int animMouseOverHash = Animator.StringToHash("isMouseOver");



	public void OnClick()
	{



	}

	public void SetIndex(int newIndex)
	{
		index = newIndex;
	}

	public void Update()
	{

		ItemStruct itemData = new ItemStruct ();
		
		if ( Inventory.myInv.GetItemFromIndex (index, ref itemData) ) 
		{
			//dont show picture if we are dragging the current item
			if(Inventory.myInv.CurrentSelectedItem != index)
			{
				imageComp.enabled = true;
			}

			imageComp.texture = itemData.itemTexture;

			if( itemData.numberOfItemsInStack > 1)
			{
				textComp.text = itemData.numberOfItemsInStack.ToString();
			}
			else
			{
				textComp.text = "";
			}
		} 
		else
		{
			imageComp.enabled = false;
			textComp.text = "";
		}

	}

	void OnGUI()
	{
		
		if (isDragged && Inventory.myInv.itemArray [index] != null) 
		{
			
			float sizeX = 100.0f;
			float sizeY = 100.0f;
			
			float positionX = Input.mousePosition.x - (sizeX / 2);
			float postitonY = (Screen.height - Input.mousePosition.y) - (sizeY / 2);
			
			Rect dragRect = new Rect (positionX, postitonY, sizeX, sizeY); 

			GUI.DrawTexture (dragRect, Inventory.myInv.itemArray [index].itemTexture);
			
		}
		
	}
	
	void Awake()
	{

		imageComp = GetComponentInChildren<RawImage> ();
		textComp = GetComponentInChildren<Text> ();
		anim = GetComponentInChildren<Animator> ();

	}

	public void OnMouseOver()
	{
		if (Inventory.myInv.itemArray [index] != null) 
		{
			anim.SetBool (animMouseOverHash, true);
		}
	}

	public void OnMouseLeaving()
	{
		if (!isDragged) 
		{
			anim.SetBool (animMouseOverHash, false);
		}
	}

	public void OnBeginDrag()
	{
		if (Inventory.myInv.itemArray [index] != null) 
		{
			anim.SetBool (animMouseOverHash, true);
			isDragged = true;
			Inventory.myInv.SelectItem (index);
			imageComp.enabled = false;
		}

	}

	public void OnEndDrag()
	{
		anim.SetBool (animMouseOverHash, false);
		isDragged = false;

		//make sure we have an item before enable again
		if (Inventory.myInv.CheckIfItemExist (index)) 
		{
			imageComp.enabled = true;
		}

	}




}
