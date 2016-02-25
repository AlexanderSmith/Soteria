using UnityEngine;
using System.Collections;

public class OpenBossEye : MonoBehaviour
{
	public GameObject openedEye;
	public GameObject closedEye;
	private bool _active;
	private Color openedLidColor;
	private Color openedEyeColor;
	private Color closedLidColor;
	private Color closedEyeColor;

	private Color fullAlpha;
	private Color noAlpha;

	Material[] opened;
	Material[] closed;

	Timer puppetEye;
	float time;

	void Start()
	{
		fullAlpha = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		time = 5.0f;
		opened = openedEye.GetComponentInChildren<MeshRenderer>().materials;
		closed = closedEye.GetComponentInChildren<MeshRenderer>().materials;
		openedLidColor = new Color(.5f, .5f, .5f, 0.0f);
		openedEyeColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		closedLidColor = new Color(.5f, .5f, .5f, 1.0f);
		closedEyeColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

		puppetEye = TimerManager.instance.Attach(TimersType.PuppetEyeTimer);
		if (!GameDirector.instance.GetPuppetActivated())
		{
			opened[0].color = openedEyeColor;
			opened[1].color = openedLidColor;
			closed[0].color = closedEyeColor;
			closed[1].color = closedLidColor;
		}
		else
		{
			opened[0].color = closedEyeColor;
			opened[1].color = closedLidColor;
			closed[0].color = openedEyeColor;
			closed[1].color = openedLidColor;
		}
	}

	void Update()
	{
		if (puppetEye.IsStarted())
		{
			if (puppetEye.ElapsedTime() < time)
			{
				opened[1].color = Vector4.Lerp (opened[1].color, Color.gray, Time.deltaTime);
				opened[0].color = Vector4.Lerp (opened[0].color, Color.white, Time.deltaTime);
				closed[1].color = Vector4.Lerp (closed[1].color, Color.gray - fullAlpha, Time.deltaTime);
				closed[0].color = Vector4.Lerp (closed[0].color, Color.white - fullAlpha, Time.deltaTime);
			}
			else
			{
				puppetEye.StopTimer();
			}
		}
	}

	public void ShowOpenEye()
	{
		puppetEye.StartTimer();
	}
}