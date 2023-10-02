using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyConstants : MonoBehaviour
{

    public readonly static int tileBuyPrice = 100;
    public readonly static int tileRentBenefit = 27;
    public readonly static int tileSellPrice = 80;

    public readonly static int burgerPrice = 10;
    public readonly static int burgerHealth = 20;
    public readonly static int healpackPrice = 50;
    public readonly static int healpackHealth = 250;
    
    public readonly static int spaceshipPrice = 10000;

    public readonly static int dailyIncome = 100;
    public readonly static int numberOfWavesWithIncome = 7;

    public readonly static int[] weaponPrices = new int[]
    {
        // Weapon 0 Gun (cannot be bought)
        0,

        // Weapon 1 Shotgun
        150,

        // Weapon 2 Auto Rifle
        300,

        // Weapon 3 Sniper
        500,

        // Weapon 4 Rocket Launcher
        1000
    };

    public readonly static BonusStats[] bonusStats = new BonusStats[]
    {
        // Max Health
        new BonusStats
        {
            price = 30,
            increase = 20f, // not in percent
            maxQuantity = 10f
        },

        // Movespeed
        new BonusStats
        {
            price = 50,
            increase = 10f, // in percent
            maxQuantity = 10f
        },

        // Damage
        new BonusStats
        {
            price = 70,
            increase = 10f, // in percent
            maxQuantity = 10f
        },

        // Range
        new BonusStats
        {
            price = 70,
            increase = 10f, // in percent
            maxQuantity = 10f
        },

        // Attack Speed
        new BonusStats
        {
            price = 80,
            increase = 10f, // in percent
            maxQuantity = 10f
        }
    };

    public class BonusStats
    {
        public int price;
        public float increase;
        public float maxQuantity;
    }

}
