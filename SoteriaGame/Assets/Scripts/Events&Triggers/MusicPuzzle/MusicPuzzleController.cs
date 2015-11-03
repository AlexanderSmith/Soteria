using UnityEngine;
using System.Collections;

public class MusicPuzzleController : MonoBehaviour
{
	private bool _brassPlaying;
	private bool _stringPlaying;
	private bool _windPlaying;
	private bool _organPlaying;

	public GameObject organTile;
	public GameObject brassTile;
	public GameObject stringTile;
	public GameObject windTile;
	public GameObject boss;

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

	// Use this for initialization
	void Start ()
	{
		this._brassPlaying = false;
		this._stringPlaying = false;
		this._windPlaying = false;
		this._organPlaying = false;
	}

	public void Initialize()
	{
		if (GameDirector.instance.GetMusicActivated())
		{
			this.organTile.transform.position = this.organDown.position;
			this.brassTile.transform.position = this.brassUp.position;
			this.stringTile.transform.position = this.stringUp.position;
			this.windTile.transform.position = this.windUp.position;
		}
		else
		{
			this.organTile.transform.position = this.organUp.position;
			this.brassTile.transform.position = this.brassDown.position;
			this.stringTile.transform.position = this.stringDown.position;
			this.windTile.transform.position = this.windDown.position;
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
			this.boss.transform.position = Vector3.Lerp (this.boss.transform.position, this.bossUp.position, (Time.time - start) / lerpTime);
			yield return null;
		}
		this.organTile.transform.position = this.organDown.position;
		this.brassTile.transform.position = this.brassUp.position;
		this.stringTile.transform.position = this.stringUp.position;
		this.windTile.transform.position = this.windUp.position;
		this.boss.transform.position = this.bossUp.position;
		GameDirector.instance.ChangeVolume(AudioID.OrganMusic, 0.0f);
	}

	public GameObject GetBoss()
	{
		return this.boss;
	}
}
