using UnityEngine;
using System.Collections;

public class TutorialEnemy : MonoBehaviour
{
	private Animator _anim;

	// Use this for initialization
	void Start()
	{
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	public void OP()
	{
		_anim.SetBool ("Aggro", true);
		_anim.SetBool ("Overpower", true);
	}

	public void OP2()
	{
		_anim.SetBool ("OP 2", true);
	}

	public void OP3()
	{
		_anim.SetBool ("OP 3", true);
	}

	public void Cower()
	{
		_anim.SetBool ("Cower", true);
	}

	public void ResetOverpower()
	{
		_anim.SetBool ("Overpower", false);
		_anim.SetBool ("OP 2", false);
		_anim.SetBool ("OP 3", false);
	}
}