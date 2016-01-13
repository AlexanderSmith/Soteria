using UnityEngine;
using System.Collections;

public class PuppetPuzzleController : MonoBehaviour
{
	public GameObject leftSpot;
	public GameObject backSpot;
	public GameObject rightSpot;
	public GameObject finalSpot;

	void Awake()
	{
		leftSpot.GetComponent<Light>().enabled = false;
		backSpot.GetComponent<Light>().enabled = false;
		rightSpot.GetComponent<Light>().enabled = false;
		finalSpot.GetComponent<Light>().enabled = false;
	}

	public void Initialize()
	{
		if (!GameDirector.instance.GetPuppetActivated())
		{
			GameDirector.instance.PuppetPuzzleActivated();
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringTheaterPuzzFirstTime");
			GameDirector.instance.StartDialogue();
		}
		else
		{
			leftSpot.GetComponent<Light>().enabled = true;
		}
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
				leftSpot.GetComponent<Light>().enabled = true;
			}
		}
	}

	public void LightEncounter(GameObject light)
	{
		if (light != null)
		{
			light.GetComponent<Light>().enabled = true;
		}
		else
		{
			this.FinalEncounter();
		}
	}

	void FinalEncounter()
	{

	}
}