using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyBehaviour
{
    public override EnemyBehaviours type { get { return EnemyBehaviours.Idle; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_Idle;
        ec.TimeBetweenActions = -1;
    }
    public override void BehaviorAction(EnemyController ec)
    {
        return;
    }
}
