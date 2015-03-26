using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {
	
	static public bool Click(GameObject amIClicked)
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			if (hit.collider == amIClicked.collider)
			{
				Debug.Log(amIClicked.name);
				return true;
			}
			
		}
		return false;
	}
	
	static public bool ClickWithMouse(GameObject amIClicked)
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			if (hit.collider == amIClicked.collider)
			{
				return true;
			}
			
		}
		return false;
	}
	
	static public Vector3 ClickWithMouseReturnVec(GameObject amIClicked)
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Casts the ray and get the first game object hit
			Physics.Raycast(ray, out hit);
			if (hit.collider == amIClicked.collider)
			{
				return hit.point;
			}
			
		}
		return Vector3.zero;
	}
}
