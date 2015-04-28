using UnityEngine;
using System.Collections;

public enum EPlayerState {
	WalkToLocation,
	UseItemOnObject,
	TalkToObject,
	Idle
};

public class NavMeshMovement : MonoBehaviour {

	public Transform target;
	public GameObject wayPointPrefab;
	NavMeshAgent agent;

	private GameObject wayPoint;
	private Vector3 clickedPosition;

	EPlayerState currentPlayerState = EPlayerState.WalkToLocation;

	//walk to location
	Vector3 locationToReach = Vector3.zero;

	//Use Item at Location
	int indexOfItem;
	float distanceItemCanBeUsed = 4.0f;
	GameObject objectToUseItemOn;

	//talk to object
	GameObject objectToTalkTo;
	float distanceItenCanBeTalkedTo = 4.0f;


	void Start () {
	
		agent = GetComponent<NavMeshAgent> ();
		clickedPosition = transform.position;

	}
	
	void Update () 
	{
		//Debug.Log (agent.remainingDistance);
		float distanceToActor = 0.0f;

		switch (currentPlayerState)
		{
			case EPlayerState.Idle :

			break;
			case EPlayerState.WalkToLocation :
				agent.SetDestination (locationToReach);
			break;

			case EPlayerState.UseItemOnObject :
				agent.SetDestination ( objectToUseItemOn.transform.position );
				
				distanceToActor = (objectToUseItemOn.transform.position - transform.position).magnitude;

				if( distanceToActor <= distanceItemCanBeUsed )
				{
					Inventory.myInv.TryUseItemOnActor(objectToUseItemOn, indexOfItem);
					agent.ResetPath();
					TrySetIdleState();
					
				}
			break;
			case EPlayerState.TalkToObject :
				agent.SetDestination ( objectToTalkTo.transform.position );
				
				distanceToActor = (objectToTalkTo.transform.position - transform.position).magnitude;
			
				if( distanceToActor <= distanceItenCanBeTalkedTo )
				{
					ISpeech canTalk = objectToTalkTo.GetComponent<ISpeech> ();
					
					if (canTalk != null)
					{
						canTalk.OnTalkTo();
					}
					agent.ResetPath();
					TrySetIdleState();
					
				}
			break;


			default :
			break;
		}


			/*if (Inventory.myInv.CurrentSelectedItem == -1) {
				if (wayPoint != null) {
					agent.SetDestination (wayPoint.transform.position);

				} 

				if (Input.GetMouseButtonDown (0)) {
					if (!EventSystem.current.IsPointerOverGameObject ()) {
						locatePositionDown ();
					}
				}
			*/


	}


	public void TrySetMoveToLocationState( Vector3 location )
	{
		switch (currentPlayerState)
		{
			case EPlayerState.Idle :
			case EPlayerState.TalkToObject :
			case EPlayerState.UseItemOnObject :
			case EPlayerState.WalkToLocation :
				//Debug.Log ("WalkToLocation");
				currentPlayerState = EPlayerState.WalkToLocation;
				locationToReach = location;
			break;
				
			default :
			break;
		}
	}

	public void TrySetIdleState( )
	{
		switch (currentPlayerState)
		{
			case EPlayerState.Idle :
			case EPlayerState.TalkToObject :
			case EPlayerState.UseItemOnObject :
			case EPlayerState.WalkToLocation :
				//Debug.Log ("Idle");
				currentPlayerState = EPlayerState.Idle;
			break;
				
			default :
			break;
		}
	}

	public void TrySetUseItemOnObject( int indexOfItem, GameObject actorToUseOn)
	{
		switch (currentPlayerState)
		{
			case EPlayerState.Idle :
			case EPlayerState.TalkToObject :
			case EPlayerState.UseItemOnObject :
			case EPlayerState.WalkToLocation :
				currentPlayerState = EPlayerState.UseItemOnObject;
				this.indexOfItem = indexOfItem;
				this.objectToUseItemOn = actorToUseOn;

				//Debug.Log ("UseItemOnObject");
				break;
			
			default :
				break;
		}
	}

	public void TrySetTalkToObject( GameObject actorToTalkTo)
	{
		switch (currentPlayerState)
		{
			case EPlayerState.Idle :
			case EPlayerState.TalkToObject :
			case EPlayerState.UseItemOnObject :
			case EPlayerState.WalkToLocation :
				currentPlayerState = EPlayerState.TalkToObject;
				this.objectToTalkTo = actorToTalkTo;
				
				//Debug.Log ("TalkToObject");
				break;
				
			default :
				break;
			}
	}

	void locatePosition (){

		Vector3 hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (NavMeshRaycast(ray, 30, out hit)) {

			clickedPosition = hit;
			
			Destroy(wayPoint);
			
			wayPoint = (GameObject) Instantiate(wayPointPrefab, clickedPosition, Quaternion.identity);

		}

	}

	void locatePositionDown (){

		Vector3 hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (NavMeshRaycast(ray, 30, out hit)) {

			clickedPosition = hit;
			
			Destroy(wayPoint);
			
			wayPoint = (GameObject) Instantiate(wayPointPrefab, clickedPosition, Quaternion.identity);

		}

	}

	bool NavMeshRaycast (Ray r, float d, out Vector3 point){

		float sampleRadius = 1f;
		int samples = Mathf.RoundToInt (d / (sampleRadius * 2));
		for (int i = 0; i < samples; i++) {

			NavMeshHit hit;
			Vector3 p = r.GetPoint(sampleRadius * 2 * i);
			bool didHit = NavMesh.SamplePosition(p, out hit, sampleRadius, NavMesh.AllAreas);

			if(didHit){
				point = hit.position;
				return true;
			}

		}

		point = Vector3.zero;
		return false;


	}

	void OnTriggerEnter (Collider col){
		if (col.gameObject.tag == "WayPoint") {
			Destroy(wayPoint);
		}
	}

}
