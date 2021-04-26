using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigFishBrain", menuName = "ScriptableObjects/BigFishBrain")]
public class BigFishBrain : EnemyBrain
{
    public float distanceToDetectPlayer = 100f;
    public float distanceToAttackPlayer = 20f;
    public float distanceToEscapeFromPlayer = 200f;

    public override void CheckConditionsForNewBehaviours(EnemyController ec)
    {
        float squareDistanceToPlayer = (ec.Player.transform.position - ec.transform.position).sqrMagnitude;
        float squareDistanceToDetectPlayer = distanceToDetectPlayer * distanceToDetectPlayer;
        float squareDistanceToAttackPlayer = distanceToAttackPlayer * distanceToAttackPlayer;
        float squareDistanceToEscapeFromPlayer = distanceToEscapeFromPlayer * distanceToEscapeFromPlayer;
        //Idle: 
        //  * Player too close -> FollowPlayer
        if(ec.currentBehaviour.type == EnemyBehaviours.Idle)
        {
            if(squareDistanceToPlayer < squareDistanceToDetectPlayer)
                EndBehaviour(ec, EnemyBehaviours.FollowPlayer);
        }
        //FollowPlayer: 
        //  * Player too close -> Attack
        //  * Player too far -> Idle
        else if(ec.currentBehaviour.type == EnemyBehaviours.FollowPlayer)
        {
            if(squareDistanceToPlayer < squareDistanceToAttackPlayer)
            {
                if(
                    (ec.CurrentDirection.x > 0 && ec.enemyData.HitboxOffset.x < 0)
                    || (ec.CurrentDirection.x < 0 && ec.enemyData.HitboxOffset.x > 0)
                )
                    ec.enemyData.HitboxOffset *= -1;
                EndBehaviour(ec, EnemyBehaviours.Attack);
            }
            else if(squareDistanceToPlayer > squareDistanceToDetectPlayer)
                EndBehaviour(ec, EnemyBehaviours.Idle);
        }
        //Escape:
        //  * Player too far -> Idle
        else if(ec.currentBehaviour.type == EnemyBehaviours.Escape)
        {
            if(squareDistanceToPlayer > squareDistanceToEscapeFromPlayer)
                EndBehaviour(ec, EnemyBehaviours.Idle);
        }
    }

    public override void EndBehaviour(EnemyController ec, EnemyBehaviours forceNewBehaviour = EnemyBehaviours.None)
    {
        //If new behaviour is forced
        if(forceNewBehaviour != EnemyBehaviours.None)
            ec.ChangeBehaviour(forceNewBehaviour);
        //Pass next behaviour defined
        else
        {
            //When finish Attack -> Escape
            if(ec.currentBehaviour.type == EnemyBehaviours.Attack) 
                ec.ChangeBehaviour(EnemyBehaviours.Escape);
        }
    }
}
