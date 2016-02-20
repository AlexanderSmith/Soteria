using UnityEngine;
using System.Collections;


public enum ItemType
{
	Lantern,
	Token,
	Compass
}

public class SelectCurrentItemForSwap : MonoBehaviour {


	private bool isSelectable = true;
	public ItemType Type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton( 0 ) && isSelectable)
		{
			GameDirector.instance.SwapItemForKey(Type);
			GameDirector.instance.EndKeyInteraction();

			isSelectable = false;
		}
		else if (!isSelectable)
		{
			///Swap for a greyscale image or deactivate!
		}
	}
}
