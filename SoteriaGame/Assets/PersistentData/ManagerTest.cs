using UnityEngine;
using System.Collections;

public class ManagerTest : MonoBehaviour
{
	private string m_PrevToolTip;
	// Use this for initialization
	void Start ()
	{
		PersistentDataManager.Init ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
		GUI.Box (new Rect (10, 10, 200, 180), "Saver/Load Menu");

		if(GUI.Button(new Rect(20, 40, 80, 20), "Save Slot 1"))
		{
			//PersistentDataManager.Load(0);
		}

		if(GUI.Button(new Rect(20, 70, 80, 20), "Save Slot 2"))
		{
			//PersistentDataManager.Load(1);
		}

		if(GUI.Button(new Rect(20, 100, 80, 20), "Save Slot 3"))
		{
			//PersistentDataManager.Load(2);
		}

		if(Event.current.type == EventType.Repaint && GUI.tooltip != m_PrevToolTip)
		{
			if(m_PrevToolTip != "")
		}

	}
}

