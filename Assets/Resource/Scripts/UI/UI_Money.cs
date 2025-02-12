using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Money : MonoBehaviour
{
    [SerializeField] private TMP_Text MoneyText;
    void Awake()
    {

        Manager.MoneyManager.MoneyChanged += UI_MoneyUpdate;

        //MoneyText.text = Manager.MoneyManager.Money.ToString();
    }


    public void UI_MoneyUpdate(int money)
    {
        MoneyText.text = Manager.MoneyManager.Money.ToString();
    }

}
