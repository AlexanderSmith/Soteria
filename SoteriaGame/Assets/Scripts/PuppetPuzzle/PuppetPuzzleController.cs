using UnityEngine;
using System.Collections;

public class PuppetPuzzleController : MonoBehaviour
{
	private GameObject _intro;

	public GameObject leftSpot;
	public GameObject backSpot;
	public GameObject rightSpot;
	public GameObject finalSpot;

	private GameObject _currentLight;

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
		// Hacks for checking ability to fight puzzle
//		GameDirector.instance.PuppetPuzzleActivated();
//		GameDirector.instance.SuitRemoved();

		if (!GameDirector.instance.GetPuppetActivated())
		{
			this._intro.SetActive(true);
		}
		else
		{
			this.LeftLightOn();
		}
	}

	public void LeftLightOn()
	{
		leftSpot.GetComponent<Light>().enabled = true;
		leftSpot.GetComponent<SphereCollider>().enabled = true;
	}

	public void LightEncounter(GameObject inThisLight, GameObject inNextLight)
	{
		this._currentLight = inThisLight;

		if (inNextLight != null)
		{
			inNextLight.GetComponent<Light>().enabled = true;
			inNextLight.GetComponent<SphereCollider>().enabled = true;
		}
		else
		{
			this.FinalEncounter();
		}
	}

	public void LightDefeated()
	{
		this._currentLight.GetComponent<PuppetLightEncounter>().LightDefeated();
	}

	void FinalEncounter()
	{

	}
}