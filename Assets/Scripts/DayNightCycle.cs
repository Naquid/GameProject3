using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DayNightCycle : MonoBehaviour {

	public enum TimeOfDay
	{
		Idle,
		SunRise,
		SunSet
	}

	public Text digitalClock;

	public Image analogClockHour;
	public Image analogClockMinute;

	public Transform[] sun;
	public float dayCycleInMinutes;

	private float dayCycleInSeconds;

	public float sunRise;
	public float sunSet;
	public float skyboxBlendModifier;

	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * SECOND;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;

	private float degreeRotation;

	private float timeOfDay;

	private TimeOfDay tod;

	public bool isNight = true;

	public float secondsThisDay = 1;

	void Start (){

		tod = TimeOfDay.Idle;

		dayCycleInSeconds = dayCycleInMinutes * MINUTE;
		timeOfDay = 0;

		RenderSettings.skybox.SetFloat ("_Blend", 0);

		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);

		sunRise *= dayCycleInSeconds;
		sunSet *= dayCycleInSeconds;


	}

	void Update(){

		for (int i = 0; i < sun.Length; i++) {
			sun [i].Rotate (new Vector3(degreeRotation, 0f, 0f) * Time.deltaTime);
		}

		analogClockHour.transform.Rotate (new Vector3 (0f, 0f, -degreeRotation * 2) * Time.deltaTime);
		analogClockMinute.transform.Rotate (new Vector3 (0f, 0f, (-degreeRotation * 2) * 12) * Time.deltaTime);

		timeOfDay += Time.deltaTime;

		if (timeOfDay > dayCycleInSeconds) {
			timeOfDay -= dayCycleInSeconds;
		}

		if (timeOfDay > sunRise && timeOfDay < sunSet && RenderSettings.skybox.GetFloat ("_Blend") < 1) {
			tod = TimeOfDay.SunRise;
			isNight = false;
			BlendSkybox ();
		} else if (timeOfDay > sunSet && RenderSettings.skybox.GetFloat ("_Blend") > 0) {
			tod = TimeOfDay.SunSet;
			isNight = true;
			BlendSkybox ();
		} else {
			tod = TimeOfDay.Idle;
		}

		secondsThisDay += Time.deltaTime * (86400 / (60*dayCycleInMinutes));

		if (secondsThisDay >= 86400) {
			secondsThisDay = 1;
		}

		digitalClock.text = ConvertSecondsToHMS ((int)secondsThisDay);

	}

	string ConvertSecondsToHMS(int secondsToConvert){

		return string.Format("{0:00}.{1:00}.{2:00}",secondsToConvert/3600,(secondsToConvert/60)%60,secondsToConvert%60);

	}

	private void BlendSkybox(){

		float blend = 0;

		switch (tod) {

		case TimeOfDay.SunRise:
			blend = (timeOfDay - sunRise) / dayCycleInSeconds * skyboxBlendModifier;
			break;
		case TimeOfDay.SunSet:
			blend = (timeOfDay - sunSet) / dayCycleInSeconds * skyboxBlendModifier;
			blend = 1 - blend;
			break;
		}

		RenderSettings.skybox.SetFloat ("_Blend", blend);

	}

}
