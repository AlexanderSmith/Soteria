using UnityEngine;
using System.Collections;

public class MusicPuzzleController : MonoBehaviour
{
	private GameObject _oMalleySuitOn;
	private GameObject _oMalleyDefeated;

	private bool _brassPlaying;
	private bool _stringPlaying;
	private bool _windPlaying;
	private bool _organPlaying;

	public GameObject organTile;
	public GameObject brassTile;
	public GameObject stringTile;
	public GameObject windTile;
	public GameObject boss;
	public GameObject musicSuckOrgan;
	public GameObject musicSuckBrass;
	public GameObject musicSuckString;
	public GameObject musicSuckWind;
	public GameObject keyPiece;

	public Transform organDown;
	public Transform organUp;
	public Transform brassDown;
	public Transform brassUp;
	public Transform stringDown;
	public Transform stringUp;
	public Transform windDown;
	public Transform windUp;
	public Transform bossDown;
	public Transform bossUp;

	public float lerpTime;
	public float initialVolume;
	public float winTime;

	public Sprite _leftKey;

	// Use this for initialization
	void Start ()
	{
		this._oMalleySuitOn = GameObject.Find("O'MalleySuitOnMusicPuzzle");
		this._oMalleyDefeated = GameObject.Find("O'MalleyPuzzleDefeated");
		this._oMalleyDefeated.SetActive(false);
		this._brassPlaying = false;
		this._stringPlaying = false;
		this._windPlaying = false;
		this._organPlaying = false;
		this.musicSuckOrgan.SetActive(false);
		this.musicSuckBrass.SetActive(false);
		this.musicSuckString.SetActive(false);
		this.musicSuckWind.SetActive(false);
	}

	public void Initialize()
	{
		// Gameplay hacks to test puzzle fight
//		GameDirector.instance.MusicPuzzleActivated();
//		GameDirector.instance.SuitWorn();
//		GameDirector.instance.SuitRemoved();

		if (GameDirector.instance.GetMusicActivated())
		{
			this.organTile.transform.position = this.organDown.position;
			this.brassTile.transform.position = this.brassUp.position;
			this.stringTile.transform.position = this.stringUp.position;
			this.windTile.transform.position = this.windUp.position;
			this.boss.transform.position = this.bossUp.position;
			if (GameDirector.instance.GetGameState() == GameStates.Suit)
			{
				GameDirector.instance.GetPlayer().PlayerActionPause();
				GameDirector.instance.SetupDialogue("AnaEnteringMusicPuzzWithSuit");
				GameDirector.instance.StartDialogue(true);
			}
			else
			{
				this._oMalleySuitOn.SetActive(false);
			}
		}
		else
		{
			this._oMalleySuitOn.SetActive(false);
			this.organTile.transform.position = this.organUp.position;
			this.brassTile.transform.position = this.brassDown.position;
			this.stringTile.transform.position = this.stringDown.position;
			this.windTile.transform.position = this.windDown.position;
			GameDirector.instance.GetPlayer().PlayerActionPause();
			GameDirector.instance.SetupDialogue("AnaEnteringMusicPuzFirstTime");
			GameDirector.instance.StartDialogue(true);

		}
		boss.GetComponentInChildren<MusicBossController>().Initialize(this);
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			if (!GameDirector.instance.isDialogueActive())
			{
				this.gameObject.GetComponent<BoxCollider>().enabled = false;
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}

	public void PuzzleActivated()
	{
		this.PrivatePuzzleActivated();
	}

	void PrivatePuzzleActivated()
	{
		GameDirector.instance.MusicPuzzleActivated();
		StartCoroutine("ActivatePuzzle");
	}

	IEnumerator ActivatePuzzle()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.organTile.transform.position = Vector3.Lerp(this.organTile.transform.position, this.organDown.position, (Time.time - start) / lerpTime);
			this.brassTile.transform.position = Vector3.Lerp(this.brassTile.transform.position, this.brassUp.position, (Time.time - start) / lerpTime);
			this.stringTile.transform.position = Vector3.Lerp(this.stringTile.transform.position, this.stringUp.position, (Time.time - start) / lerpTime);
			this.windTile.transform.position = Vector3.Lerp(this.windTile.transform.position, this.windUp.position, (Time.time - start) / lerpTime);
			//GameDirector.instance.SubtractVolumePuzzle(AudioID.OrganMusic, 1f);
			yield return null;
		}
		this.organTile.transform.position = this.organDown.position;
		this.brassTile.transform.position = this.brassUp.position;
		this.stringTile.transform.position = this.stringUp.position;
		this.windTile.transform.position = this.windUp.position;
		this.boss.transform.position = this.bossUp.position;
		//GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 0.0f);
	}

	public void PuzzleStartHackBoss()
	{
		StartCoroutine("PuzzleStartHack");
	}

	IEnumerator PuzzleStartHack()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.boss.transform.position = Vector3.Lerp (this.boss.transform.position, this.bossUp.position, (Time.time - start) / lerpTime);
			yield return null;
		}
		this.boss.transform.position = this.bossUp.position;
	}

	public void OrganTileActive()
	{
		StartCoroutine("OrganTileUp");
	}

	IEnumerator OrganTileUp()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.organTile.transform.position = Vector3.Lerp(this.organTile.transform.position, this.organUp.position, (Time.time - start) / lerpTime);
			yield return null;
		}
	}

	public GameObject GetBoss()
	{
		return this.boss;
	}

	public float GetInitialVolume()
	{
		return this.initialVolume;
	}

	public void TileDown(string inMusicPile)
	{
		StartCoroutine(inMusicPile);
	}

	IEnumerator BrassTileDown()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.brassTile.transform.position = Vector3.Lerp(this.brassTile.transform.position, this.brassDown.position, (Time.time - start) / lerpTime);
			GameDirector.instance.DefeatedMusicTile(AudioID.BrassMusic);
			yield return null;
		}
	}

	IEnumerator StringTileDown()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.stringTile.transform.position = Vector3.Lerp(this.stringTile.transform.position, this.stringDown.position, (Time.time - start) / lerpTime);
			GameDirector.instance.DefeatedMusicTile(AudioID.StringMusic);
			yield return null;
		}
	}

	IEnumerator WindTileDown()
	{
		float start = Time.time;
		while (Time.time < start + lerpTime)
		{
			this.windTile.transform.position = Vector3.Lerp(this.windTile.transform.position, this.windDown.position, (Time.time - start) / lerpTime);
			GameDirector.instance.DefeatedMusicTile(AudioID.WindMusic);
			yield return null;
		}
	}

	IEnumerator OrganTileDown()
	{
		yield return null;
	}

	public void PuzzleDefeated()
	{
		GameDirector.instance.MusicPuzzleDefeated();
		//StartCoroutine ("OvercamePuzzle");
		this.KillSuckOrganMusic();
		this._oMalleyDefeated.SetActive(true);
		StopAllCoroutines ();
		GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.WindMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.StringMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 1.0f);
	}

	IEnumerator OvercamePuzzle()
	{
		this.KillSuckOrganMusic();
		this._oMalleyDefeated.SetActive(true);
		float start = Time.time;
		while (Time.time < start + winTime)
		{
			GameDirector.instance.OvercomeMusicPuzzle(AudioID.BrassMusic);
			GameDirector.instance.OvercomeMusicPuzzle(AudioID.WindMusic);
			GameDirector.instance.OvercomeMusicPuzzle(AudioID.StringMusic);
			GameDirector.instance.OvercomeMusicPuzzle(AudioID.OrganMusic);
			yield return null;
		}
		GameDirector.instance.ChangeVolume(AudioID.BrassMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.WindMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.StringMusic, 1.0f);
		GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 1.0f);
	}

	public void StartAllMusicSuck()
	{
		this.musicSuckOrgan.SetActive(true);
		this.musicSuckBrass.SetActive(true);
		this.musicSuckString.SetActive(true);
		this.musicSuckWind.SetActive(true);
	}

	public void MusicSuck(string inMusicSuck)
	{
		switch (inMusicSuck)
		{
		case "Organ":
			this.SuckOrganMusic();
			break;
		case "Brass":
			this.SuckBrassMusic();
			break;
		case "String":
			this.SuckStringMusic();
			break;
		case "Wind":
			this.SuckWindMusic();
			break;
		}
	}

	private void SuckOrganMusic()
	{
		this.musicSuckOrgan.SetActive(true);
	}

	private void SuckBrassMusic()
	{
		this.musicSuckBrass.SetActive(true);
	}

	private void SuckStringMusic()
	{
		this.musicSuckString.SetActive(true);
	}

	private void SuckWindMusic()
	{
		this.musicSuckWind.SetActive(true);
	}

	public void KillMusicSuck(string inMusicSuck)
	{
		switch (inMusicSuck)
		{
		case "Organ":
			this.KillSuckOrganMusic();
			break;
		case "Brass":
			this.KillSuckBrassMusic();
			break;
		case "String":
			this.KillSuckStringMusic();
			break;
		case "Wind":
			this.KillSuckWindMusic();
			break;
		}
	}
	
	private void KillSuckOrganMusic()
	{
		this.musicSuckOrgan.SetActive(false);
	}
	
	private void KillSuckBrassMusic()
	{
		this.musicSuckBrass.SetActive(false);
	}
	
	private void KillSuckStringMusic()
	{
		this.musicSuckString.SetActive(false);
	}
	
	private void KillSuckWindMusic()
	{
		this.musicSuckWind.SetActive(false);
	}
}
