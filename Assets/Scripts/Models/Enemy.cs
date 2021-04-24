using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public class Enemy
    {
        public float Speed { get; }
        public float MaxHealth { get; }
        public float TimeBetweenSwams { get; }

        public Enemy(float speed, float maxHealth, float timeBetweenSwams)
        {
            Speed = speed;
            MaxHealth = maxHealth;
            TimeBetweenSwams = timeBetweenSwams;
        }
    }
}
