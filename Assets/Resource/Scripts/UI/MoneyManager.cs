using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public event Action<int> MoneyChanged;

    private int money = 2;

    public int Money
    {
        get => money;
        set
        {
            money = value;
            MoneyChanged?.Invoke(money);
        }
    }


    public void AddMoney(int value)
    {
        Money += value;
    }

    public bool SpendMoney(int value)
    {
        if (money >= value)
        {
            Money -= value;
            return true;
        }
        else
            return false;
    }

}
