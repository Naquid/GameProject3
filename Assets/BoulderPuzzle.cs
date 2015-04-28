using UnityEngine;
using System.Collections;

public class BoulderPuzzle : MonoBehaviour, IClickAbleInterface
{
	bool followMouse = false;
	

	// Update is called once per frame
	void Update () 
	{
		if (followMouse) 
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane newPlane = new Plane( new Vector3(0.0f, 0.0f, -1.0f)  ,transform.position );
			float dist = 0.0f;
			                          
			newPlane.Raycast(mouseRay, out dist);

			transform.position = mouseRay.GetPoint(dist);


		}
	}

	public void OnClick()
	{
		//Debug.Log ("Hej");
		followMouse = true;
	}

	public void OnClickRelease()
	{
		//Debug.Log ("Blää");
		followMouse = false;
	}

	public void OnDragOver( Vector3 deltaMousePosition )
	{


	}

}
