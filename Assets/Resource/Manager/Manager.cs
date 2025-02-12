using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;

    FoodManager _foodinstance;

    MoneyManager _moneyinstance;

    public static Manager Instance { get { Init(); return _instance; } }
    public static FoodManager FoodManager { get { InitFoodMenu(); return Instance._foodinstance; } }

    public static MoneyManager MoneyManager { get { InitMoneyMenu(); return Instance._moneyinstance; } }
    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);

            _instance = go.GetComponent<Manager>();
        }
    }


    static void InitFoodMenu()
    {
        if (Instance._foodinstance == null)
        {
            GameObject foodMenuObject = new GameObject("FoodMenu");
            foodMenuObject.transform.SetParent(_instance.transform);  // FoodMenu를 Manager 아래에 배치
            Instance._foodinstance = foodMenuObject.AddComponent<FoodManager>();
        }
    }

    static void InitMoneyMenu()
    {
        if(Instance._moneyinstance == null)
        {
            GameObject MoneyManagerObject = new GameObject("MoneyManager");
            MoneyManagerObject.transform.SetParent(_instance.transform);
            Instance._moneyinstance = MoneyManagerObject.AddComponent<MoneyManager>();
        }
    }
}

