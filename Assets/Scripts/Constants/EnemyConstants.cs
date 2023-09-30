using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyConstants : MonoBehaviour
{
    public static readonly EnemyStats[] enemyStats = new EnemyStats[]
    {
        // Enemy 0
        new EnemyStats
        {
            speed = 1f,
            health = 100f,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Enemy 1
        new EnemyStats
        {
            speed = 3f,
            health = 50,
            damage = 10f,
            attackSpeed = 1f,
            attackRange = 0.6f
        },
        // Enemy 2
        new EnemyStats
        {
            speed = 1f,
            health = 100f,
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
