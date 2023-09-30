using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstants : MonoBehaviour
{

    public static readonly WeaponStats[] weaponStats = new WeaponStats[]
    {
        // Weapon 0
        new WeaponStats
        {
            projectileSpeed = 10f,
            damage = 10f,
            attackSpeed = 5f,
            range = 10f
        }
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
    }
}
