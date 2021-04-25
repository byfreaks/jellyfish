using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToolComponent))]
public class HarpoonBehaviour : MonoBehaviour
{
    [SerializeField] GameObject harpoonMissile;
    [SerializeField] float cooldown;
    [SerializeField] GameObject fire;
    float currentCooldown;

    [SerializeField] float missileStrenght;
    [SerializeField] float missileDistance;


    void Update()
    {
        currentCooldown -= Time.deltaTime; 
        if(Input.GetMouseButtonDown(0))
            Shoot();

    }

    void Shoot()
    {
        if(currentCooldown <= 0){
            currentCooldown = cooldown;
            var missile = Instantiate(harpoonMissile, fire.transform.position, Quaternion.identity).GetComponent<MissileComponent>();
            missile.Setup(missileStrenght, missileDistance, PointHelper.DirectionToMouse(this.transform));
        } else return;
    }
}
