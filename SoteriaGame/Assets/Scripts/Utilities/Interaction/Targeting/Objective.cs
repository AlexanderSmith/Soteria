using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Quick Prototype for the Indicator Mostly done it on the Object.
//it Should work with the HUD Manager so I'm thinking the Manager should look
//up the Target in the scene and take care of traking it, this does the exact opposite.

public class Objective : MonoBehaviour {

	private Canvas _canvas;
	private bool _isonscreen;

	public Image onIndicator;
	public Image offIndicator;
	// Use this for initialization
	void Start () 
	{
		this._canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		//instanetate it in the scene at a random position outside the screen (vector3 uses screen coordinates).
		this.onIndicator  = Instantiate(this.onIndicator,Vector3.zero,Quaternion.Euler(new Vector3(0,0,0))) as Image; 
		this.offIndicator  = Instantiate(this.offIndicator,Vector3.zero ,Quaternion.Euler(new Vector3(0,0,0))) as Image;

		this.onIndicator.transform.SetParent (this._canvas.transform);
		this.offIndicator.transform.SetParent (this._canvas.transform);

		this.offIndicator.gameObject.SetActive(false);
		this.onIndicator.gameObject.SetActive(false);
		this.onIndicator.enabled = true; // On instatiation the Image script is deactivated but the object is not...
		this.offIndicator.enabled = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
		///Convert position of Objective to Screen Coordinates.
		Vector3 ObjectPosition = Camera.main.WorldToScreenPoint (this.transform.position);
	
		if (InSideScreen (ObjectPosition))
		{
			this.onIndicator.gameObject.SetActive(true);
			this.offIndicator.gameObject.SetActive(false);

			onIndicator.transform.position = new Vector3( ObjectPosition.x , ObjectPosition.y , 0);
		}
		else
		{
			this.offIndicator.gameObject.SetActive(true);
			this.onIndicator.gameObject.SetActive(false);

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

			offIndicator.transform.position = new Vector3 (x, y , 0);
			//Should also add rotation for the arrow to point at,
			//Add a second check if it's in the corner than rotate the arrow correctly. 

		}
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
}
