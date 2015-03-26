using UnityEngine;
using System.Collections;

public class PCController : MonoBehaviour {

	public float moveSpeed;
	public int numSoteriaStatues;
	public Object soteriaStatuePrefab;
	public int fearResidue;
    public float playerRotation;
    public float forcedRotation;
	public float gameOverTimer;

	Quaternion overwhelmedRotation;
	Movement myMovementComponents;

	enum State
	{
		Normal,
		Overwhelmed
	};
	State currentState;

	// Use this for initialization
	void Start () {
        myMovementComponents = new Movement();
		currentState = State.Normal;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState == State.Normal)
        {
            myMovementComponents.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), moveSpeed, this.transform);
        }
		
		if (currentState == State.Overwhelmed)
		{
			//Space bar smash and constant rotation applied during overwhelmed state
			//		Refactored Space bar smash to click to rotate
			//Debug.Log("Current rotation: " + this.transform.rotation);
			if (this.transform.rotation != overwhelmedRotation)
			{
				this.transform.RotateAround(this.transform.position, Vector3.up, forcedRotation);
			}
			GameOverTimer ();
		}
	}

	void OnMouseDown()
	{
		this.transform.RotateAround(this.transform.position, Vector3.up, playerRotation);
	}

	void Overwhelm(Transform enemy)
	{
		if (currentState == State.Normal)
		{
			overwhelmedRotation = enemy.rotation;
			this.transform.rotation = overwhelmedRotation;
			currentState = State.Overwhelmed;
			CheckEscape ();
		}
	}

	void CheckEscape()
	{
		if (numSoteriaStatues > 0)
		{
			numSoteriaStatues -= 1;
			//Create Soteria Statue prefab if player has statues in inventory
            Vector3 normal = this.transform.forward;
            normal.Normalize();
            GameObject statue = Instantiate(soteriaStatuePrefab, new Vector3(5 * normal.x + this.transform.position.x, this.transform.position.y, 5 * normal.z + this.transform.position.z), this.transform.rotation) as GameObject;
            //Debug.Log("normal " + normal);
		}
	}

	void GameOverTimer()
	{
		gameOverTimer -= Time.deltaTime;
		if (gameOverTimer <= 0)
		{
			PlayerOverwhelmed();
		}
	}

	void PlayerOverwhelmed()
	{
		Application.LoadLevel("Basic");
	}
}
