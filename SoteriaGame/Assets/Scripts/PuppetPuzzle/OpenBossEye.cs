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
//		fullAlpha = new Color (0.0f, 0.0f, 0.0f, 1.0f);
//		time = 5.0f;
//		opened = openedEye.GetComponentInChildren<MeshRenderer>().materials;
//		closed = closedEye.GetComponentInChildren<MeshRenderer>().materials;
//		openedLidColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
//		openedEyeColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
//		closedLidColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
//		closedEyeColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
//
//		puppetEye = TimerManager.instance.Attach(TimersType.PuppetEyeTimer);
//		if (!GameDirector.instance.GetPuppetActivated())
//		{
//			opened[0].color = opened[0].color - fullAlpha;
//			opened[1].color = opened[1].color - fullAlpha;
//			opened[2].color = opened[2].color - fullAlpha;
////			closed[0].color = closed[0].color + closedEyeColor;
////			closed[1].color = closed[1].color + closedLidColor;
//		}
//		else
//		{
////			opened[0].color = opened[0].color + closedEyeColor;
////			opened[1].color = opened[1].color + closedLidColor;
//			closed[0].color = closed[0].color - fullAlpha;
//			closed[1].color = closed[1].color - fullAlpha;
//		}
	}

	void Update()
	{
//		if (puppetEye.IsStarted())
//		{
//			if (puppetEye.ElapsedTime() < time)
//			{
//				opened[0].color = Vector4.Lerp (opened[0].color, opened[0].color + fullAlpha, Time.deltaTime);
//				opened[1].color = Vector4.Lerp (opened[1].color, opened[1].color + fullAlpha, Time.deltaTime);
//				closed[0].color = Vector4.Lerp (closed[0].color, closed[0].color - fullAlpha, Time.deltaTime);
//				closed[1].color = Vector4.Lerp (closed[1].color, closed[1].color - fullAlpha, Time.deltaTime);
//			}
//			else
//			{
//				puppetEye.StopTimer();
//			}
//		}
	}

	public void ShowOpenEye()
	{
		//puppetEye.StartTimer();
	}
}