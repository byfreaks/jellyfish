using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyBehaviour
{
    public override EnemyBehaviours type { get { return EnemyBehaviours.Idle; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_Default;
        ec.TimeBetweenActions = ec.enemyData.TimeBetweenAction_Default;
    }
    public override void BehaviorAction(EnemyController ec)
    {
        ec.CurrentDirection = new Vector2(Random.Range(-20f,20f),Random.Range(-10f,10f));
    }
}
