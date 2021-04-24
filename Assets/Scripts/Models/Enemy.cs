using System.Collections;
using System.Collections.Generic;

public class Enemy
{
    public float Speed { get; set; }
    public int MaxHealth { get; }
    public float TimeBetweenSwams { get; set; }

    public Enemy(float speed, int maxHealth, float timeBetweenSwams)
    {
        Speed = speed;
        MaxHealth = maxHealth;
        TimeBetweenSwams = timeBetweenSwams;
    }
}
