using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public int type;
  private Vector2 direction;
  private Vector3 startPosition;

  private int targetCount = 0;
  // Keep track of already hit targets
  private List<GameObject> hitTargets = new List<GameObject>();

  void Start()
  {
    // Rotate toward direction
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    angle -= 90;
    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
  }

  public void Init(Vector2 direction, Vector3 startPosition, int type)
  {
    this.direction = direction;
    this.startPosition = startPosition;
    this.type = type;
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
    if (other.gameObject.tag == "EnemyHitbox" && !hitTargets.Contains(other.gameObject))
    {
      hitTargets.Add(other.gameObject);
      targetCount++;

      // Deal damage to primary target
      other.gameObject.GetComponentInParent<Enemy>().TakeDamage(WeaponConstants.weaponStats[type].damage);

      // Deal damage to secondary targets
      if (WeaponConstants.weaponStats[type].explosive)
      {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, WeaponConstants.weaponStats[type].explosiveRange);
        foreach (Collider2D collider in colliders)
        {
          if (collider.gameObject.tag == "EnemyHitbox" && !hitTargets.Contains(collider.gameObject))
          {
            hitTargets.Add(collider.gameObject);
            targetCount++;
            collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(WeaponConstants.weaponStats[type].damage);
          }
        }
      }

      // Destroy if piercing limit reached
      if (targetCount >= WeaponConstants.weaponStats[type].piercing)
      {
        Destroy(gameObject);
      }
    }
  }
}
