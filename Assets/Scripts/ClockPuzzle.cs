using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockPuzzle : MonoBehaviour {

	public InputField guessTime;
	public GameObject analogClock;
	public GameObject reward;
	public string rightAnswer1;
	public string rightAnswer2;
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			guessTime.gameObject.SetActive (true);
			analogClock.SetActive (true);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			guessTime.gameObject.SetActive (false);	
			analogClock.SetActive (false);
		}
	}

	void Update(){

		if(guessTime.text == rightAnswer1 && Input.GetKeyDown(KeyCode.Return)){
			Instantiate(reward, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
			guessTime.gameObject.SetActive (false);	
			analogClock.SetActive (false);
			Destroy(gameObject);
		} else if(guessTime.text == rightAnswer2 && Input.GetKeyDown(KeyCode.Return)){
			Instantiate(reward, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
			guessTime.gameObject.SetActive (false);	
			analogClock.SetActive (false);
			Destroy(gameObject);
		} else if (Input.GetKeyDown(KeyCode.Return)){
			Debug.Log("Wrong!");
		}

	}

}