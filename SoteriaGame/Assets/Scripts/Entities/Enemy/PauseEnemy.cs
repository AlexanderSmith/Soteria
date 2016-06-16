using UnityEngine;
using System.Collections;

public class PauseEnemy : MonoBehaviour
{
	private bool _canMove;

	void Awake()
	{
		this._canMove = true;
	}

	void Update()
	{
		if(!this._canMove)
		{
			this.PauseThis();
		}
	}

	private void PauseThis()
	{
		this.gameObject.GetComponent<NavMeshAgent>().Stop();
	}

	public void Pause()
	{
		this._canMove = false;
	}

	public void Resume()
	{
		this._canMove = true;
		this.gameObject.GetComponent<NavMeshAgent>().Resume();
	}
}
