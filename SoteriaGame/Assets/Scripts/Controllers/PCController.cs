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
	public int Coins;

	Quaternion overwhelmedRotation;
	Movement myMovementComponents;

	private Quaternion enemyRoation;

	enum State
	{
		Normal,
		Overwhelmed,
		Dead,
		Free
	};
	State currentState;

	// Use this for initialization
	void Start () {
        myMovementComponents = new Movement();
		currentState = State.Normal;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentState == State.Dead)
		{
			Debug.Log("MoveAway");
			Vector3 speed = new Vector3( 0.2f, 0.0f, 0.2f);
			speed.x *= this.transform.forward.x;
			speed.z *= this.transform.forward.z;
			
			this.transform.position += ( speed ) ;
		}

        if (currentState == State.Normal)
        {
            myMovementComponents.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), moveSpeed, this.transform);
        }
		
		if (currentState == State.Overwhelmed)
		{
			GameOverTimer();

			float angle = Quaternion.Angle(this.transform.rotation, overwhelmedRotation);

			if (angle > 5)
			{
				Debug.Log ("TOO LARGE -- TOO LARGE");
				this.transform.RotateAround(this.transform.position, Vector3.up, forcedRotation);
			}
		
			GameObject Enemy = GameObject.Find("Enemy");

			angle = 10;
			if  ( Vector3.Angle(this.transform.forward, Enemy.transform.position - this.transform.position) < angle) 
			{
				currentState = State.Free;
			}
		}

		if (currentState == State.Free)
		{
			Vector3 speed = new Vector3( 0.2f, 0.0f, 0.2f);
			speed.x *= this.transform.forward.x;
			speed.z *= this.transform.forward.z;
			
			this.transform.position += ( speed ) ;
		}
	}

	void OnMouseDown()
	{
		this.transform.RotateAround(this.transform.position, Vector3.up, playerRotation);
	}

	void Overwhelm(Transform enemy)
	{
		enemyRoation = enemy.rotation;
		if (currentState == State.Normal)
		{
			overwhelmedRotation = enemy.rotation;

			this.transform.rotation = overwhelmedRotation;
			Debug.Log(this.transform.rotation);
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
            GameObject statue = Instantiate(soteriaStatuePrefab, 
			                                new Vector3(10 * normal.x + this.transform.position.x, this.transform.position.y, 10 * normal.z + this.transform.position.z), 
			                                this.transform.rotation) as GameObject;
            //Debug.Log("normal " + normal);
		}
	}

	void GameOverTimer()
	{
		gameOverTimer -= Time.deltaTime;
		if (gameOverTimer <= 0)
		{
			overwhelmedRotation = enemyRoation;
			this.transform.rotation = overwhelmedRotation;
			GameObject Enemy = GameObject.Find("Enemy");
			Enemy.gameObject.SendMessage("EndEncounter", true);
			this.currentState = State.Dead;
		}
	}

	void PlayerOverwhelmed()
	{
		Application.LoadLevel("TileEventSystem");
	}

	void OnCollisionEnter (Collision other) 
	{
		if (other.gameObject.name.Equals("SoteriaStatue(Clone)"))
		{
			PlayerOverwhelmed();
		}

		if (other.gameObject.name.Equals("Enemy"))
		{
			Destroy(other.gameObject);
			this.currentState = State.Normal;
			GameObject Statue = GameObject.Find("SoteriaStatue(Clone)");
			if (Statue != null)
				Destroy(Statue);

			gameOverTimer = 10;
		}
	}
	void UseCoin()
	{
		if (Coins > 0)
		{
			GameObject Enemy = GameObject.Find("Enemy");
			if (Enemy != null)
				Destroy(Enemy.gameObject);
			this.currentState = State.Normal;
			GameObject Statue = GameObject.Find("SoteriaStatue(Clone)");
			if (Statue != null)
				Destroy(Statue);

			gameOverTimer = 10;
		}
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.button == 1 && e.isMouse)
			UseCoin();
	}

}
