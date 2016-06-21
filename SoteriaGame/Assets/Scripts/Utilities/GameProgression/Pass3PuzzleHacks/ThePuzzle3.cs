using UnityEngine;
using System.Collections;

public class ThePuzzle3 : MonoBehaviour
{
	void Start()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			GameDirector.instance.TheaterPuzzleVisitedSuit();
		}
	}
}