using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudManager : MonoBehaviour {

	private Image _fadeinout;
	public float blackSpeed = .15f;
	public float clearSpeed = .15f;
	public bool isToClear = true;

	private GameObject _hudinterface;
	private ButtonController _coinController;

	private Image _onIndicator;
	private Image _offIndicator;
	private bool _isonscreen;
	public GameObject Objective;

	private GameObject [] Buttons;

	// Inventory items
	private GameObject token;
	private GameObject lantern;
	private GameObject compass;
	//	private GameObject cards;
	//	private GameObject suit;
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

		this.Buttons = new GameObject [6];
		for (int i=0; i< 4; ++i)
			this.Buttons[i] = _hudinterface.transform.FindChild("Button_"+i).gameObject;

		this._coinController = _hudinterface.GetComponentInChildren<ButtonController> ();
		this._coinController.Initialize (this);
		
		//Inventory item game object references
		this.token = GameObject.Find("Coin");
		this.token.SetActive(true);
		this.token.GetComponent<Image>().enabled = false;
		this.lantern = GameObject.Find("Lantern");
		this.lantern.SetActive(true);
		this.lantern.GetComponent<Image>().enabled = false;
//		// Same for compass, cards, suit, and key pieces;
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
		if (this.token.activeSelf == true)
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
		this.token.GetComponent<Image>().enabled = true;
	}

	public void CompassTrue(bool inCompass)
	{
		//this.compass.SetActive(inCompass);
		this.ChangeObjective(GameObject.Find("ReceiveLantern"));
	}
	
	public void LanternTrue(bool inLantern)
	{
		//this.lantern.SetActive(inLantern);
		this.lantern.GetComponent<Image>().enabled = true;
		this.ChangeObjective(GameObject.Find("HubToMusic"));
	}

	public void ChangeObjective(GameObject gObj)
	{
		this.Objective = gObj;
	}
}