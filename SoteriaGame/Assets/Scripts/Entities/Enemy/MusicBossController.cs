using UnityEngine;
using System.Collections;

public class MusicBossController : MonoBehaviour
{
	private bool _brass;
	private bool _string;
	private bool _wind;

	void Start()
	{
		this._brass = false;
		this._string = false;
		this._wind = false;
	}

	public void BrassMusicStart()
	{
		this._brass = true;
	}

	IEnumerator BrassMusicEncounter()
	{
		while (
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
