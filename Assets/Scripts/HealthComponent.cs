using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    int maxHealth;
    [SerializeField] int currentHealth;

    [Header("FeedbackEffect Settings")]
    public float duration = 0.4f;
    [SerializeField] private Color damageColor;
    private IEnumerator coroutine;

    [SerializeField] Material material;

    public bool isDead {get; private set; }
    private bool hasBeenSetup;

    private void Awake() {
        if(TryGetComponent<SpriteRenderer>(out var sr)){
            if(sr.material == null)
                sr.material = new Material(material);
        }
    }

    public void Setup(int max = 5, int? current = null){
        if(hasBeenSetup){
            Debug.LogError($"{this.gameObject.name}'s health component has alredy been set up once!");
            return;
        }
        maxHealth = max;
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
        StartFeedbackEffect(damageColor);
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
        // StartFeedbackEffect(damageColor);
    }

    private void StartFeedbackEffect(Color? color = null){
        if(!color.HasValue) color = Color.white;
        
        if(this.gameObject.TryGetComponent<SpriteRenderer>(out var sr)){
            if(sr.sharedMaterial.HasProperty("_Tint")){
                if(coroutine != null){
                    StopCoroutine(coroutine);
                }
                coroutine = FeedbackEffect(sr, color.Value);
                StartCoroutine( coroutine );
            } else {
                Debug.Log($"Health Component FeedbackEffect does not work with {sr.sharedMaterial.name}");
            }
        } else {
            Debug.Log($"Game Object has no Sprite Renderer to apply the effect to");
        }
    }

    private IEnumerator FeedbackEffect(SpriteRenderer sr, Color color){
        sr.sharedMaterial.SetColor("_Tint", color);

        var alpha = 0f;

        for(float i = duration; i>= 0; i -= Time.deltaTime){
            alpha = (i-0) / (duration-0);
            sr.sharedMaterial.SetColor("_Tint", new Color(color.r, color.g, color.b, alpha) );
            
            yield return null;
        }

        StopCoroutine(coroutine);
    }
}
