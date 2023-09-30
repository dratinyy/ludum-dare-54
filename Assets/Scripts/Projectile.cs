using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    private static float speed = 10.0f;
    private static float range = 10.0f;
    public Vector3 startPosition;

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
        autoDestroy();
    }

    public void Move()
    { 
      // move up
      Vector3 move = transform.up  * speed * Time.deltaTime;
      transform.position += move;
    }

    public void autoDestroy()
    {
      // if out of bounds, destroy
      if(Vector3.Distance(startPosition, transform.position) > range)
      {
        Destroy(gameObject);
      }
    }
}
