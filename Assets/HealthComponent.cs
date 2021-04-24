using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    public bool isDead {get; private set; }
    private bool hasBeenSetup;

    public void Setup(int max = 5, int? current = null){
        if(hasBeenSetup){
            Debug.LogError($"{this.gameObject.name}'s health component has alredy been set up once!");
            return;
        }
        maxHealth = 5;
        if(current.HasValue) 
            currentHealth = current.Value;
        else
            currentHealth = maxHealth;

        hasBeenSetup = true;
    }

    void AlterHealth(int mod)
    {
        if(!hasBeenSetup) Debug.LogError($"{this.gameObject.name}'s health component has not ben set up!");
        if(isDead) return;

        currentHealth += mod;
        if(currentHealth <= 0){
            isDead = true;
        }
    }

    public void Hurt(int val = 1)
    {
        AlterHealth(-val);
    }

    public void Heal(int val = 1)
    {
        AlterHealth(+val);
    }

    public void HealAll(){
        AlterHealth(maxHealth - currentHealth);
    }

    public void Kill(){
        AlterHealth(-maxHealth);
    }
}
