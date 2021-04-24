using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public class Enemy
    {
        private float speed;
        private float maxHealth;

        public float Speed { get { return speed; } }
        public float MaxHealth { get { return maxHealth; } }

        public Enemy(float speed, float maxHealth)
        {
            this.speed = speed;
            this.maxHealth = maxHealth;
        }
    }
}
