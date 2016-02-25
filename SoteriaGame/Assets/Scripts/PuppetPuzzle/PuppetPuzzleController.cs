using UnityEngine;
using System.Collections;

public class PuppetPuzzleController : MonoBehaviour
{
	public GameObject boss;
	public GameObject keyPiece;
	private GameObject _intro;
	private GameObject _oMalleyPuppetDefeated;

	public GameObject leftSpot;
	public GameObject backSpot;
	public GameObject rightSpot;
	public GameObject finalSpot;

	private GameObject _currentLight;
	private bool _lastFight;

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
		keyPiece.GetComponentInChildren<SphereCollider>().enabled = false;
		this._oMalleyPuppetDefeated = GameObject.Find("O'MalleyPuppetPuzzleDefeated");
		this._oMalleyPuppetDefeated.SetActive(false);
	}

	public void Initialize()
	{
		// Hacks for checking ability to fight puzzle
		GameDirector.instance.PuppetPuzzleActivated();
		GameDirector.instance.SuitRemoved();

		if (!GameDirector.instance.IsTheaterDefeated())
		{
			if (!GameDirector.instance.GetPuppetActivated())
			{
				this._intro.SetActive(true);
			}
			else
			{
				this.LeftLightOn();
			}
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
		if (this._lastFight)
		{
			GameDirector.instance.TheaterPuzzleDefeated();
			this.keyPiece.GetComponentInChildren<SphereCollider>().enabled = true;
			this.KillLights();
		}
	}

	void KillLights()
	{
		leftSpot.GetComponent<Light>().enabled = false;
		leftSpot.GetComponent<SphereCollider>().enabled = false;
		backSpot.GetComponent<Light>().enabled = false;
		backSpot.GetComponent<SphereCollider>().enabled = false;
		rightSpot.GetComponent<Light>().enabled = false;
		rightSpot.GetComponent<SphereCollider>().enabled = false;
		finalSpot.GetComponent<Light>().enabled = false;
		finalSpot.GetComponent<SphereCollider>().enabled = false;
	}

	void FinalEncounter()
	{
		this._lastFight = true;
	}

	public void OpenBossEye()
	{
		this.boss.GetComponent<OpenBossEye>().ShowOpenEye();
	}

	public void SpawnOMalleyAfterPuzzleDefeated()
	{
		this._oMalleyPuppetDefeated.SetActive(true);
	}
}