using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameCleanUp : MonoBehaviour {

	public GameObject canvasPrefab;
	private GameObject backButton;

	// Use this for initialization
	void Awake () 
	{
		GameObject MCP = GameObject.Find("MCP");
		GameObject UI = GameObject.Find("UI");

		DestroyImmediate(MCP);
		DestroyImmediate(UI);

		GameObject canvas = Instantiate(canvasPrefab) as GameObject;
		EventTrigger trigger = canvas.GetComponentInChildren<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback = new EventTrigger.TriggerEvent();
		UnityEngine.Events.UnityAction<BaseEventData> call = new UnityEngine.Events.UnityAction<BaseEventData>(SwitchToMainMenu);
		entry.callback.AddListener(call);
		trigger.triggers.Add(entry);
	}

	public void SwitchToMainMenu(UnityEngine.EventSystems.BaseEventData baseEvent)
	{
		Application.LoadLevel("MainMenu");
	}
}
