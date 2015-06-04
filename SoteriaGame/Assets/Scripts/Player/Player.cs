using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour 
{
	public float moveSpeed = 0.15f;
	public float rotationSpeed = 20.0f;
	private Animator _animator;
	public float MoveAngleCorrection = 45.0f;
	private Vector3 _direction;

	// Use this for initialization
	void Start () 
	{
		this._animator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this._animator.SetBool("Moving", false);
		this.ApplyDirection();
	}

	public void Move()
	{
		Vector3 Direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		Direction = Quaternion.AngleAxis (this.MoveAngleCorrection, Vector3.up) * Direction;
	
		this.ApplyMovement(Direction);

		this._animator.SetBool("Moving", true);
	}
	
	public void ApplyMovement(Vector3 inDirection)
	{
		this.transform.rigidbody.velocity = new Vector3(0, 0, 0);
		this.transform.rigidbody.MovePosition((inDirection * this.moveSpeed) + this.transform.rigidbody.position);
		this.transform.rigidbody.position = new Vector3( this.transform.rigidbody.position.x, 0.5f, this.transform.rigidbody.rigidbody.position.z);

		this._direction = inDirection;
	}

	private void ApplyDirection()
	{
		if (this._direction != Vector3.zero)
		{	
			this.transform.rotation = Quaternion.Slerp(this.transform.rigidbody.rotation, Quaternion.LookRotation(this._direction),Time.deltaTime * 20.0f);
		}
	}

	public void BeginLingering()
	{
		this._animator.SetBool("PreOvercome", true);
	}

	public void ResetLinger()
	{
		this._animator.SetBool("PreOvercome", false);
	}
}