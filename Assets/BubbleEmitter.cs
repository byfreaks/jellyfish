using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEmitter : MonoBehaviour
{

    [SerializeField] GameObject bubblePrefab;
    [SerializeField] List<Sprite> bubbleSprites;

    private float rnd => Random.Range(-0.6f,0.6f);
    private float rndSpd => Random.Range(-20f,0f);

    public void Emit(int amount, float speed, Vector2 direction){
        var bubbles = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            var b = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
            var bc = b.GetComponent<BubbleBehaviour>();
            bc.Initialize( new Vector2(direction.x + rnd, direction.y + rnd).normalized, speed * rndSpd, bubbleSprites[Random.Range(0,2)]);
            
        }
    }
}
