using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float maxHealth = 100f;
  public float damageMultiplier = 1f;
  public float movespeedMultiplier = 1f;
  public float attackSpeedMultiplier = 1f;
  public float attackRangeMultiplier = 1f;
  private int weaponType = 0;

  private float health;
  public float Health
  {
    get
    {
      return health;
    }
  }
  private float speed = 5f;
  private int money = 1000;
  public int Money
  {
    get
    {
      return money;
    }
  }
  public Animator animatorLegs;
  public SpriteRenderer spriteRendererLegs;
  public SpriteRenderer spriteRendererTop;

  public bool canShoot = true;


  private float nextFire = 0.0f;

  private bool isShooting = false;

  void Start()
  {
    health = maxHealth;
    animatorLegs = transform.Find("SpriteLegs").GetComponent<Animator>();
    spriteRendererLegs = transform.Find("SpriteLegs").GetComponent<SpriteRenderer>();
    spriteRendererTop = transform.Find("SpriteTop").GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    Move();
    FaceMouse();
    cameraFollow();
    handleShoot();
  }

  public void SetWeaponType(int weaponType)
  {
    // TODO: update sprite
    this.weaponType = weaponType;
  }

  public void Move()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    // normalize the vector if magnitude > 1
    float magnitude = Mathf.Sqrt(x * x + y * y);
    Vector3 move = new Vector3(x, y, 0);

    if (magnitude > 1)
    {
      move = move.normalized;
    }
    animatorLegs.SetFloat("Speed", Mathf.Abs(magnitude));
    if (x != 0)
    {
      spriteRendererLegs.flipX = x < 0;
    }

    GetComponent<Rigidbody2D>().velocity = move * speed;
  }

  void FaceMouse()
  {
    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    spriteRendererTop.flipX = worldMousePosition.x < transform.position.x;
  }

  public void handleShoot()
  {
    if (!canShoot)
    {
      return;
    }
    // if mouse down anywhere on screen, shoot
    if (Input.GetMouseButtonDown(0))
    {
      isShooting = true;
    }
    if (Input.GetMouseButtonUp(0))
    {
      isShooting = false;
    }
    if (isShooting && Time.time > nextFire)
    {
      nextFire = Time.time + 1 / WeaponConstants.weaponStats[weaponType].attackSpeed;
      ShootProjectiles();
    }
  }

  public void ShootProjectiles()
  {
    // Vector towards mouse
    var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 dirTowardsMouse = new Vector2(worldMousePosition.x - transform.position.x, worldMousePosition.y - transform.position.y);
    Vector2 gunPosition = transform.Find("GunPosition").position;

    // Instantiate projectiles
    for (int i = 0; i < WeaponConstants.weaponStats[weaponType].projectileCount; i++)
    {
      // Randomize angle
      float randomAngleDelta = UnityEngine.Random.Range(-WeaponConstants.maxDispersionDegrees * (100 - WeaponConstants.weaponStats[weaponType].accuracy) / 100f,
      WeaponConstants.maxDispersionDegrees * (100 - WeaponConstants.weaponStats[weaponType].accuracy) / 100f) * Mathf.Deg2Rad;

      // Rotate vector towards mouse by random angle
      float randomAngle = Mathf.Atan2(dirTowardsMouse.y, dirTowardsMouse.x) + randomAngleDelta;
      dirTowardsMouse = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

      GameObject projectile = Instantiate(WeaponConstants.weaponStats[weaponType].projectilePrefab, gunPosition, Quaternion.identity);
      projectile.GetComponent<Projectile>().Init(dirTowardsMouse, transform.position, weaponType);
    }
  }

  public void cameraFollow()
  {
    Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
  }

  public void Pay(int amount)
  {
    money -= amount;
  }

  public void Heal(float damage)
  {
    health = Mathf.Min(maxHealth, health + damage);
    UIManager.Instance.UpdateHealth(health, maxHealth);
  }

  public void TakeDamage(float damage)
  {
    health = Mathf.Max(0, health - damage);
    UIManager.Instance.UpdateHealth(health, maxHealth);
    UIManager.Instance.FlashScreen();
    if (health <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    //Destroy(gameObject);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    }
    else if (collision.gameObject.tag == "Tile")
    {
      if (collision.gameObject.GetComponent<Tile>().getIsWalkable())
      {
        // ignore collision
        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
      }
    }
  }
}
