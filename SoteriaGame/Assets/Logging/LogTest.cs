using UnityEngine;
using System.Collections;

public class LogTest : MonoBehaviour
{
	public Transform m_Transform;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("HI");

		Logger.RegisterNewChannel (LogChannels.ObjectCreation, Logger.LogLevel.E_LOG_VERBOSE, true);

		Logger.Log (LogChannels.ObjectCreation, gameObject.name + " Log Test 1!", false);
		Logger.Log (LogChannels.ObjectCreation, gameObject.name + " Log Test 2!", false);

		Logger.RegisterNewChannel (LogChannels.PositionData, Logger.LogLevel.E_LOG_VERBOSE, false);

		string msg = gameObject.name + " current position at " + m_Transform.position.ToString ();
		Logger.Log (LogChannels.PositionData, msg, true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}