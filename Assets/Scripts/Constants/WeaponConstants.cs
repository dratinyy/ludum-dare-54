using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstants : MonoBehaviour
{

    public static readonly WeaponStats[] weaponStats = new WeaponStats[]
    {
        // Weapon 0 Gun
        new WeaponStats
        {
            projectileSpeed = 8f,
            damage = 20f,
            attackSpeed = 5f,
            range = 6f,
            randomAngle = 0.05f
        },
        // Weapon 1 Shotgun
        new WeaponStats
        {
            projectileSpeed = 8f,
            damage = 15f,
            attackSpeed = 3f,
            range = 3f,
            randomAngle = 0.25f
        },
        // Weapon 2 Auto Rifle
        new WeaponStats
        {
            projectileSpeed = 16f,
            damage = 4f,
            attackSpeed = 20f,
            range = 9f,
            randomAngle = 0.25f
        },
        // Weapon 3 Sniper
        new WeaponStats
        {
            projectileSpeed = 12f,
            damage = 8f,
            attackSpeed = 7.5f,
            range = 15f,
            randomAngle = 0.25f
        },
        // Weapon 4 Rocket Launcher
        new WeaponStats
        {
            projectileSpeed = 4f,
            damage = 30f,
            attackSpeed = 1.67f,
            range = 8f,
            randomAngle = 0.25f
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
        public float damage;
        public float attackSpeed;
        public float range;
        public float randomAngle;
    }
}
