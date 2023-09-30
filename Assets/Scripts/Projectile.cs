using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public int type;
  private Vector2 direction;
  private Vector3 startPosition;

  void Start()
  {
    // Rotate toward direction
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    angle -= 90;
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
  }

  public void Init(Vector2 direction, Vector3 startPosition)
  {
    this.direction = direction;
    this.startPosition = startPosition;
  }

  void Update()
  {
    Move();
  }

  public void Move()
  {
    Vector3 move = transform.up * WeaponConstants.weaponStats[type].projectileSpeed * Time.deltaTime;
    transform.position += move;
    // if out of bounds, destroy
    if (Vector3.Distance(startPosition, transform.position) > WeaponConstants.weaponStats[type].range)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    // Collide with enemy hitbox
    if (other.gameObject.tag == "EnemyHitbox")
    {
      other.gameObject.GetComponentInParent<Enemy>().TakeDamage(10);
      Destroy(gameObject);
    }
  }
}
