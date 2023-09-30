using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

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
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
}
