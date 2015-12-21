using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	public float noPilesDefeatedVolume = .01f;
	public float onePilesDefeatedVolume = .02f;
	public float twoPilesDefeatedVOlume = .03f;
	public float allPilesDefeatedVolume = .04f;
	public float _rateOfVolumeIncrease = .03f;
	private float _rateOfVolumeDecrease;
	private int _musicPilesDefeated;

	private AudioID _currentMusic;

	private string _currentPile;
	private bool _fighting;

	private MusicPuzzleController _musicController;

	void Start()
	{
		this._musicPilesDefeated = 0;
	}

	public void Initialize(MusicPuzzleController inMusPuzCont)
	{
		this._musicController = inMusPuzCont;
	}

	public void PuzzleStart()
	{
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
	}

	public void MusicStart(AudioID inAID, string inMusicPile)
	{
		this._currentMusic = inAID;
		this._fighting = false;
		this._currentPile = inMusicPile + "TileDown";
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
            this._rateOfVolumeDecrease = this.twoPilesDefeatedVOlume;
            break;
        case 3:
            this._rateOfVolumeDecrease = this.allPilesDefeatedVolume;
            break;
		}
		while (!this._fighting)
		{
			if (GameDirector.instance.GetVolume(inAID) < GameDirector.instance.GetPuzzleWinVolume())
			{
				GameDirector.instance.SubtractVolumePuzzle(inAID, this._rateOfVolumeDecrease);
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
		this._musicController.TileDown(this._currentPile);
		GameDirector.instance.GetPlayer().ResetLinger();
	}

	public void FightingBoss()
	{
		GameDirector.instance.AddVolumePuzzle(this._currentMusic, this._rateOfVolumeIncrease);
	}

	public void Cower()
	{
		if (this._musicPilesDefeated == 2)
		{
			this._musicController.OrganTileActive();
		}
		if (this._musicPilesDefeated == 3)
		{
			this.gameObject.SetActive(false);
			this._musicController.PuzzleDefeated();
		}
		this._musicPilesDefeated++;
	}
}
