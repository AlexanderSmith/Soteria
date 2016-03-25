using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    public GameObject camera;
    public int speed = 1;
    public string level;
    bool pauseTheRoll = false;

	// Update is called once per frame
	void Update () {

        if (pauseTheRoll == false) {

            camera.transform.Translate(Vector3.down * Time.deltaTime * speed);

        }
        

        StartCoroutine(waitFor());
        
    }

    IEnumerator Pause()
    {
        pauseTheRoll = true;
        yield return new WaitForSeconds(35);
        Application.LoadLevel(level);
    }
    IEnumerator waitFor()
    {
        
        yield return new WaitForSeconds(235);
        StartCoroutine(Pause());
    }
}
