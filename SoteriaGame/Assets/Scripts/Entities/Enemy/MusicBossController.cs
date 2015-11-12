using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	public float noPilesDefeatedVolume = .01f;
	public float onePilesDefeatedVolume = .02f;
	public float allPilesDefeatedVolume = .03f;
	public float _rateOfVolumeIncrease = .03f;
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
	}

	public void FightingBoss()
	{
		GameDirector.instance.AddVolume(this._currentMusic, this._rateOfVolumeIncrease);
	}

	public void Cower()
	{
		if (this._musicPilesDefeated == 2)
		{
			this._musicController.OrganTileActive();
		}
		if (this._musicPilesDefeated == 3)
		{
			Destroy(this.gameObject);
			GameDirector.instance.StopAudioClip(AudioID.OrganMusic);
			GameDirector.instance.StopAudioClip(AudioID.BrassMusic);
			GameDirector.instance.StopAudioClip(AudioID.StringMusic);
			GameDirector.instance.StopAudioClip(AudioID.WindMusic);
		}
		this._musicPilesDefeated++;
	}
}
