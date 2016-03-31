using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	public float noPilesDefeatedVolume;
	public float onePilesDefeatedVolume;
	public float twoPilesDefeatedVOlume;
	public float allPilesDefeatedVolume;
	static public float _rateOfVolumeIncrease;
	private float _rateOfVolumeDecrease;
	private int _musicPilesDefeated;

	private AudioID _currentMusic;
	private bool _started;

	private string _currentPile;
	private string _musicSuck;
	private bool _fighting;

	private MusicPuzzleController _musicController;

	void Start()
	{
		this._musicPilesDefeated = 0;
	}

	public void HackStart(AudioID inAID)
	{
		this._musicController.StartAllMusicSuck();
		this._currentMusic = inAID;
		this._started = true;
		this._musicController.PuzzleStartHackBoss();
	}

	public void Initialize(MusicPuzzleController inMusPuzCont)
	{
		this._musicController = inMusPuzCont;
	}

	public void PuzzleStart()
	{
		//GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
		MusicStart(AudioID.OrganMusicComplete, "Organ");
	}

	public bool GetHackStart()
	{
		return this._started;
	}

	public void MusicStart(AudioID inAID, string inMusicPile)
	{
		this._currentMusic = inAID;
		this._fighting = false;
		this._currentPile = inMusicPile + "TileDown";
		this._musicSuck = inMusicPile;
		StartCoroutine("MusicEncounter", inAID);
		GameDirector.instance.MusicPuzzleEncounter(this.gameObject);
		this._musicController.MusicSuck(this._musicSuck);
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
			// BAD -- Temp fix to suck music away on start up. This needs to be looked at again later
			if (inAID == AudioID.OrganMusicComplete)
			{
				GameDirector.instance.SubtractVolumePuzzle(inAID, this._rateOfVolumeDecrease);
			}
			if (GameDirector.instance.GetVolume(inAID) < GameDirector.instance.GetPuzzleWinVolume() && inAID != AudioID.OrganMusicComplete)
			{
				GameDirector.instance.SubtractVolumePuzzle(inAID, this._rateOfVolumeDecrease);
			}
			else if (inAID != AudioID.OrganMusicComplete)
			{
				this.PileDone();
				GameDirector.instance.ChangeVolume(inAID, GameDirector.instance.GetPuzzleWinVolume());
			}
			yield return null;
		}
		// Bad -- Temp fix to ensure player leaving trigger zone because puzzle has started doesn't restart this function with different audio
		this._started = false;
	}

	public void PileDone()
	{
		this._fighting = true;
		this._musicController.TileDown(this._currentPile);
		this._musicController.KillMusicSuck(this._musicSuck);
		GameDirector.instance.GetPlayer().ResetLinger();
	}

	public void FightingBoss()
	{
		GameDirector.instance.AddVolumePuzzle(this._currentMusic, MusicBossController._rateOfVolumeIncrease);
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