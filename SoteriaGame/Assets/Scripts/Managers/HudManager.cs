using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudManager : MonoBehaviour {

	private Image _fadeinout;
	private Color _clearFade;
	public float blackSpeed = .15f;
	public float clearSpeed = .15f;
	public bool isToClear = true;
	private bool _isonscreen;
	private bool _puppetPuzzle;

	private GameObject _hudinterface;
	private ButtonController _coinController;
	private LanternModeController _lanternController;

	private Image _onIndicator;
	private Image _offIndicator;
	private Quaternion _compassRotation;
	private Sprite _1card;
	private Sprite _2cards;
	private Sprite _3cards;
	private Sprite _splashTest;
	private Sprite _idleLantern;
	private Sprite _activeLantern;
	// 1 = Music, 2 = Theater, 3 = Observatory
	private int _district;
	private GameObject _currentCard;

	public GameObject Objective;
	private GameObject [] Buttons;
	private GameObject _splashScreen;
	private GameObject _splashScreenCard;
	private GameObject _noResponse;
	private GameObject _yesResponse;

	// Inventory items
	private GameObject _token;
	private GameObject _lantern;
	private GameObject _compass;
	private GameObject _cards;
	private GameObject _suit;
	//	private GameObject leftKeyPiece;
	//	private GameObject middleKeyPiece;
	//	private GameObject centerKeyPiece;

	// Use this for initialization
	void Awake () 
	{

	}

	public void Initialize()
	{
		this._clearFade = new Color(0, 0, 0, .01f);
		this._hudinterface = GameObject.Find ("HUDInterface");
		this._fadeinout = _hudinterface.transform.FindChild("FadeTexture").gameObject.GetComponent<Image>();
		this._fadeinout.color = Color.clear;

		this._onIndicator = _hudinterface.transform.FindChild("OnIndicator").gameObject.GetComponent<Image> ();
		this._offIndicator = _hudinterface.transform.FindChild("OffIndicator").gameObject.GetComponent<Image> ();

		this._offIndicator.gameObject.SetActive(false);
		this._onIndicator.gameObject.SetActive(false);

		this.Buttons = new GameObject [4];
		for (int i=0; i< 4; ++i)
			this.Buttons[i] = _hudinterface.transform.FindChild("Button_"+i).gameObject;

		this._coinController = _hudinterface.GetComponentInChildren<ButtonController>();
		this._coinController.Initialize (this);
		this._lanternController = _hudinterface.GetComponentInChildren<LanternModeController>();
		this._lanternController.Initialize(this);

		this._1card = Resources.Load("GUI/1Card", typeof(Sprite)) as Sprite;
		this._2cards = Resources.Load("GUI/2Cards", typeof(Sprite)) as Sprite;
		this._3cards = Resources.Load("GUI/3Cards", typeof(Sprite)) as Sprite;
		//this._idleLantern = Resources.Load("GUI/HUD_lantern_off", typeof(Sprite)) as Sprite;
		//this._activeLantern = Resources.Load("GUI/HUD_lantern_light", typeof(Sprite)) as Sprite;

		// Splash screen images
		this._splashTest = Resources.Load ("GUI/ScreenTest", typeof(Sprite)) as Sprite;
		this._splashScreen = GameObject.Find("SplashScreen");
		this._splashScreen.GetComponent<Image>().sprite = this._splashTest;
		this._splashScreen.GetComponent<Image>().enabled = false;
		this._splashScreenCard = GameObject.Find("SplashScreenCard");
		this._splashScreenCard.GetComponent<Image>().enabled = false;
		this._noResponse = GameObject.Find("No");
		this._noResponse.GetComponent<Text>().enabled = false;
		this._yesResponse = GameObject.Find("Yes");
		this._yesResponse.GetComponent<Text>().enabled = false;
		
		//Inventory item game object references
		this._token = GameObject.Find("Coin");
		this._token.GetComponent<Image>().enabled = false;
		this._lantern = GameObject.Find ("Lantern");
		this._lantern.GetComponent<Image>().enabled = false;
		this._compass = GameObject.Find("Compass");
		this._compass.GetComponent<Image>().enabled = false;
		this._cards = GameObject.Find("Cards");
		this._cards.GetComponent<Image>().enabled = false;
		this._suit = GameObject.Find("Suit");
		this._suit.GetComponent<Image>().enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateFade ();
		UpdateIndicator ();
	}

	private void UpdateIndicator ()
	{
		if (Objective == null) 
		{
			this._offIndicator.gameObject.SetActive(false);
			this._onIndicator.gameObject.SetActive(false);
			return;
		}
		///Convert position of Objective to Screen Coordinates.
		/// Create A Camera Manager!!!
		Vector3 ObjectPosition = Camera.main.WorldToScreenPoint (Objective.transform.position);
		
		if (InSideScreen (ObjectPosition))
		{
			this._onIndicator.gameObject.SetActive(true);
			this._offIndicator.gameObject.SetActive(false);
			
			this._onIndicator.transform.position = new Vector3( ObjectPosition.x , ObjectPosition.y , 0);
		}
		else
		{
			this._offIndicator.gameObject.SetActive(true);
			this._onIndicator.gameObject.SetActive(false);
			
			///Calc the Offscreen Position///
			float offset = 25.0f;
			float x = ObjectPosition.x , y = ObjectPosition.y;
			//Vector3 BorderPosition = new Vector3(0.0f, 0.0f, 0.0f);
			
			//Check if Set Z to infront of camera if it's behind set it infront for the sake of calculations//
			//Shouldn't be a problem unless we move the camera can turn in all 3 Axis.
			//if (ObjectPosition.z < 0)
			//	ObjectPosition = - ObjectPosition;
			
			//Check if X is out of bounds//
			if (ObjectPosition.x > Screen.width)
				x = Screen.width - offset;
			else if (ObjectPosition.x < 0)
				x = offset;
			
			//Check if Y is out of bounds//
			if (ObjectPosition.y > Screen.height)
				y = Screen.height - offset;
			else if (ObjectPosition.y < 0)
				y = offset;
			
			_offIndicator.transform.position = new Vector3 (x, y , 0);

			//Should also add rotation for the arrow to point at,
			this._compassRotation = Quaternion.identity;
			if (GameDirector.instance.GetPlayer().gameObject.transform.position.x < ObjectPosition.x /*|| GameDirector.instance.GetPlayer().gameObject.transform.position.z < ObjectPosition.z*/)
			{
				this._compassRotation.eulerAngles = new Vector3(0, 0, -Vector3.Angle(GameDirector.instance.GetPlayer().gameObject.transform.position - ObjectPosition, 
			                                                                    GameDirector.instance.GetPlayer().gameObject.transform.forward));
			}
			else
			{
				this._compassRotation.eulerAngles = new Vector3(0, 0, Vector3.Angle(ObjectPosition - GameDirector.instance.GetPlayer().gameObject.transform.position, 
				                                                                    GameDirector.instance.GetPlayer().gameObject.transform.forward));
			}
			this._offIndicator.transform.rotation = this._compassRotation;

			/************************Still need below*********************/
			//Add a second check if it's in the corner than rotate the arrow correctly.
		}
	}

	private void UpdateFade()
	{
		if (isToClear)
			FadeToClear ();
		else if (!isToClear && _puppetPuzzle)
		{
			FadeToWhite();
		}
		else
			FadeToBlack ();
	}

	private bool InSideScreen( Vector3 inObjPos)
	{
		//Nested IFs for the sake of readability. (checks that the object is within the screen )
		if (inObjPos.z > 0)
		{
			if (inObjPos.x < Screen.width && inObjPos.x > 0)
			{
				if (inObjPos.y < Screen.height && inObjPos.y > 0 )
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	///To swap components the only method I found is by using reflection,
	//No worth the trouble. hmmm, I'll check for another method for now it works.
	public void RemoveItemFromInventory (int itemindex)
	{		
		for (int i=0; i<4; ++i)
		{
			if (Buttons[i].activeSelf)
			{				
				Buttons[i].SetActive(false);
				break;
			}	
		}
	}

	public void RemoveIndicator()
	{
		Objective = null;
	}

	public void EncounterClear()
	{
		_fadeinout.color -= this._clearFade;
	}

	public void PuppetPuzzleEncounterClear()
	{
		this._fadeinout.color -= this._clearFade;
	}

	private void FadeToClear()
	{
		_fadeinout.color = Color.Lerp(_fadeinout.color, Color.clear, clearSpeed);
	}
	
	private void FadeToBlack()
	{
		_fadeinout.color = Color.Lerp(_fadeinout.color, Color.black, blackSpeed * Time.deltaTime);
		if (_fadeinout.color.a >= .8f && !GameDirector.instance.IsTokenUsed())
		{
			GameDirector.instance.EncounterOver();
		}
	}

	private void FadeToWhite()
	{
		this._fadeinout.color = Color.Lerp (this._fadeinout.color, Color.white, blackSpeed * Time.deltaTime);
		if (this._fadeinout.color.a >= .8f && !GameDirector.instance.IsTokenUsed())
		{
			GameDirector.instance.EncounterOver();
		}
	}

	public void OMalleyFadeTrue()
	{
		this._fadeinout.gameObject.SetActive(true);
	}

	public void EnableEncounterView()
	{
		this._fadeinout.gameObject.SetActive(true);
		if (this._token.activeSelf == true)
		{
			this._coinController.Pulsing = true;
		}
	}

	public void EnablePuppetPuzzleEncounterView()
	{
		this._fadeinout.gameObject.SetActive(true);
		this._puppetPuzzle = true;
		if (this._token.activeSelf == true)
		{
			this._coinController.Pulsing = true;
		}
	}
	
	public void DisableEncounterView()
	{
		//this._fadeinout.gameObject.SetActive (false);
		this.isToClear = true;
		this._puppetPuzzle = false;
		this._coinController.Pulsing = false;
	}

	public void SafteyLightButtonHit()
	{
		GameDirector.instance.TakeSafteyLight();
	}

	public void TokenTrue(bool inToken)
	{
		//this.token.SetActive(inToken);
		this._token.GetComponent<Image>().enabled = true;
	}

	public void CompassTrue(bool inCompass)
	{
		//this.compass.SetActive(inCompass);
		this._compass.GetComponent<Image>().enabled = true;
		this.ChangeObjective(GameObject.Find("ReceiveLantern"));
	}
	
	public void LanternTrue(bool inLantern)
	{
		//this.lantern.SetActive(inLantern);
		this._lantern.GetComponent<Image>().enabled = true;
		this.ChangeObjective(GameObject.Find("HubToMusic"));
	}

	public void PulseLantern()
	{
		if (this._lantern.activeSelf == true)
		{
			this._lanternController.Pulsing = true;
		}
	}

	public void IdleLantern()
	{
		this._lanternController.Pulsing = false;
	}

	public void ChangeObjective(GameObject gObj)
	{
		this.Objective = gObj;
	}

	public void StartItemInteraction(Sprite inSprite)
	{
 		this._splashScreen.GetComponent<Image>().enabled = true;
		this._splashScreenCard.GetComponent<Image>().sprite = inSprite;
		this._splashScreenCard.GetComponent<Image>().enabled = true;
	}

	public void EnableItemResponseOptions()
	{
		this._noResponse.GetComponent<Text>().enabled = true;
		this._yesResponse.GetComponent<Text>().enabled = true;
	}
	
	public void EndItemInteraction(bool inResponse)
	{
		this._splashScreen.GetComponent<Image>().enabled = false;
		this._splashScreenCard.GetComponent<Image>().enabled = false;
	}

	public void StartCardInteraction(Sprite inSprite, int inDist, GameObject inCardObj)
	{
		this._district = inDist;
		this._currentCard = inCardObj;
 		this._splashScreen.GetComponent<Image>().enabled = true;
		this._splashScreenCard.GetComponent<Image>().sprite = inSprite;
		this._splashScreenCard.GetComponent<Image>().enabled = true;
	}

	public void EnableCardResponseOptions()
	{
		this._noResponse.GetComponent<Text>().enabled = true;
		this._yesResponse.GetComponent<Text>().enabled = true;
	}

	public void EndCardInteraction(bool inResponse)
	{
		this._splashScreen.GetComponent<Image>().enabled = false;
		this._splashScreenCard.GetComponent<Image>().enabled = false;
		this._noResponse.GetComponent<Text>().enabled = false;
		this._yesResponse.GetComponent<Text>().enabled = false;
		if (inResponse)
		{
			string selectedCard = FindCard (this._district);
			GameDirector.instance.PlayerHasCard(this._district, selectedCard);
			this._currentCard.SetActive(false);
		}
	}

	string FindCard(int card)
	{
		switch (card)
		{
		case 1:
			return "Whistle";
		case 2:
			return "HandDoll";
		case 3:
			return "EggShells";
		case 4:
			return "StringOfPearls";
		case 5:
			return "PartyHat";
		case 6:
			return "Chameleon";
		case 7:
			return "BackpackAndJournal";
		case 8:
			return "TrainTicket";
		case 9:
			return "StarChart";
		default:
			return "";
		};
	}

	public void DisplayCards(int inNumCards)
	{
		switch (inNumCards)
		{
		case 0:
			this._cards.GetComponent<Image>().enabled = false;
			break;
		case 1:
			this._cards.GetComponent<Image>().sprite = this._1card;
			this._cards.GetComponent<Image>().enabled = true;
			break;
		case 2:
			this._cards.GetComponent<Image>().sprite = this._2cards;
			this._cards.GetComponent<Image>().enabled = true;
			break;
		case 3:
			this._cards.GetComponent<Image>().sprite = this._3cards;
			this._cards.GetComponent<Image>().enabled = true;
			break;
		};
	}

	public void SuitTrue()
	{
		this._cards.GetComponent<Image>().enabled = false;
		this._suit.GetComponent<Image>().enabled = true;
	}

	public void OMalleyEncounter()
	{
		OMalleyFadeTrue();
	}

	public void SetupScreenFade ()
	{
		this._fadeinout.gameObject.SetActive(true);
		_fadeinout.color = Color.clear;
	}

	public void FadeScreenByAmount(Color NewColor, float deltaTime)
	{
		_fadeinout.color = Color.Lerp(_fadeinout.color, NewColor, blackSpeed * deltaTime);
	}

	public void ClearScreenFade()
	{
		_fadeinout.color = new Color(0, 0, 0, 0);
	}

	public void PauseScreenFade()
	{
		this._fadeinout.gameObject.SetActive(false);
		this._fadeinout.color = this._fadeinout.color;
	}

	public void ResumeScreenFade()
	{
		this._fadeinout.gameObject.SetActive(true);
	}
}