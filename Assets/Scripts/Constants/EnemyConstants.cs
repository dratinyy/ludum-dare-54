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
            speed = 0.7f,
            health = 31f,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Worm
        new EnemyStats
        {
            speed = 3f,
            health = 17f,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Golem
        new EnemyStats
        {
            speed = 1f,
            health = 167f,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Necromancer
        new EnemyStats
        {
            speed = 1f,
            health = 57f,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 10f
        },
        // Boss
        new EnemyStats
        {
            speed = 1f,
            health = 3000f,
            damage = 10f,
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
