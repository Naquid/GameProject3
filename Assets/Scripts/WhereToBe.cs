using UnityEngine;
using System.Collections;

public class WhereToBe : MonoBehaviour {

	public DayNightCycle DNC;
	NavMeshAgent agent;
	public Transform nightTimeTransform;
	public Transform dayTimeTransform;

	void Start () {
	
		agent = GetComponent<NavMeshAgent> ();
		DNC = GameObject.Find ("DayNightCycle").GetComponent<DayNightCycle>();
		nightTimeTransform = gameObject.transform.GetChild(0);
		dayTimeTransform = gameObject.transform.GetChild(1);
		transform.DetachChildren ();

	}
	

	void Update () {
	
		if (DNC.isNight == true && gameObject.GetComponent<MeshRenderer>().isVisible == false) {
			agent.SetDestination(nightTimeTransform.position);

		} 

		if (DNC.isNight == false && gameObject.GetComponent<MeshRenderer>().isVisible == false) {
			agent.SetDestination(dayTimeTransform.position);
		}

	}
}