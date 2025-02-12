using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodManager : MonoBehaviour
{
    public List<Food> _menu = new List<Food>();
 
    public List<string> WaitingOrder = new List<string>();

    public List<int> WaitingCustomer = new List<int>(); 

    public List<Transform> WaitingTablePos = new List<Transform>();



    public int money;

    void Awake()
    {
        _menu.Add(new Food("Sandwich", 10, 3f));

    }


    public Food GetRandomFood()
    {
        int randomIndex = Random.Range(0, _menu.Count);
        return _menu[randomIndex];
    }

    public void DecreaseSecond(string Foodname , float second)
    {
        int index = 0;

        for(int i = 0; i < _menu.Count; i++)
        {
            if (_menu[i].FoodName == Foodname)
            {
                //food = _menu[i];
                index = i;
                break;
            }    
        }
        Food food = _menu[index];  // 해당 인덱스의 Food 객체 가져오기
        food.FoodSecond -= second;  // second 값을 amount만큼 감소

    }

}
