using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyConstants : MonoBehaviour
{

    public readonly static int tileBuyPrice = 100;
    public readonly static int tileRentBenefit = 26;
    public readonly static int tileSellPrice = 80;

    public readonly static int burgerPrice = 5;
    public readonly static int burgerHealth = 10;
    public readonly static int healpackPrice = 50;
    public readonly static int healpackHealth = 200;
    
    public readonly static int spaceshipPrice = 10000;

    public readonly static int dailyIncome = 120;
    public readonly static int numberOfWavesWithIncome = 8;

    public readonly static int[] weaponPrices = new int[]
    {
        // Weapon 0 Gun (cannot be bought)
        0,

        // Weapon 1 Shotgun
        150,

        // Weapon 2 uzi
        350,

        // Weapon 3 auto rifle
        1000,

        // Weapon 4 Rocket Launcher
        3500
    };

    public readonly static BonusStats[] bonusStats = new BonusStats[]
    {
        // Max Health
        new BonusStats
        {
            price = 25,
            increase = 20f, // not in percent
            maxQuantity = 15f
        },

        // Movespeed
        new BonusStats
        {
            price = 40,
            increase = 10f, // in percent
            maxQuantity = 5f
        },

        // Damage
        new BonusStats
        {
            price = 60,
            increase = 10f, // in percent
            maxQuantity = 15f
        },

        // Range
        new BonusStats
        {
            price = 60,
            increase = 10f, // in percent
            maxQuantity = 30f
        },

        // Attack Speed
        new BonusStats
        {
            price = 70,
            increase = 10f, // in percent
            maxQuantity = 30f
        }
    };

    public class BonusStats
    {
        public int price;
        public float increase;
        public float maxQuantity;
    }

}
