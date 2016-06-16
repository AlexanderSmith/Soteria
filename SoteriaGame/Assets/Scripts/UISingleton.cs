using UnityEngine;
using System.Collections;

public class UISingleton : MonoBehaviour
{
	private static UISingleton _instance;
	private void Awake()
	{
		if (_instance == null) {
			_instance = this;            
			DontDestroyOnLoad (this.gameObject); //Keep the instance going between scenes
		} else {
			if (this != _instance)
				DestroyImmediate (this.gameObject);
		}
	}
}
