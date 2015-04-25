using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * public abstract class ButtonMode
 * 
 * Control interface for the Coin Button
 * Button starts in 'Idle' mode, automatically switches
 * based on value of public property 'Pulsing'
 */
public class ButtonController : MonoBehaviour
{
    HUDManager myManager;
    ButtonMode m_ButtonMode;
    bool m_bIsPulsing;

    // Use this for initialization
    void Start()
    {
        m_ButtonMode = new IdleButtonMode(this);
        this.Pulsing = false;
    }

    //Give the controller a call back to the HUDManager so it can make the appropriate calls to the game director for a click event. 
    public void Initialize(HUDManager manager)
    {
        myManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        //hide derived implementation
        m_ButtonMode.VOnUpdate();

        if (this.guiTexture.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    //helper
    private void SetButtonMode(ButtonMode mode)
    {
        m_ButtonMode.VOnSwitch();
        m_ButtonMode = mode;
    }

    void OnMouseDown()
    {
        myManager.SafteyLightButtonHit();
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
			if(m_bIsPulsing != value)
			{
				m_bIsPulsing = value;

				ButtonMode newMode = null;

				if(m_bIsPulsing)
					newMode = new PulseButtonMode(this);
				else
					newMode = new IdleButtonMode(this);

				this.SetButtonMode(newMode);
			}
		}
	}
}
