using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : EnemyBehaviour
{
    public override EnemyBehaviours type { get { return EnemyBehaviours.Escape; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_Escape;
        ec.TimeBetweenActions = ec.enemyData.TimeBetweenAction_Escape;
    }
    public override void BehaviorAction(EnemyController ec)
    {
        Vector2 playerPosition = ec.Player.transform.position;
        Vector2 currentPosition = ec.transform.position;
        float weight = ec.enemyData.MovementWeight_Escape;
        ec.CurrentDirection =  -(playerPosition - currentPosition).normalized * weight;
        ec.CurrentSpeed = ec.Speed;
    }
}
