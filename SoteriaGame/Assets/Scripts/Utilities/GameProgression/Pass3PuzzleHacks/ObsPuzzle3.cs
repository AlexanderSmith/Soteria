using UnityEngine;
using System.Collections;

public class ObsPuzzle3 : MonoBehaviour
{
	void Start()
	{
		if (GameDirector.instance.GetGameState() == GameStates.Suit)
		{
			GameDirector.instance.ObservatoryPuzzleVisitedSuit();
		}
	}
}