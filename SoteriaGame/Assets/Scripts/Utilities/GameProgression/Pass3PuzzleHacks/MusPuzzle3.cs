using UnityEngine;
using System.Collections;

public class MusPuzzle3 : MonoBehaviour
{
	void Start()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			GameDirector.instance.MusicPuzzleVisitedSuit();
		}
	}
}