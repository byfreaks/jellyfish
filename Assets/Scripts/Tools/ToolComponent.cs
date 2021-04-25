using UnityEngine;

public class ToolComponent : MonoBehaviour
{
 
    SpriteRenderer sr;
    [SerializeField] bool flipWeapon = true;

    private void Start() {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var mouesPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sr.flipX = flipWeapon;
        this.transform.localScale = new Vector3( flipWeapon?-1:1, this.transform.localScale.y, 1 ); 

        PointHelper.PointAtTarget(this.transform, Camera.main.ScreenToWorldPoint(Input.mousePosition), flipWeapon);
    }
}