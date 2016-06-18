using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipStringFix : MonoBehaviour
{
    string text;

    void Update()
    {
        text = this.GetComponent<Text>().text;

        if (text.Length > 0)
        { 
            text = text.Replace('$', '\n');

            this.GetComponent<Text>().text = text;
        }
    }	
}