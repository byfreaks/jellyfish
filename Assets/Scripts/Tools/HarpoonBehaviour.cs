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
    PlayerController pc;

    Animator an;

    private void Start() {
        an = this.gameObject.GetComponent<Animator>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime; 
        if(Input.GetMouseButtonDown(0) && !pc.openInventory && pc.CountItems("Missile") > 0)
            Shoot();

        if(currentCooldown <= 0){
            an.Play("harpoon_full_idle");
        }

    }

    public void SetToEmpty(){
        an.Play("harpoon_empty_idle");
    }

    void Shoot()
    {
        if(currentCooldown <= 0){
            pc.DeleteHarpoon();
            currentCooldown = cooldown;
            var missile = Instantiate(harpoonMissile, fire.transform.position, Quaternion.identity).GetComponent<MissileComponent>();
            missile.Setup(missileStrenght, missileDistance, PointHelper.DirectionToMouse(fire.transform));
            an.Play("harpoon_shoot");
            SFXHelper.PlayEffect(SFXs.ShootHarpoon);
        } else return;
    }
}
