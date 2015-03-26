using UnityEngine;
using System.Collections;

public class CollisionEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionExit(Collision coll)
    {
//        Debug.Log("HIT " + coll.gameObject.name);
        rigidbody.velocity = new Vector3(0, 0, 0);
    }
}
