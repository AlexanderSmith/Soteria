using UnityEngine;
using UnityEngine.Events;

/*
 * public abstract class LanternModeController
 * 
 * Control interface for the Lantern Button
 * Lantern starts in 'Idle' mode, automatically switches
 * based on value of public property 'Pulsing'
 */
public class LanternModeController : MonoBehaviour
{
	private HudManager _hudmanager;
	private LanternMode m_LanternMode;
	private bool m_lIsPulsing;
	
	// Use this for initialization
	public void Start()
	{
		this.m_LanternMode = new IdleLanternMode(this);
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
		this.m_LanternMode.VOnUpdate();
		//Removed the If ONMouse click statement I used the one in the Inspector in on the coin object.
	}
	
	//helper
	private void SetLanternMode(LanternMode mode)
	{
		this.m_LanternMode.VOnSwitch();
		this.m_LanternMode = mode;
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
			return m_lIsPulsing;
		}
		set
		{
			//avoid redundant sets
			if(this.m_lIsPulsing != value)
			{
				this.m_lIsPulsing = value;
				
				LanternMode newMode = null;
				
				if(this.m_lIsPulsing)
					newMode = new PulseLanternMode(this);
				else
					newMode = new IdleLanternMode(this);
				
				this.SetLanternMode(newMode);
			}
		}
	}
}
