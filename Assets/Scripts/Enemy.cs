using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type;

    private Transform player;

    void Start()
    {
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
}
