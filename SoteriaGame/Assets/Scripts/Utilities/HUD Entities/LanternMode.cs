using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * public abstract class LanternMode
 * 
 * Strategy interface for current state of Lantern Button
 * LanternModeController.cs handles switch mechanism
 */
public abstract class LanternMode
{
	protected LanternModeController m_Owner;
	
	public LanternMode(LanternModeController controller)
	{
		this.m_Owner = controller;
	}
	
	//interface
	public abstract void VOnUpdate ();
	public abstract void VOnSwitch();
}

/*
 * public  class PulseLanternMode
 * 
 * Implementation of pulse mode for Lantern Button
 * Button will pulse up and down, can mess with intervals below
 */
public class PulseLanternMode : LanternMode
{
	private static readonly float PULSE_INTERVAL = 0.5f;
	
	private Light 	m_PulseLight;
	private Sprite m_NormalTexture;
	private Vector3 m_ScaleFactor;
	private float 	m_fTimeElapsed;
	
	public PulseLanternMode(LanternModeController controller)
		:base(controller)
	{
		this.m_NormalTexture = Resources.Load("GUI/HUD_lantern_off", typeof(Sprite)) as Sprite;
		this.m_ScaleFactor = new Vector3 (1.2f, 1.3f, 1.0f);
		this.m_fTimeElapsed = 0.0f;
	}
	
	public override void VOnUpdate ()
	{
		this.Pulse();
	}
	
	public override void VOnSwitch ()
	{
		m_Owner.gameObject.GetComponent<Image>().sprite = m_NormalTexture;
	}
	
	private void Pulse()
	{
		if(this.m_fTimeElapsed >= PULSE_INTERVAL)
		{
			this.m_fTimeElapsed = 0.0f;
			this.m_ScaleFactor = -m_ScaleFactor;
			
		}
		
		//make the button blink
		//Color button_blink =  GUI.color;
		//button_blink.a = Mathf.Sin(Time.time * 3.0f);
		
		//m_ButtonImage.color = button_blink;
		m_Owner.transform.localScale += m_ScaleFactor * Time.deltaTime;
		this.m_fTimeElapsed += Time.deltaTime;
	}
}

/*
 * public  class IdleLanternMode
 * 
 * Implementation of idle mode for Lantern Button
 * Pretty empty, open to more implemenation down the road
 */

public class IdleLanternMode : LanternMode
{
	private Sprite m_PulseTexture;
	
	public IdleLanternMode(LanternModeController controller)
		:base(controller)
	{
		m_Owner.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		this.m_PulseTexture = Resources.Load("GUI/HUD_lantern_light", typeof(Sprite)) as Sprite;
	}
	
	public override void VOnUpdate () {}
	
	public override void VOnSwitch ()
	{
		m_Owner.gameObject.GetComponent<Image>().sprite = m_PulseTexture;
	}
}

