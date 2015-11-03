using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	public float noPilesDefeatedVolume;
	public float onePilesDefeatedVolume;
	public float allPilesDefeatedVolume;
	private float _rateOfVolumeDecrease;
	private int _musicPilesDefeated;

	private bool _brass;
	private bool _string;
	private bool _wind;

	void Start()
	{
		noPilesDefeatedVolume = .01f;
		onePilesDefeatedVolume = .02f;
		allPilesDefeatedVolume = .03f;
		this._musicPilesDefeated = 0;
		this._brass = false;
		this._string = false;
		this._wind = false;
	}

	public void BrassMusicStart()
	{
		this._brass = true;
		StartCoroutine("BrassMusicEncounter");
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
		while (GameDirector.instance.GetVolume(AudioID.BrassMusic) > 0 || GameDirector.instance.GetVolume(AudioID.BrassMusic) < .15f)
		{
			GameDirector.instance.SubtractVolume(AudioID.BrassMusic, this._rateOfVolumeDecrease);
			yield return null;
		}
	}

	public void StringMusicStart()
	{
		this._string = true;
	}

	public void WindMusicStart()
	{
		this._wind = true;
	}
}
