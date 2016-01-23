using UnityEngine;
using System.Collections;

public class PuppetPuzzleController : MonoBehaviour
{
	private GameObject _intro;

	public GameObject leftSpot;
	public GameObject backSpot;
	public GameObject rightSpot;
	public GameObject finalSpot;

	void Awake()
	{
		leftSpot.GetComponent<Light>().enabled = false;
		leftSpot.GetComponent<SphereCollider>().enabled = false;
		backSpot.GetComponent<Light>().enabled = false;
		backSpot.GetComponent<SphereCollider>().enabled = false;
		rightSpot.GetComponent<Light>().enabled = false;
		rightSpot.GetComponent<SphereCollider>().enabled = false;
		finalSpot.GetComponent<Light>().enabled = false;
		finalSpot.GetComponent<SphereCollider>().enabled = false;
		this._intro = GameObject.Find("PuppetPuzzleIntro");
		this._intro.SetActive(false);
	}

	public void Initialize()
	{
		if (!GameDirector.instance.GetPuppetActivated())
		{
			GameDirector.instance.PuppetPuzzleActivated();
			this._intro.SetActive(true);
		}
		else
		{
			leftSpot.GetComponent<Light>().enabled = true;
		}
	}

	public void LeftLightOn()
	{
		leftSpot.GetComponent<Light>().enabled = true;
		leftSpot.GetComponent<SphereCollider>().enabled = true;
	}

	public void LightEncounter(GameObject light)
	{
		if (light != null)
		{
			light.GetComponent<Light>().enabled = true;
			light.GetComponent<SphereCollider>().enabled = true;
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