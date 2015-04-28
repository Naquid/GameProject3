using UnityEngine;
using UnityEngine.UI;
using System.Collections;

interface ISpeech
{
	void OnTalkTo();
}


public class SpeechBubble : MonoBehaviour {

	RectTransform mainCanvasRect;
	Text textComp;
	Text nameComp;
	Image speechBubbleComp;
	Canvas mainCanvas;
	Image speechSymbol;

	public AudioClip speechSound;

	Animator anim;

	float currentOpenTime;
	bool bubbleOpen = false;

	const float symbolOpenTime = 5.0f;
	const float textOpenTimeBase = 2.0f;
	const float textOpenTimePerLetter = 0.1f;

	const float textWidthPerLetter = 15.0f;
	const float textMaxWidth = textWidthPerLetter * 35.0f; 
	const float textMinWidth = 200.0f;

	const float textHightPerLetter = 35.0f;
	const float textMinHight = 150.0f;




	// Use this for initialization
	void Start () {


		mainCanvas = gameObject.GetComponentInChildren<Canvas> ();
		anim = gameObject.GetComponentInChildren<Animator>();
		mainCanvasRect = mainCanvas.GetComponent<RectTransform> ();

		//dont show at start
		mainCanvas.enabled = false;


		//IMAGES
		Image[] tmpImages = gameObject.GetComponentsInChildren<Image> ();

		foreach (Image img in tmpImages)
		{
			if( img.CompareTag("SpeechSymbol") )
			{
				speechSymbol = img;
			}
			else
			{
				speechBubbleComp = img;
			}
		}

		//Text
		Text[] tmpNames = gameObject.GetComponentsInChildren<Text>();

		foreach (Text txt in tmpNames)
		{
			if( txt.CompareTag("SpeechName") )
			{
				nameComp = txt;
			}
			else
			{
				textComp = txt;
			}
		}


	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (bubbleOpen == true)
		{

			currentOpenTime -= Time.deltaTime;

			if(currentOpenTime <= 0.0f)
			{
				//anim.Play(outAnimation);
				anim.SetBool("enabled", false);
				bubbleOpen = false;
			}

		}

	}


	public void SetText(string text, int length = -1)
	{
		if (length == -1)
		{
			length = text.Length;
		}


		if(string.Compare(text, "") != 0 )
		{
			PlaySpeechAudio();

			anim.SetBool("enabled",true);
			//anim.Play ( inAnimation );
			textComp.text = text;
			mainCanvas.enabled = true;

			bubbleOpen = true;

			currentOpenTime = textOpenTimeBase;
			currentOpenTime += (length * textOpenTimePerLetter);

			float width = 0.0f;
			float hight = 0.0f;
			float numberOfLines = (length * textWidthPerLetter) / textMaxWidth;

			//see if we have more than one line
			if( numberOfLines < 1)
			{
				width = (length * textWidthPerLetter);
			}
			else
			{
				width = textMaxWidth;
			}

			//dont go below the min value
			if( width < textMinWidth )
			{
				width = textMinWidth;
			}

			hight = numberOfLines * textHightPerLetter;

			if( hight < textMinHight )
			{
				hight = textMinHight;
			}

			mainCanvasRect.sizeDelta = new Vector2 (width, hight);
		}
		else
		{
			textComp.text = "";
		}

	}


	public void SetSymbol(Sprite newTexture)
	{
		if (newTexture != null) 
		{

			PlaySpeechAudio();

			anim.SetBool("enabled",true);
			//anim.Play (inAnimation);
			speechSymbol.sprite = newTexture;
			mainCanvas.enabled = true;

			bubbleOpen = true;
			currentOpenTime = symbolOpenTime;
			//speechSymbol.color = startColor;
		} 
		else
		{
			speechSymbol.sprite = null;
			//speechSymbol.color = new Color(0.0f,0.0f,0.0f,0.0f);
		}

	}

	public void SetName(string newName)
	{
		if (newName != null) 
		{
			nameComp.text = newName;
		} 

	}

	void PlaySpeechAudio()
	{
		if(speechSound != null)
		{
			AudioSource.PlayClipAtPoint( speechSound, transform.localPosition );
		}
	}

}
