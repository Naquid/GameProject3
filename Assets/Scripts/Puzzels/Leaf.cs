using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour, IClickAbleInterface
{

	Rigidbody rigiBody;
	float forceMultiplier = 25.0f;

	float currentTime = 0.0f;
	float maxTime = 0.2f;


	// Use this for initialization
	void Start () {

		rigiBody = GetComponent<Rigidbody> ();


	}
	
	// Update is called once per frame
	void Update ()
	{

		if (currentTime > 0.0f)
		{
			currentTime -= Time.deltaTime;
		}


	}

	public void OnClick()
	{

	}

	public void OnClickRelease( )
	{


	}

	public void OnDragOver( Vector3 deltaMousePosition )
	{
		//Debug.Log ("Force Added" + deltaMousePosition);
		if (currentTime <= 0.0f) 
		{
			currentTime = maxTime;
			AddForce ((deltaMousePosition * Time.deltaTime) * forceMultiplier);
		}

	}

	void AddForce( Vector3 forceToAdd)
	{

		rigiBody.AddForce ( forceToAdd );

	}
}
