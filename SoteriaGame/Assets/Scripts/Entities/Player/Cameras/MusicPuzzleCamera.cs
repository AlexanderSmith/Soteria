using UnityEngine;
using System.Collections;

public class MusicPuzzleCamera : MonoBehaviour
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
		minZ = -32.5f;
		maxZ = 19.0f;
	}

	void Update () 
	{
		Vector3 targetposition = new Vector3 (this.target.transform.position.x, this.target.transform.position.y, this.target.position.z) + CameraOffset;
		if (targetposition.z <= minZ)
		{
			targetposition.z = minZ;
		}
		if (targetposition.z >= maxZ)
		{
			targetposition.z = maxZ;
		}
		
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}