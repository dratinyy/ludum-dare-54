using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    private Transform player;

    private Transform GetPlayer()
    {
        if (player == null)
        {
            player = GameManager.Instance.Player;
        }
        return player;
    }

    private int[] bonusBought = new int[5] { 0, 0, 0, 0, 0 };
    private bool[] weaponBought = new bool[5] { true, false, false, false, false };

    private void BuyWeapon(int index)
    {
        if (weaponBought[index])
        {
            GetPlayer().GetComponent<Player>().SetWeaponType(index);
            return;
            //TODO: update prices and acquired weapons indicators
        }
        else
        {
            if (GetPlayer().GetComponent<Player>().money >= EconomyConstants.weaponPrices[index])
            {
                GetPlayer().GetComponent<Player>().money -= EconomyConstants.weaponPrices[index];
                weaponBought[index] = true;
                GetPlayer().GetComponent<Player>().SetWeaponType(index);
                //TODO: update prices and acquired weapons indicators
            }
        }
    }

    private void BuyBonus(int index)
    {
        if (bonusBought[index] >= EconomyConstants.bonusStats[index].maxQuantity ||
            GetPlayer().GetComponent<Player>().money < EconomyConstants.bonusStats[index].price)
        {
            return;
        }
        else
        {
            //TODO: update prices and acquired bonuses indicators
            GetPlayer().GetComponent<Player>().money -= EconomyConstants.bonusStats[index].price;
            bonusBought[index]++;
            switch (index)
            {
                // Max Health
                case 0:
                    GetPlayer().GetComponent<Player>().maxHealth += EconomyConstants.bonusStats[index].increase;
                    break;

                // Movespeed
                case 1:
                    GetPlayer().GetComponent<Player>().movespeedMultiplier += EconomyConstants.bonusStats[index].increase / 100f;
                    break;

                // Damage
                case 2:
                    GetPlayer().GetComponent<Player>().damageMultiplier += EconomyConstants.bonusStats[index].increase / 100f;
                    break;

                // Attack Speed
                case 3:
                    GetPlayer().GetComponent<Player>().attackSpeedMultiplier += EconomyConstants.bonusStats[index].increase / 100f;
                    break;

                // Range
                case 4:
                    GetPlayer().GetComponent<Player>().attackRangeMultiplier += EconomyConstants.bonusStats[index].increase / 100f;
                    break;
            }
        }
    }

    public void BuyBurger()
    {
        if (GetPlayer().GetComponent<Player>().money >= EconomyConstants.burgerPrice &&
            GetPlayer().GetComponent<Player>().Health < GetPlayer().GetComponent<Player>().maxHealth)
        {
            GetPlayer().GetComponent<Player>().money -= EconomyConstants.burgerPrice;
            GetPlayer().GetComponent<Player>().Heal(EconomyConstants.burgerHealth);
        }
    }

    public void BuySpaceship()
    {
        if (GetPlayer().GetComponent<Player>().money >= EconomyConstants.spaceshipPrice)
        {
            GetPlayer().GetComponent<Player>().money -= EconomyConstants.spaceshipPrice;
            // GameManager.Instance.Win();
        }
    }

    public void BuyWeapon0()
    {
        BuyWeapon(0);
    }

    public void BuyWeapon1()
    {
        BuyWeapon(1);
    }

    public void BuyWeapon2()
    {
        BuyWeapon(2);
    }

    public void BuyWeapon3()
    {
        BuyWeapon(3);
    }

    public void BuyWeapon4()
    {
        BuyWeapon(4);
    }

    public void BuyBonus0()
    {
        BuyBonus(0);
    }

    public void BuyBonus1()
    {
        BuyBonus(1);
    }

    public void BuyBonus2()
    {
        BuyBonus(2);
    }

    public void BuyBonus3()
    {
        BuyBonus(3);
    }

    public void BuyBonus4()
    {
        BuyBonus(4);
    }

}
