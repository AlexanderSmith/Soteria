using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    Movement myMovementComponents;
	// Use this for initialization
	void Start () {
        myMovementComponents = new Movement();
	}
	
	// Update is called once per frame
	void Update () {
        myMovementComponents.Move(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"), moveSpeed, this.transform);
	}
}
