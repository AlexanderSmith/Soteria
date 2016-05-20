using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	private bool _playerVisible;
	public float fieldOfVision = 90.0f;
	private SphereCollider _sphereCollider;
	public float eyeHeightOffset;

	// Use this for initialization
	void Start ()
	{
		this._sphereCollider = GetComponent<SphereCollider> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerStay(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._playerVisible = false;
			
			Vector3 direction = player.transform.position - this.gameObject.transform.position;
			float angle = Vector3.Angle(direction, this.gameObject.transform.forward);
			
			if(angle < fieldOfVision * 0.5f)
			{
				RaycastHit hit;
				
				if(Physics.Raycast(this.gameObject.transform.position + (eyeHeightOffset * this.gameObject.transform.up), direction, out hit, this._sphereCollider.radius))
				{
					if(hit.collider.gameObject.Equals(player.gameObject) && !GetComponent<BasicEnemyController>().GetStunStatus())
					{
						this._playerVisible = true;
						this.GetComponent<BasicEnemyController>().VisibleAction();
					}
				}
				//Debug.DrawRay(this.gameObject.transform.position + (eyeHeightOffset * this.gameObject.transform.up), direction, Color.white, 200, false);
			}
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			this._playerVisible = false;
			this.GetComponent<BasicEnemyController>().NotVisibleAction();
		}
	}

	public bool IsPlayerVisible()
	{
		return this._playerVisible;
	}
}
