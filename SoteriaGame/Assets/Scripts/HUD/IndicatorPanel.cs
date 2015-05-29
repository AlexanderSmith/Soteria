using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndicatorPanel : MonoBehaviour {

    public string indicatorPrefab;
    public string arrowPrefab;
    public Vector3 screenPositionOffsetFromCenter;

    List<GameObject> indicatorPool = new List<GameObject>();
    int indicatorPoolCursor = 0;

    List<GameObject> arrowPool = new List<GameObject>();
    int arrowPoolCursor = 0;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        paint();
	}

    void paint()
    {
        resetPool();

        GameObject[] areas = GameObject.FindGameObjectsWithTag("SafeArea");

        foreach (GameObject gObj in areas)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(gObj.transform.position);
            Color color = Color.red;

            Rect screenPosPixelInset = new Rect(screenPos.x, screenPos.y, 0, 0);

            if (screenPos.z > 0 &&
               screenPos.x > 0 && screenPos.x < Screen.width &&
               screenPos.y > 0 && screenPos.y < Screen.height)
            {
                GameObject spot = getIndicator();

                spot.GetComponent<GUITexture>().pixelInset = screenPosPixelInset;    
            }
            else
            {
                Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

                GameObject myArrow = getArrow();
                screenPos = screenCenter + screenPositionOffsetFromCenter;

                myArrow.transform.position = cam.ScreenToWorldPoint(screenPos);

                Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector3 playerToAreaOfInterest = gObj.transform.position - playerPos;
                float zAngle = Mathf.Acos((Vector3.Dot(playerPos, playerToAreaOfInterest)) / (playerPos.magnitude * playerToAreaOfInterest.magnitude));

                myArrow.transform.localRotation = new Quaternion(cam.transform.localRotation[0], cam.transform.localRotation[1], zAngle, cam.transform.localRotation[3]);

                //screenPos = Camera.main.WorldToScreenPoint(gObj.transform.position);
                //if (screenPos.z < 0)
                //{
                //    screenPos *= -1;
                //}

                //Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                //screenPos = screenCenter;

                //float angle = Mathf.Atan2(screenPos.y, screenPos.x);
                //angle -= 90 * Mathf.Deg2Rad;

                //float cos = Mathf.Cos(angle);
                //float sin = -Mathf.Sin(angle);
                //screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                //float m = cos / sin;

                //Vector3 screenBounds = screenCenter * 0.9f;

                //if (cos > 0)
                //{
                //    screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
                //}
                //else
                //{
                //    screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
                //}

                //Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 

                //screenPos += screenCenter;
                //screenPos.z = cam.nearClipPlane;
                //Debug.Log(screenPos);
                //GameObject myArrow = getArrow();
                //myArrow.transform.position = cam.ScreenToWorldPoint(screenPos);
                //myArrow.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            }
        }
        cleanPool();
    }

    void resetPool()
    {
        indicatorPoolCursor = 0;
        arrowPoolCursor = 0;
    }

    GameObject getIndicator()
    {
        GameObject output;

        if (indicatorPoolCursor < indicatorPool.Count)
        {
            output = indicatorPool[indicatorPoolCursor];
        }
        else
        {
            output = Instantiate(Resources.Load("Prefabs/" + indicatorPrefab) as GameObject) as GameObject;
            indicatorPool.Add(output);
        }

        indicatorPoolCursor++;
        return output;
    }

    GameObject getArrow()
    {
        GameObject output;

        if (arrowPoolCursor < arrowPool.Count)
        {
            output = arrowPool[indicatorPoolCursor];
        }
        else
        {
            output = Instantiate(Resources.Load("Prefabs/" + arrowPrefab) as GameObject) as GameObject;
            arrowPool.Add(output);
        }

        arrowPoolCursor++;
        return output;
    }

    void cleanPool()
    {
        while (indicatorPool.Count > indicatorPoolCursor)
        {
            GameObject obj = indicatorPool[indicatorPool.Count - 1];
            indicatorPool.Remove(obj);
            Destroy(obj);
        }

        while (arrowPool.Count > arrowPoolCursor)
        {
            GameObject obj = arrowPool[arrowPool.Count - 1];
            arrowPool.Remove(obj);
            Destroy(obj);
        }
    }

}
