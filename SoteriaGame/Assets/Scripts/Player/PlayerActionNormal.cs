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

		moveVect = (_camForward * _vertical) + (_camRight * _horizontal);
		moveVect.y = 0;
		inPlayer.gameObject.transform.rigidbody.MovePosition((moveVect * inPlayer.moveSpeed * Time.deltaTime) + inPlayer.gameObject.transform.rigidbody.position);

		if (moveVect != Vector3.zero)
		{
			Quaternion targetRot = Quaternion.LookRotation (moveVect);
			Quaternion rot = Quaternion.Lerp (inPlayer.gameObject.transform.rigidbody.rotation, targetRot, inPlayer.rotationSpeed * Time.deltaTime);
			inPlayer.gameObject.transform.rigidbody.MoveRotation (rot);
		}
	}
}