using UnityEngine;
using System.Collections;

public class ObservatoryDistrictCamera : MonoBehaviour {
	
	public Transform target;
	public float smooth;
	public Vector3 CameraOffset;
	public float minZ;
	
	// Use this for initialization
	void Start ()
	{
		if (target == null)
			target = GameObject.FindWithTag ("Player").transform;
		minZ = -121.5f;
	}
	
	// Update is called once per frame
	
	
	void Update () 
	{
		Vector3 targetposition = new Vector3 (this.target.transform.position.x, this.target.transform.position.y, this.target.position.z) + CameraOffset;
		if (targetposition.z <= minZ)
		{
			targetposition.z = minZ;
		}
		
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}