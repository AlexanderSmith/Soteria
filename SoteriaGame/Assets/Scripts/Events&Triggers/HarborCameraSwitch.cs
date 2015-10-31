using UnityEngine;
using System.Collections;

public class HarborCameraSwitch : MonoBehaviour
{
	private Camera harborCam;
	private Camera flyThroughCam;
	private GameObject swarm;

	void Start()
	{
		harborCam = GameObject.Find("HarborTheaterDistrictCamera").GetComponent<Camera>();
		flyThroughCam = GameObject.Find("FlyThroughCamera").GetComponent<Camera>();
		harborCam.enabled = true;
		flyThroughCam.enabled = false;
		flyThroughCam.GetComponent<FlyThroughSoteriaStatue>().enabled = false;
		swarm = GameObject.FindWithTag("Swarm");
		swarm.SetActive(false);
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			flyThroughCam.enabled = true;
			flyThroughCam.GetComponent<FlyThroughSoteriaStatue>().enabled = true;
			flyThroughCam.GetComponent<FlyThroughSoteriaStatue>().Initialize(this);
			harborCam.enabled = false;
		}
	}

	void OnTriggerExit()
	{
		swarm.SetActive(true);
	}

	public void SwitchToHarbor()
	{
		harborCam.enabled = true;
		flyThroughCam.enabled = false;
		flyThroughCam.GetComponent<FlyThroughSoteriaStatue>().enabled = false;
	}
}
