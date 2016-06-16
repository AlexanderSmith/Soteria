using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public void ChangeScene (string sceneToChangeTo) {
        Application.LoadLevel(sceneToChangeTo);
	}
}
