using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // public Projectile(Vector2 direction, float speed)
    // {
    //     this.direction = direction;
    //     this.speed = speed;
    // }

    public Vector2 direction = new Vector2(0, 1);
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
      // Rotate toward direction
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

}
