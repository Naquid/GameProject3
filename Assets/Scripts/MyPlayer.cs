using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MyPlayer : MonoBehaviour 
{

	GameObject selectedObject;

	bool mouseClicked = false;
	Vector3 mouseLocationPrevFrame;

	NavMeshMovement movementScript;

	void Start () 
	{
		movementScript = GetComponentInChildren<NavMeshMovement>();
	}

	void Update () 
	{
		//on left mouse click
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject() ) 
		{
			RaycastHit hitInfo = new RaycastHit ();
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			bool traceHit = Physics.Raycast (mouseRay, out hitInfo);

			if ( Inventory.myInv.CurrentSelectedItem == -1 && traceHit )
			{
				selectedObject = hitInfo.collider.gameObject;
				IClickAbleInterface canClick = selectedObject.GetComponent<IClickAbleInterface> ();
				ISpeech canTalk = selectedObject.GetComponent<ISpeech> ();

				if (canClick != null)
				{
					canClick.OnClick ();
				}

				if (canTalk != null)
				{
					movementScript.TrySetTalkToObject(selectedObject);
				}

				//check if we are holding over ha UI elemetn
				else if ( !EventSystem.current.IsPointerOverGameObject() )
				{
					if( hitInfo.collider.gameObject.CompareTag("Actor") == false && hitInfo.collider.gameObject.CompareTag("Player") == false )
					{
						movementScript.TrySetMoveToLocationState(hitInfo.point);
					}
				}
			}

			mouseClicked = true;
			mouseLocationPrevFrame = Input.mousePosition;

		}
		//for dragging
		else if (Input.GetMouseButton (0) && mouseClicked) 
		{

			RaycastHit hitInfo = new RaycastHit ();
			Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Inventory.myInv.CurrentSelectedItem == -1 && Physics.Raycast (mouseRay, out hitInfo)) {
				GameObject hitObject = hitInfo.collider.gameObject;
				IClickAbleInterface canClick = hitObject.GetComponent<IClickAbleInterface> ();
				
				if (canClick != null) {

					Vector3 mouseLocation = Input.mousePosition;
					Vector3 deltaMouseLocation = mouseLocation - mouseLocationPrevFrame;

					canClick.OnDragOver( deltaMouseLocation );
				}
			}
			
			mouseClicked = true;
			mouseLocationPrevFrame = Input.mousePosition;

		}

		//when releasing mouse
		else if( Input.GetMouseButtonUp (0) )
		{

			if ( Inventory.myInv.CurrentSelectedItem == -1 && selectedObject != null )
			{

				IClickAbleInterface canClick = selectedObject.GetComponentInChildren<IClickAbleInterface> ();

				if (canClick != null) 
				{
					canClick.OnClickRelease ();
				}

				selectedObject = null;
			}

			else if( Inventory.myInv.CurrentSelectedItem != -1 )
			{

				RaycastHit hitInfo = new RaycastHit();
				Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if ( Physics.Raycast (mouseRay, out hitInfo) )
				{

					GameObject hitActor = hitInfo.collider.gameObject;

					IUseItem canUseItem = hitActor.GetComponent<IUseItem> ();

					if(canUseItem != null)
					{
						movementScript.TrySetUseItemOnObject( Inventory.myInv.CurrentSelectedItem,  hitActor );
					}
				}

				Inventory.myInv.DeselectItem();

			}

			mouseClicked = false;

		}
	}
}
