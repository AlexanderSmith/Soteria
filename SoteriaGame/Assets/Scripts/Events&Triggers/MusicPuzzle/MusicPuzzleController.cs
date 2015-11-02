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

	public Transform organDown;
	public Transform organUp;
	public Transform brassDown;
	public Transform brassUp;
	public Transform stringDown;
	public Transform stringUp;
	public Transform windDown;
	public Transform windUp;

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
		this.LerpOrganTileDown();
	}

	void LerpOrganTileDown()
	{

	}
}
