using UnityEngine;
using System.Collections;

public class BasicAggroSystem {

     public void AggroCheckAndBasicMove (float lookAtDistance, float attackRange, Transform target, Transform seeker) 
     {
         Debug.Log("TARGET"+target.position);
         Debug.Log("SEEKER"+seeker.position);
         float distance = Vector3.Distance(target.position, seeker.position);
 
         if(distance < lookAtDistance)
         {
             seeker.renderer.material.color = Color.yellow;
             seeker.LookAt(target);
         }   
         if(distance > lookAtDistance)
         {
            seeker.renderer.material.color = Color.green; 
         }
         if(distance < attackRange)
         {
             target.position += target.forward * Time.deltaTime * 0.3f;//arb speed
             seeker.renderer.material.color = Color.red;
         }
     }
}
