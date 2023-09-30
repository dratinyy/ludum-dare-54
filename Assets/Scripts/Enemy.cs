using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;
    private float currentHealth;
    private float attackClock = 0f;

    private Transform player;

    void Start()
    {
        currentHealth = EnemyConstants.enemyStats[type].health;
        player = GameManager.Instance.Player;
    }

    void Update()
    {
        attackClock += Time.deltaTime;
        Vector2 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= EnemyConstants.enemyStats[type].attackRange)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }
    void Move()
    {
        Vector2 dir = player.position - transform.position;
        transform.Translate(dir.normalized * EnemyConstants.enemyStats[type].speed * Time.deltaTime, Space.World);
    }

    void Attack()
    {
        if (attackClock > EnemyConstants.enemyStats[type].attackSpeed)
        {
            attackClock = 0f;
            player.GetComponent<Player>().TakeDamage(EnemyConstants.enemyStats[type].damage);
        }
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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Tile")
      {
          Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
    }
}
