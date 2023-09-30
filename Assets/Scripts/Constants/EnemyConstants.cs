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
            attackSpeed = 1f
        },
        // Enemy 1
        new EnemyStats
        {
            speed = 1f,
            health = 100f,
            damage = 10f,
            attackSpeed = 1f
        },
        // Enemy 2
        new EnemyStats
        {
            speed = 1f,
            health = 100f,
            damage = 10f,
            attackSpeed = 1f
        }
    };

    public class EnemyStats
    {
        public float speed;
        public float health;
        public float damage;
        public float attackSpeed;
    }
}
