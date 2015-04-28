using UnityEngine;
using System.Collections;

public class BaseParticle : MonoBehaviour {

	ParticleSystem pS;


	// Use this for initialization
	void Start () {
		pS = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (pS.isStopped)
		{
			Destroy(gameObject);
		}

	}
}
