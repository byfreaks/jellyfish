using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNamedScript : MonoBehaviour
{
    public string name;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Contains(name))
            GameObject.Destroy(other.gameObject, 0.01f);
    }
}
