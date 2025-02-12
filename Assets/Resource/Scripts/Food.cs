using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    public string FoodName { get; set; }
    public int FoodPrice { get; set; }
    public float FoodSecond { get; set; }
    public Food(string name, int price, float second)
    {
        FoodName = name;
        FoodPrice = price;
        FoodSecond = second;
    }

    public override string ToString()
    {
        return $"{FoodName}, {FoodPrice} minutes";
    }
}
