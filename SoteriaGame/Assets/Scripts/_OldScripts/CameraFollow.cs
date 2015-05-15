using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smooth;

	void Update () 
	{
		this.transform.position = Vector3.Lerp (this.transform.position, target.transform.position,
		              Time.deltaTime * smooth);
	}
}
