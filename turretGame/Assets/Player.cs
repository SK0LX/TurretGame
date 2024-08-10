using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    public int maxHP;
    public int ammo;
    public int maxAmmo;
    public int allAmmo;
    public int coins;
    public int EXP;

    private void Start()
    {
        maxHP = 100;
        hp = 100;
        ammo = 100;
        maxAmmo = 100;
        allAmmo = 1000;
        EXP = 0;
        coins = 0;
    }

    public void DamageHp(int damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
    }

    public void HealHp(int heal)
    {
        hp += heal;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    public void SubtractCoins(int coins)
    {
        if (this.coins < coins)
        {
            print("Недостаточно монет");
        }
        else
        {
            this.coins -= coins;
        }
    }

    public void AddEXP(int newEXP)
    {
        EXP += newEXP;
    }

    public void ReloadGun()
    {
        if (allAmmo >= maxAmmo)
        {
            ammo = maxAmmo;
            allAmmo -= maxAmmo;
        }
        else
        {
            ammo = allAmmo;
            allAmmo = 0;
        }
    }
}

