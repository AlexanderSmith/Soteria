using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionPrompt : MonoBehaviour {
	
	public float timeDelay = 5;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameObject.Find("Player").transform.position.x > 93.0f)
		{
		//	Debug.Log ("OH yEAH");
			
			//this.GetComponent<Text>().text = "Hide Behind the Wall to Avoid the Shadow Creatures!";
			this.gameObject.SetActive(true);
			if (timeDelay == 0) timeDelay = 5;	
		}

		timeDelay -= Time.deltaTime;
		if ( timeDelay < 0 )
		{
			this.gameObject.SetActive(false);
			//GameObject X =  GameObject.FindGameObjectWithTag("InstructionPrompt");
			//X.SetActive(false);
		}

	}
}
