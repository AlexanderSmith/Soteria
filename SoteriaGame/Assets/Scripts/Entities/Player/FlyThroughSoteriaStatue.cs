using UnityEngine;
using System.Collections;

public class FlyThroughSoteriaStatue : MonoBehaviour
{
	private GameObject _soteriaStatue;
	public float distance = 15.0f;
	private HarborCameraSwitch _cameraSwitch;

	public void Initialize(HarborCameraSwitch camSwitch)
	{
		_soteriaStatue = GameObject.Find ("SotStat");
		_cameraSwitch = camSwitch;
	}

	void Update()
	{
		if (this.gameObject.GetComponent<FlyThroughSoteriaStatue>().enabled == true)
		{
			transform.LookAt (_soteriaStatue.transform);
			if (Vector3.Distance(this.transform.position, _soteriaStatue.transform.position) > distance)
			{
				this.transform.position = Vector3.Lerp (this.transform.position, _soteriaStatue.transform.position, .1f * Time.deltaTime);
			}
			else
			{
				_cameraSwitch.SwitchToHarbor();
			}
		}
	}
}
