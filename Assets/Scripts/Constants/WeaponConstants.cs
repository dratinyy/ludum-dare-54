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
            projectileCount = 1f,
            damage = 8f,
            attackSpeed = 3f,
            range = 6f,
            accuracy = 70f,

            piercing = 1,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 1 Shotgun
        new WeaponStats
        {
            projectileSpeed = 8f,
            projectileCount = 8f,
            damage = 13f,
            attackSpeed = 1.2f,
            range = 3f,
            accuracy = 0f, // Dispersion maximale

            piercing = 1,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 2 uzi
        new WeaponStats
        {
            projectileSpeed = 16f,
            projectileCount = 1f,
            damage = 4f,
            attackSpeed = 12f,
            range = 6f,
            accuracy = 0f,

            piercing = 1,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 3 auto rifle
        new WeaponStats
        {
            projectileSpeed = 12f,
            projectileCount = 1f,
            damage = 8f,
            attackSpeed = 7.5f,
            range = 15f,
            accuracy = 70f,

            piercing = 6,
            explosive = false,
            explosiveRange = 0f
        },
        // Weapon 4 Rocket Launcher
        new WeaponStats
        {
            projectileSpeed = 4f,
            projectileCount = 1f,
            damage = 30f,
            attackSpeed = 1.5f,
            range = 8f,
            accuracy = 60f,

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
        public float projectileCount;
        public float damage;
        public float attackSpeed;
        public float range;
        public float accuracy;

        public int piercing;
        public bool explosive;
        public float explosiveRange;
    }
}
