using UnityEngine;
using System.Collections;

public class InteractionProxy : MonoBehaviour {

	public InteractionBase InteractionType;

	// Use this for initialization
	void Awake () 
	{
		if (InteractionType != null)
			this.InteractionType.Awake();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (InteractionType != null)
			this.InteractionType.Update();
	}
	
	void OnTriggerEnter(Collider other)
	{
		this.InteractionType.TriggerEnter(other);
		Debug.Log("Trigger Start");
	}
	void OnTriggerExit(Collider other)
	{
		this.InteractionType.TriggerExit(other);
		Debug.Log("Trigger exit");
	}
	void OnTriggerStay(Collider other)
	{
		this.InteractionType.TriggerStay(other);
		Debug.Log("Trigger stay");
	}

}
