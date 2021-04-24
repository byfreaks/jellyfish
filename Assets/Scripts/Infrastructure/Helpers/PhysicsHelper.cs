using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsHelper
{
    #region [HARDCODE]
    public static float waterFriction = 25f;
    public static float waterPullHorizontalRange = 4f;
    public static float timeForWaterPull = 3f;
    #endregion
    public static Vector2 calculateVelocity(ref float currentSpeed, float movementSpeed, Vector2 direction, float timeSinceLastSwam)
    {
        float drag = (waterFriction * Time.deltaTime) / 2f;
        currentSpeed = Mathf.SmoothStep(currentSpeed, 0, drag);
        Vector2 newVelocity = direction * currentSpeed;

        if(timeSinceLastSwam > timeForWaterPull){
            var horPull = waterPullHorizontalRange;
            var verPull = -(waterFriction / 2f);
            var waterPull = new Vector2(Random.Range(-horPull, +horPull), verPull);

            newVelocity += waterPull;
        }

        return newVelocity;
    }
}
