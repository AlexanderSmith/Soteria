using UnityEngine;
using System.Collections;

public class HubSuitObjective : MonoBehaviour
{
	private GameObject _oMalleySuitOff;
	private GameObject _oMalleyStatue;

	public GameObject hubToMusic;
	public GameObject hubToTheater;
	public GameObject hubToObservatory;
	public GameObject oMalley;
	public GameObject soteriaStatue;

	private bool _music;
	private bool _theater;
	private bool _observatory;
	private bool _toMusic;
	private bool _toTheater;
	private bool _toObservatory;

	/*****************************************************************************
	Objective Determination Logic
	if _music false => objective = hubToMusic
	if _music true & _theater false => objective = hubToTheater
	if _music & _theater true & _observatory false => objective = hubToObservatory
	if all true => objective = oMalley
	*****************************************************************************/
	void Awake()
	{
		this._oMalleySuitOff = GameObject.Find("O'MalleySuitOff");
		this._oMalleyStatue = GameObject.Find("O'MalleySoteriaStatue");
		this._oMalleyStatue.SetActive(false);

		// Testing code
//		GameDirector.instance.ChangeGameState(GameStates.Suit);
//		this._music = true;
//		this._theater = true;
//		this._observatory = true;

		// Testing statue final crumble code
//		GameDirector.instance.StatueCrumbleTwo();
	}

	void Start ()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			DetermineObjective();
		}
		else
		{
			this._oMalleySuitOff.SetActive(false);
			if (GameDirector.instance.IsTutorialComplete())
			{
				DeterminePass4Objective();
			}

			if (GameDirector.instance.GetStatueCrumble() == StatueCrumbleState.CRUMBLETHREE)
			{
				this._oMalleyStatue.SetActive(true);
			}
		}
	}

	void DetermineObjective()
	{
		this._music = GameDirector.instance.GetMusicPuzzleVisitedSuit();
		this._theater = GameDirector.instance.GetTheaterPuzzleVisitedSuit();
		this._observatory = GameDirector.instance.GetObservatoryPuzzleVisitedSuit();

		this._toMusic = !this._music;
		this._toTheater = this._music && !this._theater;
		this._toObservatory = this._music && this._theater && !this._observatory;

		if (this._toMusic)
		{
			GameDirector.instance.ChangeObjective(hubToMusic);
			this._oMalleySuitOff.SetActive(false);
			return;
		}

		if (this._toTheater)
		{
			GameDirector.instance.ChangeObjective(hubToTheater);
			this._oMalleySuitOff.SetActive(false);
			return;
		}

		if (this._toObservatory)
		{
			GameDirector.instance.ChangeObjective(hubToObservatory);
			this._oMalleySuitOff.SetActive(false);
			return;
		}

		GameDirector.instance.ChangeObjective(oMalley);
	}

	void DeterminePass4Objective()
	{
		this._music = GameDirector.instance.IsMusicDefeated();
		this._theater = GameDirector.instance.IsTheaterDefeated();
		this._observatory = GameDirector.instance.IsObservatoryDefeated();

		this._toMusic = !this._music;
		this._toTheater = this._music && !this._theater;
		this._toObservatory = this._music && this._theater && !this._observatory;

		if (GameDirector.instance.GetCompass())
		{
			if (this._toMusic)
			{
				GameDirector.instance.ChangeObjective(hubToMusic);
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
			
			GameDirector.instance.ChangeObjective(soteriaStatue);
		}
	}
}