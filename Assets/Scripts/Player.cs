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
        cameraFollow();
    }

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // normalize the vector if magnitude > 1
        float magnitude = Mathf.Sqrt(x * x + y * y);
        Vector3 move = new Vector3(x, y, 0);
        if(magnitude > 1)
        {
          move = move.normalized;
        }

        transform.Translate(move * speed * Time.deltaTime);
    }

    public void cameraFollow()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
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
