using UnityEngine;
using System.Collections;

public class StandardMovementController : MonoBehaviour {

    public float moveSpeed;
    public float playerRotation;

    Quaternion overwhelmedRotation;
    Movement myMovementComponents;
	Animator animator;

    // Use this for initialization
    void Start()
    {
        myMovementComponents = new Movement();
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
