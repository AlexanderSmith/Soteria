using UnityEngine;
using System.Collections;

public class BasicAggroSystem {
    public Movement moveSystem;


    public BasicAggroSystem()
    {
        moveSystem = new Movement();
    }

    public void AggroCheckAndBasicMove (float lookAtDistance, float attackRange, float overwhelmRange, Transform target, Transform seeker) 
    {
		Texture CurrenTexture;
		float distance = Vector3.Distance(target.position, seeker.position);

        if (target.GetComponent<EncounterMovementController>().GetCurrentState() != EncounterMovementController.EncounterState.Hidden)
        {
            if (distance < lookAtDistance)
            {
                CurrenTexture = Resources.Load("ShadowCreatureAlert") as Texture;
                seeker.renderer.material.mainTexture = CurrenTexture;
                seeker.LookAt(target);
            }
            if (distance > lookAtDistance)
            {
                CurrenTexture = Resources.Load("ShadowCreature Unaware") as Texture;
                seeker.renderer.material.mainTexture = CurrenTexture;
            }
            if (distance < attackRange && distance >= overwhelmRange)
            {
                moveSystem.Move(seeker.forward, 0.3f, seeker);
                CurrenTexture = Resources.Load("ShadowCreature_Attack") as Texture;
                seeker.renderer.material.mainTexture = CurrenTexture;
            }
            if (distance <= overwhelmRange)
            {
                target.GetComponent<EncounterMovementController>().Overwhelm(seeker);
            }
        }
    }
}
