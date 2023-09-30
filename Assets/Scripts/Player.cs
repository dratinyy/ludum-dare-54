using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public GameObject projectilePrefab;
    public float speed = 5f;

    private static float rateOfFire = 10f;

    private float nextFire = 0.0f;

    private bool isShooting = false;

    void Start()
    {
        // get projectile prefab in resources
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    void Update()
    {
        Move();
        cameraFollow();
        handleShoot();
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

        // move the rigidbody
        GetComponent<Rigidbody2D>().velocity = move * speed;
    }

    public void handleShoot()
    {
      // if mouse down anywhere on screen, shoot
      if(Input.GetMouseButtonDown(0))
      {
        isShooting = true;
      }
      if(Input.GetMouseButtonUp(0))
      {
        isShooting = false;
      }
      if(isShooting && Time.time > nextFire)
      {
        nextFire = Time.time + 1 / rateOfFire ;
        shootProjectile();
      }
    }

    public void shootProjectile()
    {
      // get vector toward mouse pos normalized 
      var worldMousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 direction = new Vector2(worldMousePosition.x - transform.position.x, worldMousePosition.y - transform.position.y);
      direction = direction.normalized;

      GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
      projectile.GetComponent<Projectile>().direction = direction;
      projectile.GetComponent<Projectile>().startPosition = transform.position;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Enemy")
      {
          Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
      if(collision.gameObject.tag == "Tile")
      {
        if(collision.gameObject.GetComponent<Tile>().getIsWalkable())
        {
          // ignore collision
          Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
      }
    }
}
