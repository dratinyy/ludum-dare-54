using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstants : MonoBehaviour
{

    public static readonly float maxDispersionDegrees = 20f;

    /**
    * Weapon Stats
    * projectileSpeed: vitesse du projectile dans le jeu
    * projectileCount: nombre de projectiles tirés par tir
    * damage: dégâts infligé à l'ennemi lors d'une collision
    * attackSpeed: vitesse de tir en balles par seconde
    * range: portée du projectile
    * accuracy: dispersion des projectiles. 100 = pas de dispersion, 0 = jusqu'à 30 degrés de dispersion
    *
    * piercing: nombre de cibles que le projectile peut traverser
    * explosive: le projectile explose à l'impact
    * explosiveRange: rayon de l'explosion
    */
    public static readonly WeaponStats[] weaponStats = new WeaponStats[]
    {
        // Weapon 0 Gun
        new WeaponStats
        {
            projectileSpeed = 8f,
            projectileCount = 1,
            damage = 9f,
            attackSpeed = 3.5f,
            range = 6f,
            accuracy = 70f,

            piercing = 1,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 1 Shotgun
        new WeaponStats
        {
            projectileSpeed = 10f,
            projectileCount = 13,
            damage = 14f,
            attackSpeed = 1.3f,
            range = 2.5f,
            accuracy = 80f,

            piercing = 1,
            explosive = false,
            explosiveRange = 100f
        },
        // Weapon 2 uzi
        new WeaponStats
        {
            projectileSpeed = 25f,
            projectileCount = 1,
            damage = 6f,
            attackSpeed = 26f,
            range = 4f,
            accuracy = 0f,

            piercing = 1,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 3 auto rifle
        new WeaponStats
        {
            projectileSpeed = 20f,
            projectileCount = 1,
            damage = 11f,
            attackSpeed = 19f,
            range = 5f,
            accuracy = 50f,

            piercing = 2,
            explosive = false,
            explosiveRange = 0.5f
        },
        // Weapon 4 Rocket Launcher
        new WeaponStats
        {
            projectileSpeed = 8f,
            projectileCount = 1,
            damage = 40f,
            attackSpeed = 4.5f,
            range = 6.5f,
            accuracy = 0f,

            piercing = 1,
            explosive = true,
            explosiveRange = 1.5f
        },
    };

    void Start()
    {
        for (int i = 0; i < weaponStats.Length; i++)
        {
            weaponStats[i].projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile/Projectile" + i);
        }
    }

    public class WeaponStats
    {
        public GameObject projectilePrefab;
        public float projectileSpeed;
        public int projectileCount;
        public float damage;
        public float attackSpeed;
        public float range;
        public float accuracy;

        public int piercing;
        public bool explosive;
        public float explosiveRange;
    }
}
