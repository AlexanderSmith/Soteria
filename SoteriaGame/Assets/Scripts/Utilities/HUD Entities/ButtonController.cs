﻿using UnityEngine;
using UnityEngine.Events;

/*
 * public abstract class ButtonMode
 * 
 * Control interface for the Coin Button
 * Button starts in 'Idle' mode, automatically switches
 * based on value of public property 'Pulsing'
 */
public class ButtonController : MonoBehaviour
{
    private HudManager _hudmanager;
    private ButtonMode m_ButtonMode;
	private bool m_bIsPulsing;

    // Use this for initialization
    public void Start()
    {
		this.m_ButtonMode = new IdleButtonMode(this);
        this.Pulsing = false;
    }

    //Give the controller a call back to the HUDManager so it can make the appropriate calls to the game director for a click event. 
    public void Initialize(HudManager manager)
    {
		this._hudmanager = manager;
		////TEstAudio
    }

    // Update is called once per frame
    void Update()
    {
        //hide derived implementation
		this.m_ButtonMode.VOnUpdate();
		//Removed the If ONMouse click statement I used the one in the Inspector in on the coin object.

    }

    //helper
    private void SetButtonMode(ButtonMode mode)
    {
		this.m_ButtonMode.VOnSwitch();
		this.m_ButtonMode = mode;
    }

    public void SafetyLightButtonHit()
    {
		if ((GameDirector.instance.GetGameState() != GameStates.Hidden) || (GameDirector.instance.GetGameState() != GameStates.HiddenTile))
		{
			this._hudmanager.SafteyLightButtonHit();
		}
    }

	//bool Pulsing
	/*
	 * Public property:
	 * Flip the bool in order to change the mode of the button
	 * (Idle or Pulsing)
	 * 
	 * If current value of Pulsing is equal to 'value,'
	 * the set is ignored to avoid initialization a duplicate object
	 */
	public bool Pulsing
	{
		get
		{
			return m_bIsPulsing;
		}
		set
		{
			//avoid redundant sets
			if(this.m_bIsPulsing != value)
			{
				this.m_bIsPulsing = value;

				ButtonMode newMode = null;

				if(this.m_bIsPulsing)
					newMode = new PulseButtonMode(this);
				else
					newMode = new IdleButtonMode(this);

				this.SetButtonMode(newMode);
			}
		}
	}
}
