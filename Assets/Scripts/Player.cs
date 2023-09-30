using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;


    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // normalize the vector so that the player moves at the same speed in all directions
        Vector2 move = new Vector2(x, y).normalized;
        Debug.Log(x.ToString() + ", " + y.ToString() + " -> " + move.ToString());

        transform.Translate(move * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
