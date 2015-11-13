using UnityEngine;
using System.Collections;

public class CardRotation : MonoBehaviour
{
	void Update()
	{
		this.transform.RotateAround(this.transform.position, Vector3.up, .5f);
	}
}
