using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBehaviour
{
    private GameObject Hitbox;
    public override EnemyBehaviours type { get { return EnemyBehaviours.Attack; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_Attack;
        ec.TimeBetweenActions = ec.enemyData.TimeBetweenAction_Attack;
        if(ec.enemyData.hasAttackAnimation) 
            ec.an.Play("Attack");
    }
    public override void BehaviorAction(EnemyController ec)
    {
        if(Hitbox == null)
        {
            Vector3 offsetPosition = (Vector3) ec.enemyData.HitboxOffset;
            Hitbox = GameObject.Instantiate(
                ec.enemyData.Hitbox, 
                ec.transform.position + offsetPosition,
                Quaternion.identity, 
                ec.transform
            );
        }
        else
        {
            GameObject.Destroy(Hitbox);
            ec.enemyBrain.EndBehaviour(ec);
        }
    }
}
