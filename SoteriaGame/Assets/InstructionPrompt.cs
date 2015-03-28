using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionPrompt : MonoBehaviour {
	
	public float timeDelay = 20;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (timeDelay < 10)
		{
		//	Debug.Log ("OH yEAH");
			
			this.GetComponent<Text>().text = "Mash Space to overcome encounters with shadow creatures. Stay in the light to run away from shadow creatures safely.";
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
