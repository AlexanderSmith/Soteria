using UnityEngine;
using System.Collections;

public class CameraFollowIso : MonoBehaviour {
	
	public Transform target;
	public float smooth;
	public Vector3 CameraOffset;
	
	// Use this for initialization
	void Start () {
		if (target == null)
			target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	
	
	void Update () 
	{
		Vector3 targetposition = new Vector3 (this.target.transform.position.x, this.target.transform.position.y, this.target.position.z) + CameraOffset;
		
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}