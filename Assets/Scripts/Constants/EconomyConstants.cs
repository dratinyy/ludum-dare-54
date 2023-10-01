using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyConstants : MonoBehaviour
{

    public readonly static int tileBuyPrice = 100;
    public readonly static int tileRentBenefit = 20;
    public readonly static int tileSellPrice = 80;

    public readonly static int burgerPrice = 10;
    public readonly static int burgerHealth = 20;
    public readonly static int healpackHealth = 50;
    public readonly static int healpackPrice = 50;
    public readonly static int spaceshipPrice = 10000;

    public readonly static int[] weaponPrices = new int[]
    {
        // Weapon 0 Gun (cannot be bought)
        0,

        // Weapon 1 Shotgun
        200,

        // Weapon 2 Auto Rifle
        300,

        // Weapon 3 Sniper
        400,

        // Weapon 4 Rocket Launcher
        500
    };

    public readonly static BonusStats[] bonusStats = new BonusStats[]
    {
        // Max Health
        new BonusStats
        {
            price = 100,
            increase = 20f, // not in percent
            maxQuantity = 3f
        },

        // Movespeed
        new BonusStats
        {
            price = 100,
            increase = 10f, // in percent
            maxQuantity = 3f
        },

        // Damage
        new BonusStats
        {
            price = 100,
            increase = 10f, // in percent
            maxQuantity = 3f
        },

        // Attack Speed
        new BonusStats
        {
            price = 100,
            increase = 10f, // in percent
            maxQuantity = 3f
        },

        // Range
        new BonusStats
        {
            price = 100,
            increase = 10f, // in percent
            maxQuantity = 3f
        }
    };

    public class BonusStats
    {
        public int price;
        public float increase;
        public float maxQuantity;
    }

}
