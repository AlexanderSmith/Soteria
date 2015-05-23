using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IndicatorPanel : MonoBehaviour {

    public string indicatorPrefab;
    public string arrowPrefab;


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

            if (screenPos.z > 0 &&
               screenPos.x > 0 && screenPos.x < Screen.width &&
               screenPos.y > 0 && screenPos.y < Screen.height)
            {
                GameObject spot = getIndicator();
                spot.transform.localPosition = screenPos;   
				spot.transform.parent = gameObject.transform;
            }
            else
            {
                if (screenPos.z < 0)
                {
                    screenPos *= -1;
                }

                Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

                screenPos = screenCenter;

                float angle = Mathf.Atan2(screenPos.y, screenPos.x);
                angle -= 90 * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = -Mathf.Sin(angle);
                screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

                float m = cos / sin;

                Vector3 screenBounds = screenCenter * 0.9f;

                if (cos > 0)
                {
                    screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
                }
                else
                {
                    screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
                }

                screenPos += screenCenter;
                GameObject myArrow = getArrow();
                myArrow.transform.localPosition = screenPos;
                myArrow.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
				myArrow.transform.parent = gameObject.transform;
            }
        }

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
