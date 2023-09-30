using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    private float currentHealth;

    private Transform player;

    void Start()
    {
        currentHealth = EnemyConstants.enemyStats[type].health;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (player == null)
        {
            player = GameManager.Instance.Player;
        }
        Vector2 dir = player.position - transform.position;
        transform.Translate(dir.normalized * EnemyConstants.enemyStats[type].speed * Time.deltaTime, Space.World);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
