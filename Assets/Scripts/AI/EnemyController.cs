using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace AI
{
    public class EnemyController : MonoBehaviour
    {
        #region [HARDCODE]
        const float SPEED = 5.0f;
        const float HEALTH = 100.0f;
        #endregion

        #region Properties
        public Sprite sprite; //[VALUE DEFINED FROM EDITOR]
        private Enemy enemy;
        private Rigidbody2D rb;
        private BoxCollider2D bc;
        private SpriteRenderer sr;
        #endregion

        #region Unity Engine Loop
        void Start()
        {
            enemy = new Enemy(SPEED, HEALTH);
            sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = sprite; //[REVIEW] How to load sprites?
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            bc = gameObject.AddComponent<BoxCollider2D>();


            Debug.Log("SPEED: " + enemy.Speed);
            Debug.Log("HEALTH: " + enemy.MaxHealth);
        }
        void Update()
        {
            Debug.Log("Update");
        }
        #endregion
    }
}
