using UnityEngine;
using System.Collections;

public class MusicDistrictCamera : MonoBehaviour {
	
	public Transform target;
	public float smooth;
	public Vector3 CameraOffset;
	public float minZ;
	private float minY;
	
	// Use this for initialization
	void Start () {
		if (target == null)
			target = GameDirector.instance.GetPlayer().gameObject.transform;
		minZ = -64.0f;
		minY = this.CameraOffset.y + target.transform.position.y;
	}
	
	// Update is called once per frame
	
	
	void Update () 
	{
		Vector3 targetposition = new Vector3 (this.target.transform.position.x, this.target.transform.position.y, this.target.position.z) + CameraOffset;
		if (targetposition.z <= minZ)
		{
			targetposition.z = minZ;
		}

		if (targetposition.y <= minY)
		{
			targetposition.y = minY;
		}
		
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}