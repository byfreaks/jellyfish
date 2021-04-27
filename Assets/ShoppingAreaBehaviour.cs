using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingAreaBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Contains("Player")){
            var player = other.GetComponent<PlayerController>();
            player.ShoppingMenu(true);
            player.RestoreOxygen();
        }   
    }
}
