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
	void Start ()
	{
	}

	public void SelectChoice() {

		if (isSelectable)
		{
			GameDirector.instance.SwapItemForKey(Type);
			GameDirector.instance.EndKeyInteraction();
			
			isSelectable = false;
			TurnOff ();
		}
	}

	// Update is called once per frame
	void TurnOff()
	{
		this.gameObject.SetActive(false);
	}
}
