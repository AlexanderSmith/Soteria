using UnityEngine;
using System.Collections;

public class CreditsTrigger : MonoBehaviour
{
	public void EndGame()
	{
		Application.LoadLevel("Credits");
	}
}