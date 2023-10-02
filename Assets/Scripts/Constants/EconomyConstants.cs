using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyConstants : MonoBehaviour
{

    public readonly static int tileBuyPrice = 100;
    public readonly static int tileRentBenefit = 23;
    public readonly static int tileSellPrice = 80;

    public readonly static int burgerPrice = 5;
    public readonly static int burgerHealth = 10;
    public readonly static int healpackPrice = 45;
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
        300,

        // Weapon 3 auto rifle
        1000,

        // Weapon 4 Rocket Launcher
        2000
    };

    public readonly static BonusStats[] bonusStats = new BonusStats[]
    {
        // Max Health
        new BonusStats
        {
            price = 30,
            increase = 20f, // not in percent
            maxQuantity = 15f
        },

        // Movespeed
        new BonusStats
        {
            price = 50,
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
            price = 70,
            increase = 10f, // in percent
            maxQuantity = 20f
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
