using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public int health = 100;
    public int speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        cameraFollow();
    }

    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

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
