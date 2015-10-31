using UnityEngine;
using System.Collections;

public class FlyThroughSoteriaStatue : MonoBehaviour
{
	private GameObject _soteriaStatue;
	private GameObject _lookLoc;
	public float distance = 15.0f;
	private HarborCameraSwitch _cameraSwitch;
	public float time = 5f;

	public void Initialize(HarborCameraSwitch camSwitch)
	{
		_soteriaStatue = GameObject.Find ("SotStat");
		_lookLoc = GameObject.Find ("CameraShift");
		_cameraSwitch = camSwitch;
		this.transform.position = new Vector3(this.transform.position.x,
		                                      GameDirector.instance.GetPlayer().transform.position.y,
		                                      GameDirector.instance.GetPlayer().transform.position.z) + new Vector3 (0, 15, -15);
		this.transform.LookAt(GameDirector.instance.GetPlayer().transform);
	}

	void Update()
	{
		if (this.gameObject.GetComponent<FlyThroughSoteriaStatue>().enabled == true)
		{
			if (time > 0)
			{
				time -= Time.deltaTime;
				Quaternion rotation = Quaternion.LookRotation(_soteriaStatue.transform.position - this.transform.position);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime);
				GameDirector.instance.GetPlayer().PlayerActionPause();
			}
			else
			{
				_cameraSwitch.SwitchToHarbor();
				GameDirector.instance.GetPlayer().PlayerActionNormal();
			}
		}
	}
}
