using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
      // Rotate toward direction
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      angle -= 90;
      transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    { 
      // move up
      transform.position += transform.up * speed * Time.deltaTime;
    }

    public void autoDestroy()
    {
      // if out of bounds, destroy
      if(transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10)
      {
        Destroy(gameObject);
      }
    }

}
