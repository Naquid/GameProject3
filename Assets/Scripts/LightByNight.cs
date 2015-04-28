using UnityEngine;
using System.Collections;

public class LightByNight : MonoBehaviour {

	public DayNightCycle DNCscript;
	public float pickedIntensity;
	private Light attachedLight;

	void Start () {

		attachedLight = gameObject.GetComponent<Light> ();
		DNCscript = GameObject.Find ("DayNightCycle").GetComponent<DayNightCycle> ();
		pickedIntensity = attachedLight.intensity;
	}

	// Update is called once per frame
	void Update () {
	
		if (DNCscript.isNight == true) {
			attachedLight.intensity = pickedIntensity;
		} 
		if (DNCscript.isNight == false) {
			attachedLight.intensity = 0;
		} 
	}
}
