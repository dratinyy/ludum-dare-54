using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private static readonly float particleMinInterval = 0.3f;
    private float particleClock = 0f;

    private static GameObject enemyBloodPrefab;
    private static GameObject enemyDeathPrefab;
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
        particleClock += Time.deltaTime;

        Vector2 distanceToPlayer = player.position - transform.position;
        transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = distanceToPlayer.x < 0;
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
        if (attackClock > 1f / EnemyConstants.enemyStats[type].attackSpeed)
        {
            attackClock = 0f;
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            player.GetComponent<Player>().TakeDamage(EnemyConstants.enemyStats[type].damage, Quaternion.Euler(new Vector3(0, 0, angle)));
        }
    }

    public void TakeDamage(float damage, Quaternion rotation)
    {
        currentHealth -= damage;
        if (enemyBloodPrefab == null)
            enemyBloodPrefab = Resources.Load<GameObject>("Prefabs/Particles/BloodPurple");

        if (particleClock >= particleMinInterval)
        {
            GameObject particle = GameObject.Instantiate(enemyBloodPrefab, transform.Find("BloodParticle").position, rotation);
            GameObject.Destroy(particle, 1.5f);
            particleClock = 0f;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        LeaderboardManager.Instance.enemyKilled[type]++;
        if (enemyDeathPrefab == null)
            enemyDeathPrefab = Resources.Load<GameObject>("Prefabs/Particles/DeathPurple");
        GameObject particle = GameObject.Instantiate(enemyDeathPrefab, transform.Find("BloodParticle").position, Quaternion.identity);
        GameObject.Destroy(particle, 1.5f);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tile" || collision.gameObject.tag == "Borders")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
