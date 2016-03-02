using UnityEngine;
using System.Collections;

public class SewerCamera : MonoBehaviour
{
	public Transform target;
	public float smooth;
	public Vector3 CameraOffset;
	public float minZ;
	public float maxZ;
	
	// Use this for initialization
	void Start () {
		if (target == null)
			target = GameObject.FindWithTag ("Player").transform;
		minZ = -33.0f;
		//maxZ = 19.0f;
	}
	
	void Update () 
	{
		Vector3 targetposition = new Vector3 (0.0f, 0.0f, this.target.position.z) + CameraOffset;
		if (targetposition.z <= minZ)
		{
			targetposition.z = minZ;
		}		
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}