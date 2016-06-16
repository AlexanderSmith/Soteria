using UnityEngine;
using System.Collections;

public class HubCamera : MonoBehaviour
{
	public Transform player;
	public float smooth;
	public Vector3 CameraOffset;
	private float _minX;
	private float _maxX;
	private float _minZ;
	
	// Use this for initialization
	void Start () {
		if (player == null)
			player = GameObject.FindWithTag("Player").transform;
		_minX = -38.0f;
		_maxX = 38.0f;
		_minZ = -40.0f;
	}
	
	// Update is called once per frame
	
	
	void Update () 
	{
		Vector3 targetposition = new Vector3 (this.player.transform.position.x, this.player.transform.position.y, this.player.transform.position.z) + CameraOffset;

		if (targetposition.x <= _minX)
		{
			targetposition.x = _minX;
		}
		else if (targetposition.x >= _maxX)
		{
			targetposition.x = _maxX;
		}

		if (targetposition.z <= _minZ)
		{
			targetposition.z = _minZ;
		}
			
		this.transform.position = Vector3.Lerp (this.transform.position, targetposition, Time.deltaTime * smooth);
	}
}