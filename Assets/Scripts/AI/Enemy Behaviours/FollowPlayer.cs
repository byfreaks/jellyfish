using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : EnemyBehaviour
{
    public override EnemyBehaviours type { get { return EnemyBehaviours.FollowPlayer; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_FollowPlayer;
        ec.TimeBetweenActions = ec.enemyData.TimeBetweenAction_FollowPlayer;
    }
    public override void BehaviorAction(EnemyController ec)
    {
        Vector2 playerPosition = ec.Player.transform.position;
        Vector2 currentPosition = ec.transform.position;
        float weight = ec.enemyData.MovementWeight_FollowPlayer;
        ec.CurrentDirection =  (playerPosition - currentPosition).normalized * weight;
        ec.CurrentSpeed = ec.Speed;
    }
}
