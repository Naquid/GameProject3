using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform followTarget;

	float cameraDistanceMax = 15f;
	float cameraDistanceMin = 1f;
	float cameraDistance = 5f;
	float scrollSpeed = 10f;

	void LateUpdate () {
		transform.position = new Vector3 (followTarget.position.x, followTarget.position.y + cameraDistance, followTarget.position.z - 7);
	
		cameraDistance += Input.GetAxis ("Mouse ScrollWheel") * -scrollSpeed;
		cameraDistance = Mathf.Clamp (cameraDistance, cameraDistanceMin, cameraDistanceMax);

		transform.LookAt (followTarget);

		if(Input.GetKey(KeyCode.Space)){

			transform.position = followTarget.position;
			transform.rotation = followTarget.rotation;

		}
	
	}
}
