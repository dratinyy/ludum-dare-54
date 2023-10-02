using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  private static GameObject playerBloodPrefab;
  private static readonly float particleMinInterval = 0.3f;
  private float particleClock = 0f;
  private float maxHealth = 100f;
  public float MaxHealth
  {
    get
    {
      return maxHealth;
    }
  }
  public float damageMultiplier = 1f;
  public float movespeedMultiplier = 1f;
  public float attackSpeedMultiplier = 100f;
  public float attackRangeMultiplier = 10f;
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
  private int money = 0;
  public int Money
  {
    get
    {
      return money;
    }
  }
  public Animator animatorLegs;
  public SpriteRenderer spriteRendererLegs;
  public SpriteRenderer[] spriteRendererTop = new SpriteRenderer[WeaponConstants.weaponStats.Length];

  public bool canShoot = true;

  private bool canMove = true;

  private bool isDead = false;

  public bool godMode = false;
  public bool bornRichMode = false;

  private float nextFire = 0.0f;

  private bool isShooting = false;

  void Start()
  {
    health = maxHealth;
    animatorLegs = transform.Find("SpriteLegs").GetComponent<Animator>();
    spriteRendererLegs = transform.Find("SpriteLegs").GetComponent<SpriteRenderer>();
    spriteRendererTop = new SpriteRenderer[WeaponConstants.weaponStats.Length];
    for (int i = 0; i < WeaponConstants.weaponStats.Length; i++)
    {
      spriteRendererTop[i] = transform.Find("SpriteTopWeapon" + i.ToString()).GetComponent<SpriteRenderer>();
    }

    if (bornRichMode)
    {
      money = 100000;
    }
  }

  void Update()
  {
    particleClock += Time.deltaTime;
    Move();
    FaceMouse();
    cameraFollow();
    handleShoot();
  }

  public void SetWeaponType(int weaponType)
  {
    this.weaponType = weaponType;

    for (int i = 0; i < spriteRendererTop.Length; i++)
    {
      if (i == weaponType)
      {
        spriteRendererTop[i].enabled = true;
      }
      else
      {
        spriteRendererTop[i].enabled = false;
      }
    }
  }

  public void Move()
  {
    if (!canMove)
    {
      return;
    }

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
    animatorLegs.SetFloat("Speed", Mathf.Abs(move.magnitude));
    if (x != 0)
    {
      spriteRendererLegs.flipX = x < 0;
    }

    GetComponent<Rigidbody2D>().velocity = move * speed * movespeedMultiplier;
  }

  void FaceMouse()
  {
    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    for (int i = 0; i < spriteRendererTop.Length; i++)
    {
      spriteRendererTop[i].flipX = worldMousePosition.x < transform.position.x;
    }
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
      nextFire = Time.time + 1 / (WeaponConstants.weaponStats[weaponType].attackSpeed * attackSpeedMultiplier);
      ShootProjectiles();
    }
  }

  public void ShootProjectiles()
  {
    // Vector towards mouse
    Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 gunPosition = transform.Find("GunPosition").position;
    Vector2 dirTowardsMouse = worldMousePosition - gunPosition;

    // Randomize angle
    float randomAngleDelta = UnityEngine.Random.Range(-WeaponConstants.maxDispersionDegrees * (100 - WeaponConstants.weaponStats[weaponType].accuracy) / 100f,
  WeaponConstants.maxDispersionDegrees * (100 - WeaponConstants.weaponStats[weaponType].accuracy) / 100f) * Mathf.Deg2Rad;

    // Instantiate projectiles
    for (int i = 0; i < WeaponConstants.weaponStats[weaponType].projectileCount; i++)
    {

      float projectileAngle = Mathf.PI / 48f * (i - WeaponConstants.weaponStats[weaponType].projectileCount / 2);
      // Rotate vector towards mouse by random angle
      float randomAngle = Mathf.Atan2(dirTowardsMouse.y, dirTowardsMouse.x) + randomAngleDelta + projectileAngle;

      GameObject projectile = Instantiate(WeaponConstants.weaponStats[weaponType].projectilePrefab, gunPosition, Quaternion.identity);
      projectile.GetComponent<Projectile>().Init(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)), gunPosition, weaponType, damageMultiplier, attackRangeMultiplier);
    }
  }

  public void cameraFollow()
  {
    Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
  }

  public void UpdateMoney(int amount)
  {
    money += amount;
    UIManager.Instance.UpdateMoney(money);
  }

  public void Heal(float amount)
  {
    health = Mathf.Min(maxHealth, health + amount);
    UIManager.Instance.UpdateHealth(health, maxHealth);
  }

  public void IncreaseMaxHealth(float amount)
  {
    maxHealth += amount;
    health += amount;
    UIManager.Instance.UpdateHealth(health, maxHealth);
  }

  public void TakeDamage(float damage, Quaternion rotation)
  {

    if (playerBloodPrefab == null)
      playerBloodPrefab = Resources.Load<GameObject>("Prefabs/Particles/BloodRed");

    if (particleClock >= particleMinInterval)
    {
      GameObject particle = GameObject.Instantiate(playerBloodPrefab, transform.Find("BloodParticle").position, rotation);
      GameObject.Destroy(particle, 1.5f);
      particleClock = 0f;
    }

    health = Mathf.Max(0, health - damage);
    UIManager.Instance.UpdateHealth(health, maxHealth);
    UIManager.Instance.FlashScreen();
    if (health <= 0 && !isDead && !godMode)
    {
      Dies();
    }
  }

  public void Dies()
  {
    canShoot = false;
    canMove = false;
    isDead = true;
    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    GameManager.Instance.GameOver();
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
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
      }
    }
  }
}
