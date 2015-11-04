using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	public float noPilesDefeatedVolume;
	public float onePilesDefeatedVolume;
	public float allPilesDefeatedVolume;
	private float _rateOfVolumeIncrease;
	private float _rateOfVolumeDecrease;
	private int _musicPilesDefeated;

	private AudioID _currentMusic;

	private bool _brass;
	private bool _string;
	private bool _wind;
	private bool _fighting;

	private MusicPuzzleController _musicController;

	void Start()
	{
		this.noPilesDefeatedVolume = .01f;
		this.onePilesDefeatedVolume = .02f;
		this.allPilesDefeatedVolume = .03f;
		this._rateOfVolumeIncrease = .02f;
		this._musicPilesDefeated = 0;
		this._brass = false;
		this._string = false;
		this._wind = false;
	}

	public void Initialize(MusicPuzzleController inMusPuzCont)
	{
		this._musicController = inMusPuzCont;
	}

	public void MusicStart(AudioID inAID, string inMusicPile)
	{
		this._currentMusic = inAID;
		this._fighting = false;
		StartCoroutine("MusicEncounter", inAID);
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
	}

	IEnumerator MusicEncounter(AudioID inAID)
	{
		switch (_musicPilesDefeated)
		{
		case 0:
			this._rateOfVolumeDecrease = this.noPilesDefeatedVolume;
			break;
		case 1:
			this._rateOfVolumeDecrease = this.onePilesDefeatedVolume;
			break;
		case 2:
			this._rateOfVolumeDecrease = this.allPilesDefeatedVolume;
			break;
		}
		while (!this._fighting)
		{
			if (GameDirector.instance.GetVolume(inAID) < GameDirector.instance.GetPuzzleWinVolume())
			{
				GameDirector.instance.SubtractVolume(inAID, this._rateOfVolumeDecrease);
			}
			else
			{
				this.PileDone();
				GameDirector.instance.ChangeVolume(inAID, GameDirector.instance.GetPuzzleWinVolume());
			}
			yield return null;
		}
	}

	public void PileDone()
	{
		this._fighting = true;
//		switch (inMusicPile)
//		{
//		case "brass":
//			this.BrassPileDone();
//			break;
//		case "string":
//			this.StringPileDone();
//			break;
//		case "wind":
//			this.WindPileDone();
//			break;
//		}
	}

	public void BrassMusicStart()
	{
		this._currentMusic = AudioID.BrassMusic;
		StartCoroutine("BrassMusicEncounter");
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
	}

	IEnumerator BrassMusicEncounter()
	{
		switch (_musicPilesDefeated)
		{
		case 0:
			this._rateOfVolumeDecrease = this.noPilesDefeatedVolume;
			break;
		case 1:
			this._rateOfVolumeDecrease = this.onePilesDefeatedVolume;
			break;
		case 2:
			this._rateOfVolumeDecrease = this.allPilesDefeatedVolume;
			break;
		}
		while (!this._brass)
		{
			if (GameDirector.instance.GetVolume(AudioID.BrassMusic) < GameDirector.instance.GetPuzzleWinVolume())
			{
				GameDirector.instance.SubtractVolume(AudioID.BrassMusic, this._rateOfVolumeDecrease);
			}
			else
			{
				this.BrassPileDone();
				GameDirector.instance.ChangeVolume(AudioID.BrassMusic, GameDirector.instance.GetPuzzleWinVolume());
			}
			yield return null;
		}
	}

	public void BrassPileDone()
	{
		this._brass = true;
	}

	public void StringMusicStart()
	{
		this._currentMusic = AudioID.StringMusic;
		StartCoroutine("StringMusicEncounter");
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
	}

	IEnumerator StringMusicEncounter()
	{
		switch (_musicPilesDefeated)
		{
		case 0:
			this._rateOfVolumeDecrease = this.noPilesDefeatedVolume;
			break;
		case 1:
			this._rateOfVolumeDecrease = this.onePilesDefeatedVolume;
			break;
		case 2:
			this._rateOfVolumeDecrease = this.allPilesDefeatedVolume;
			break;
		}
		while (!this._string)
		{
			if (GameDirector.instance.GetVolume(AudioID.StringMusic) < GameDirector.instance.GetPuzzleWinVolume())
			{
				GameDirector.instance.SubtractVolume(AudioID.StringMusic, this._rateOfVolumeDecrease);
			}
			else
			{
				this.StringPileDone();
				GameDirector.instance.ChangeVolume(AudioID.StringMusic, GameDirector.instance.GetPuzzleWinVolume());
			}
			yield return null;
		}
	}

	public void StringPileDone()
	{
		this._string = true;
	}

	public void WindMusicStart()
	{
		this._currentMusic = AudioID.WindMusic;
		StartCoroutine("WindMusicEncounter");
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
	}

	IEnumerator WindMusicEncounter()
	{
		switch (_musicPilesDefeated)
		{
		case 0:
			this._rateOfVolumeDecrease = this.noPilesDefeatedVolume;
			break;
		case 1:
			this._rateOfVolumeDecrease = this.onePilesDefeatedVolume;
			break;
		case 2:
			this._rateOfVolumeDecrease = this.allPilesDefeatedVolume;
			break;
		}
		while (!this._wind)
		{
			if (GameDirector.instance.GetVolume(AudioID.WindMusic) < GameDirector.instance.GetPuzzleWinVolume())
			{
				GameDirector.instance.SubtractVolume(AudioID.WindMusic, this._rateOfVolumeDecrease);
			}
			else
			{
				this.WindPileDone();
				GameDirector.instance.ChangeVolume(AudioID.WindMusic, GameDirector.instance.GetPuzzleWinVolume());
			}
			yield return null;
		}
	}

	public void WindPileDone()
	{
		this._wind = true;
	}

	public void FightingBoss()
	{
		GameDirector.instance.AddVolume(this._currentMusic, this._rateOfVolumeIncrease);
	}

	public void Cower()
	{
		this._musicPilesDefeated++;
		if (this._musicPilesDefeated == 2)
		{
			this._musicController.OrganTileActive();
		}
		if (this._musicPilesDefeated > 3)
		{
			Destroy(this);
		}
	}
}
