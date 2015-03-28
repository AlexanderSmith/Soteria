using UnityEngine;
using System.Collections;

public class GUISoteriaStatue : MonoBehaviour {

	public float xOffset;
	public float yOffset;
	public Light safetyLight;
	public Transform player;

	float lightOffset = 10;

	// Use this for initialization
	void Start () {
		Debug.Log ("Light pos: " + safetyLight.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
}