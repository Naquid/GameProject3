using UnityEngine;
using System.Collections;

interface IClickAbleInterface
{
	void OnClick();
	void OnDragOver( Vector3 deltaMousePosition );
	void OnClickRelease();
}

interface IUseItem
{
	bool UseItemOnObject(EItem itemType);
}

public class ClickObject : MonoBehaviour, IUseItem, IClickAbleInterface, ISpeech
{

	SpeechBubble speech;
	public string actorName;
	
	// Use this for initialization
	void Start () {
	
		speech = gameObject.GetComponent<SpeechBubble> ();

		if (speech != null)
		{
			speech.SetName (actorName);
		}

	}

	public void OnTalkTo()
	{
		if (speech != null)
		{
			speech.SetText ("Jag vill ha Äpple !");
		}

	}

	public void OnClick()
	{		
		if (speech != null) 
		{
			//speech.SetSymbol(test);
			//speech.SetText("Hej på dig! Du är bäst ! Hej på dig! Du är bäst ! Hej på dig! Du är bäst ! Hej på dig! Du är bäst ! Hej på dig! Du är bäst ! Hej på dig! Du är bäst ! Mooooooooooo");
			//speech.SetText("Mooo på <color=#008000ff>" + actorName + "!</color> Hej Hej", 21);
			speech.SetText("Hej, kom hit !");

		}
	}
	
	public void OnClickRelease()
	{
		if (speech != null) 
		{
			//speech.SetText ("Pheew !");	
		}
	}

	public void OnDragOver( Vector3 deltaMousePosition )
	{

	}
	
	public bool UseItemOnObject(EItem itemType)
	{

		switch ( itemType )
		{
			case EItem.Apple :
				speech.SetText("Tack för <color=#008000ff>Äpplet!!</color>", 17);
				return true;

			case EItem.Pear :
				speech.SetText("Usch! Vill inte ha <color=#ff0000ff>Lakrits!!</color>", 26);
				return false;

			default :
				return false;

		}

		return false;
	}

}
