using UnityEngine;
using System.Collections;

public class HubSuitObjective : MonoBehaviour
{
	private GameObject _oMalleySuitOff;

	public GameObject hubToMusic;
	public GameObject hubToTheater;
	public GameObject hubToObservatory;
	public GameObject oMalley;

	private bool _music;
	private bool _theater;
	private bool _observatory;
	private bool _toMusic;
	private bool _toTheater;
	private bool _toObservatory;
	private bool _toOMalley;

	/*****************************************************************************
	Objective Determination Logic
	if _music false => objective = hubToMusic
	if _music true & _theater false => objective = hubToTheater
	if _music & _theater true & _observatory false => objective = hubToObservatory
	if all true => objective = oMalley
	*****************************************************************************/
	void Awake()
	{
		this._music = GameDirector.instance.GetMusicPuzzleVisitedSuit();
		this._theater = GameDirector.instance.GetTheaterPuzzleVisitedSuit();
		this._observatory = GameDirector.instance.GetObservatoryPuzzleVisitedSuit();
		this._oMalleySuitOff = GameObject.Find("O'MalleySuitOff");
	}

	void Start ()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			DetermineObjective(this._music, this._theater, this._observatory);
		}
		else
		{
			this._oMalleySuitOff.SetActive(false);
		}

//		// Testing code
//		GameDirector.instance.ChangeGameState(GameStates.Suit);
//		this._music = true;
//		this._theater = true;
//		this._observatory = true;
//		DetermineObjective(this._music, this._theater, this._observatory);
	}

	void DetermineObjective(bool inMusic, bool inTheater, bool inObservatory)
	{
		this._toMusic = !this._music;
		this._toTheater = this._music && !this._theater;
		this._toObservatory = this._music && this._theater && !this._observatory;
		//this._toOMalley = this._music && this._theater && this._observatory;

		if (this._toMusic)
		{
			GameDirector.instance.ChangeObjective(hubToMusic);
			this._oMalleySuitOff.SetActive(false);
			return;
		}

		if (this._toTheater)
		{
			GameDirector.instance.ChangeObjective(hubToTheater);
			return;
		}

		if (this._toObservatory)
		{
			GameDirector.instance.ChangeObjective(hubToObservatory);
			return;
		}

		// Necessary?
//		if (this._toOMalley)
//		{
//			GameDirector.instance.ChangeObjective(oMalley);
//			return;
//		}
		GameDirector.instance.ChangeObjective(oMalley);
	}
}
