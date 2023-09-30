using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public int health = 100;
    public int speed = 5;
    public GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        // get projectile prefab in resources
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        cameraFollow();
        shoot();
    }

    public void move()
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

    public void shoot()
    {
      // get vector toward mouse pos normalized 
      var worldMousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 direction = new Vector2(worldMousePosition.x - transform.position.x, worldMousePosition.y - transform.position.y);
      direction = direction.normalized;

      // if mouse down anywhere on screen, shoot
      if(Input.GetMouseButtonDown(0))
      {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = direction;
        print(projectile.GetComponent<Projectile>().direction);
      }
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
