using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * public abstract class ButtonMode
 * 
 * Strategy interface for current state of Coin Button
 * ButtonController.cs handles switch mechanism
 */
public abstract class ButtonMode
{
	protected ButtonController m_Owner;

	public ButtonMode(ButtonController controller)
	{
		m_Owner = controller;
	}

	//interface
	public abstract void VOnUpdate ();
	public abstract void VOnSwitch();
}

/*
 * public  class PulseButtonMode
 * 
 * Implementation of pulse mode for Coin Button
 * Button will pulse up and down, can mess with intervals below
 */
public class PulseButtonMode : ButtonMode
{
	private static readonly float PULSE_INTERVAL = 3.0f;
	private static readonly Color LIGHT_COLOR = new Color (204.0f, 204.0f, 204.0f, 255.0f);

	private Light m_PulseLight;
	private Color m_IdleColor;
	private Vector3 m_ScaleFactor;
	private float m_fTimeElapsed;

	public PulseButtonMode(ButtonController controller)
		:base(controller)
	{
		Image img = m_Owner.GetComponent<Image> ();
		m_IdleColor = img.color;
		img.color = LIGHT_COLOR;
		m_ScaleFactor = new Vector3 (0.1f, 0.1f, 0.1f);
		m_fTimeElapsed = 0.0f;
	}

	public override void VOnUpdate ()
	{
		this.Pulse();
	}

	public override void VOnSwitch ()
	{
		m_Owner.GetComponent<Image>().color = m_IdleColor;
	}

	private void Pulse()
	{
		if(m_fTimeElapsed >= PULSE_INTERVAL)
		{
			m_fTimeElapsed = 0.0f;
			m_ScaleFactor = -m_ScaleFactor;

		}


		//make the button blink
		//Color button_blink =  GUI.color;
		//button_blink.a = Mathf.Sin(Time.time * 3.0f);
 
		//m_ButtonImage.color = button_blink;
		m_Owner.transform.localScale += m_ScaleFactor * Time.deltaTime;
		m_fTimeElapsed += Time.deltaTime;
	}
}

/*
 * public  class PulseButtonMode
 * 
 * Implementation of idle mode for Coin Button
 * Pretty empty, open to more implemenation down the road
 */

public class IdleButtonMode : ButtonMode
{
	public IdleButtonMode(ButtonController controller)
		:base(controller)
	{
	}
	
	public override void VOnUpdate ()
	{
	}
	public override void VOnSwitch ()
	{
	}
}

