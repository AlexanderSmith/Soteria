using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudManager : MonoBehaviour {

	private Image _fadeinout;
	public float blackSpeed = .15f;
	public float clearSpeed = .15f;
	public bool isToClear = true;
	private bool _isonscreen;

	private GameObject _hudinterface;
	private ButtonController _coinController;

	private Image _onIndicator;
	private Image _offIndicator;
	private Quaternion _compassRotation;
	private Sprite _1card;
	private Sprite _2cards;
	private Sprite _3cards;
	private Sprite _splashTest;
	private Sprite _backPack;
	private Sprite _eggShells;
	private Sprite _handDoll;
	private Sprite _partyHat;
	private Sprite _starChart;
	private Sprite _stringOfPearls;
	private Sprite _trainTicket;
	private Sprite _whistle;
	private Sprite _chameleon;

	public GameObject Objective;
	private GameObject [] Buttons;
	private GameObject _splashScreen;
	private GameObject _splashScreenCard;

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

		this._coinController = _hudinterface.GetComponentInChildren<ButtonController> ();
		this._coinController.Initialize (this);

		this._1card = Resources.Load("GUI/1Card", typeof(Sprite)) as Sprite;
		this._2cards = Resources.Load("GUI/2Card", typeof(Sprite)) as Sprite;
		this._3cards = Resources.Load("GUI/3Card", typeof(Sprite)) as Sprite;

		// Splash screen images
		this._splashTest = Resources.Load ("GUI/ScreenTest", typeof(Sprite)) as Sprite;
		this._splashScreen = GameObject.Find("SplashScreen");
		this._splashScreen.GetComponent<Image>().sprite = this._splashTest;
		this._splashScreen.GetComponent<Image>().enabled = false;
		this._splashScreenCard = GameObject.Find("SplashScreenCard");
		this._splashScreenCard.GetComponent<Image>().enabled = false;

		
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

		this._backPack = Resources.Load("GUI/Cards/BackPackAndJournal", typeof(Sprite)) as Sprite;
		this._eggShells = Resources.Load("GUI/Cards/EggShells", typeof(Sprite)) as Sprite;
		this._handDoll = Resources.Load("GUI/Cards/HandDoll", typeof(Sprite)) as Sprite;
		this._partyHat = Resources.Load("GUI/Cards/PartyHat", typeof(Sprite)) as Sprite;
		this._starChart = Resources.Load("GUI/Cards/StarChart", typeof(Sprite)) as Sprite;
		this._stringOfPearls = Resources.Load("GUI/Cards/StringOfPearls", typeof(Sprite)) as Sprite;
		this._trainTicket = Resources.Load("GUI/Cards/TrainTicket", typeof(Sprite)) as Sprite;
		this._whistle = Resources.Load("GUI/Cards/Whistle", typeof(Sprite)) as Sprite;
		//this._chameleon = Resources.Load("GUI/Cards/Chameleon", typeof(Sprite)) as Sprite;
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
			this._compassRotation.eulerAngles = new Vector3(0, 0, Vector3.Angle(_offIndicator.transform.position - ObjectPosition, _offIndicator.transform.forward));
			this._offIndicator.transform.rotation = this._compassRotation;

			/************************Still need below*********************/
			//Add a second check if it's in the corner than rotate the arrow correctly.
		}
	}

	private void UpdateFade()
	{
		if (isToClear)
			FadeToClear ();
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
		_fadeinout.color -= new Color (0, 0, 0, .01f);
	}

	private void FadeToClear()
	{
		_fadeinout.color = Color.Lerp(_fadeinout.color, Color.clear, clearSpeed);
	}
	
	private void FadeToBlack()
	{
		_fadeinout.color = Color.Lerp(_fadeinout.color, Color.black, blackSpeed * Time.deltaTime);
		if (_fadeinout.color.a >= .8f)
		{
			GameDirector.instance.EncounterOver();
		}
	}

	public void EnableEncounterView()
	{
		this._fadeinout.gameObject.SetActive(true);
		if (this._token.activeSelf == true)
		{
			this._coinController.Pulsing = true;
		}
	}
	
	public void DisableEncounterView()
	{
		//this._fadeinout.gameObject.SetActive (false);
		this.isToClear = true;
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

	public void ChangeObjective(GameObject gObj)
	{
		this.Objective = gObj;
	}

	public void StartCardInteraction()
	{
		this._splashScreen.GetComponent<Image>().enabled = true;
	}

	public void EndCardInteraction()
	{
		this._splashScreen.GetComponent<Image>().enabled = false;
	}
}