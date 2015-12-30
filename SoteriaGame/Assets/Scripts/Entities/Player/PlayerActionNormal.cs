using UnityEngine;

public class PlayerActionNormal : IPlayerAction
{
	private Vector3 _camForward;
	private Vector3 _camRight;
	private float _horizontal;
	private float _vertical;
	private Vector3 moveVect;

	public void PlayerAction(Player inPlayer)
	{
		_camForward = Camera.main.transform.forward;
		_camRight = Camera.main.transform.right;
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");

		if (!_horizontal.Equals(0) || !_vertical.Equals(0))
		{
			inPlayer.Moving();
		}

		moveVect = (_camForward * _vertical) + (_camRight * _horizontal) * Time.deltaTime * inPlayer.moveSpeed;
		moveVect.y = 0;	



		if (moveVect != Vector3.zero)
		{
			//Debug.Log( Quaternion.LookRotation (moveVect) );
			Quaternion targetRot = Quaternion.LookRotation (moveVect);
			Vector3 currentRotation =  inPlayer.gameObject.transform.rotation * Vector3.forward;
			Vector3 rot = Vector3.Lerp (currentRotation, moveVect, inPlayer.rotationSpeed);
			inPlayer.transform.rotation = Quaternion.LookRotation(rot, Vector3.up);
		}


		float snapDistance = 1.0f; //Adjust this based on the CharacterController's height and the slopes you want to handle - my CharacterController's height is 1.8
	
		if (inPlayer.gameObject.GetComponent<CharacterController>().isGrounded == false)
		{
			moveVect.y -= 10.0f; // quick Hack it kind of works but raycasting would be better (maybe?)

			//RaycastHit hitInfo = new RaycastHit();
			//if (Physics.Raycast(new Ray(inPlayer.transform.position, Vector3.down), out hitInfo, snapDistance))
			//	inPlayer.gameObject.GetComponent<CharacterController>().Move(hitInfo.point - inPlayer.transform.position);

			//Debug.DrawRay(inPlayer.transform.position, Vector3.down);
		}

		inPlayer.gameObject.GetComponent<CharacterController>().Move (moveVect );
	}

	public void InitializeValues(Player inPlayer)
	{
	}
}