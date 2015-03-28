using UnityEngine;
using System.Collections;

public class GUISoteriaStatue : MonoBehaviour {

	public float xOffset;
	public float yOffset;
	public Texture statue;
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

	private void OnGUI()
	{
		if (GUI.Button (new Rect (xOffset, Screen.height - yOffset, 100, 100), statue)) 
		{
			safetyLight.enabled = true;
			Vector3 normal = player.forward;
			//Debug.Log ("Player pos: " + player.position);
			//Debug.Log ("Player normal: " + player.forward);
			normal.Normalize();
			//Debug.Log ("Normalized: " + normal);
			safetyLight.transform.position = new Vector3(5 * normal.x + player.position.x, lightOffset, 5 * normal.z + player.position.z);
			//Debug.Log ("Light pos: " + safetyLight.transform.position);
            player.GetComponent<PCController>().EnableStandardMovement();
            player.GetComponent<EncounterMovementController>().CheckEscape();
		}
	}
}