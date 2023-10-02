using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyConstants : MonoBehaviour
{
    public static readonly EnemyStats[] enemyStats = new EnemyStats[]
    {
        // Zombie
        new EnemyStats
        {
            speed = 0.6f,
            health = 31f,
            damage = 8f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Worm
        new EnemyStats
        {
            speed = 2.8f,
            health = 17f,
            damage = 3f,
            attackSpeed = 2.5f,
            attackRange = 0.6f
        },
        // Golem
        new EnemyStats
        {
            speed = 1.2f,
            health = 187f,
            damage = 20f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Necromancer
        new EnemyStats
        {
            speed = 0.8f,
            health = 72f,
            damage = 2f,
            attackSpeed = 2f,
            attackRange = 2.5f
        },
        // Boss
        new EnemyStats
        {
            speed = 0.9f,
            health = 1400f,
            damage = 60f,
            attackSpeed = 1f,
            attackRange = 0.6f
        }
    };

    public class EnemyStats
    {
        public float speed;
        public float health;
        public float damage;
        public float attackSpeed;
        public float attackRange;
    }
}
