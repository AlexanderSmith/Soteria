/*using UnityEngine;
using System.Collections;

public class QTETest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		///Lerp it for better quality!
		Vector3 temp = this.transform.localScale;
		this.transform.localScale = Vector3.one;

		float modifier =  1 + ( 0.1f * GameDirector.instance.GetQTECount ());
		//this.transform.localScale = new Vector3 (this.transform.localScale.x * ( modifier ),
		  //                                       this.transform.localScale.y * ( modifier ),
		    //                                     this.transform.localScale.z * ( modifier ));

		this.transform.localScale = Vector3.Slerp(temp, new Vector3( ( modifier ),
		                                                             ( modifier ), 
		                                                             ( modifier )), Time.deltaTime );

	}
}
*/
