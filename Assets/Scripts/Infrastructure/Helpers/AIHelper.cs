using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIHelper
{
    public static Vector2 FlockMainBehaviour(EnemyController enemy)
    {
        //Get Neighbors
        List<EnemyController> enemies = GetNearbyEnemies(enemy);

        //No enemies nearby -> No adjustment is necessary
        if(enemies.Count == 0)
            return Vector2.zero;
        
        //Because performance is better use sqrMagnitude than magnitude.
        float squareAvoidanceRadius = enemy.enemyData.AvoidanceRadius;
        squareAvoidanceRadius *= squareAvoidanceRadius;

        //Enemies nearby -> Calculate adjustment
        Vector2 flockAdjustment = Vector2.zero;
        flockAdjustment += FlockAvoidanceBehaviour(enemy, enemies, squareAvoidanceRadius).normalized * enemy.enemyData.MovementWeight_Avoidance;
        flockAdjustment += FlockAlignmentBehaviour(enemy, enemies).normalized * enemy.enemyData.MovementWeight_Aligment;
        flockAdjustment += FlockCohesionBehaviour(enemy, enemies).normalized * enemy.enemyData.MovementWeight_Cohesion;

        return flockAdjustment;
    }
    static List<EnemyController> GetNearbyEnemies(EnemyController enemy)
    {
        List<EnemyController> enemies = new List<EnemyController>();
        Collider2D[] enemiesColliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.enemyData.NeighborRadius);
        foreach (Collider2D c in enemiesColliders)
        {
            if (c != enemy.bc)
            {
                EnemyController enemyNearby = c.gameObject.GetComponent<EnemyController>();
                if(enemyNearby != null)
                    enemies.Add(enemyNearby);
            }
        }
        return enemies;
    }
    static Vector2 FlockAvoidanceBehaviour(EnemyController enemy, List<EnemyController> enemies, float squareAvoidanceRadius)
    {
        Vector2 avoidanceAdjustment = Vector2.zero;
        int numberToAvoid = 0;
        //Add all positions, take the average
        foreach(EnemyController enemyNearby in enemies)
        {
            if (Vector2.SqrMagnitude(enemyNearby.transform.position - enemy.transform.position) < squareAvoidanceRadius)
            {
                numberToAvoid++;
                avoidanceAdjustment += (Vector2)(enemy.transform.position - enemyNearby.transform.position);
            }
        }
        if (numberToAvoid > 0)
            avoidanceAdjustment /= numberToAvoid;

        return avoidanceAdjustment;
    }
    static Vector2 FlockAlignmentBehaviour(EnemyController enemy, List<EnemyController> enemies)
    {
        Vector2 alignmentAdjustment = Vector2.zero;
        //Add all positions, take the average
        foreach(EnemyController enemyNearby in enemies)
            alignmentAdjustment += (Vector2) enemyNearby.rb.velocity;
        alignmentAdjustment /= enemies.Count;

        return alignmentAdjustment;
    }
    static Vector2 FlockCohesionBehaviour(EnemyController enemy, List<EnemyController> enemies)
    {
        Vector2 cohesionAdjustment = Vector2.zero;
        //Add all positions, take the average
        foreach(EnemyController enemyNearby in enemies)
            cohesionAdjustment += (Vector2) enemyNearby.transform.position;
        cohesionAdjustment /= enemies.Count;
        //Create offset for enemy position
        cohesionAdjustment -= (Vector2) enemy.transform.position;
        return cohesionAdjustment;
    }

}
