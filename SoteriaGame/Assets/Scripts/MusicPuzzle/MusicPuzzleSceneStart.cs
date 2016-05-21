using UnityEngine;
using System.Collections;

public class MusicPuzzleSceneStart : MonoBehaviour
{
	public GameObject playerPrefab;
	public Transform playerSpawnLoc;

	private GameObject _controller;
	private GameObject _key;
	private GameObject _oMalley;

	private WorldFlags worldFlagState 	= WorldFlags.EMPTY_FLAG;

	// Instantiate Player prefab based on progression using GameDirector
	void Awake()
	{
		GameObject player = Instantiate(playerPrefab, playerSpawnLoc.position, playerSpawnLoc.rotation) as GameObject;
		GameDirector.instance.InitializePlayer();
	}
	
	void Start()
	{
		// These won't need to be here during normal game play
//		GameDirector.instance.SuitRemoved();
//		GameDirector.instance.TokenTrue();
//		GameDirector.instance.CompassTrue();
//		GameDirector.instance.LanternTrue();

		worldFlagState = ProgressionManager.instance.Flags_World;

		if (!FlagTools.World_CheckFlag(worldFlagState, WorldFlags.MUSIC_DEFEATED))
		{
			GameDirector.instance.ChangeObjective(GameObject.Find("KeyPiece"));
			_controller = GameObject.Find("MusicPuzzleControl");
			_controller.GetComponent<MusicPuzzleController>().Initialize();
		}
		else
		{
			GameDirector.instance.PlayAudioClip(AudioID.OrganMusicComplete);
			GameDirector.instance.ChangeVolume(AudioID.OrganMusicComplete, 1.0f);
			_key = GameObject.Find("KeyPiece");
			_oMalley = GameObject.Find("O'MalleySuitOnMusicPuzzle");
			_key.SetActive(false);
			_oMalley.SetActive(false);
		}
	}
}