using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private GameObject _tableObj;

    public List<GameObject> Tables = new List<GameObject>();
    public List<bool> TableBool;

    public int Tablenumber;
    //public delegate void OnTable();
    //public event OnTable InCustomer;



    public void Awake()
    {
        _tableObj = this.gameObject;

        for (int i = 0; i < _tableObj.transform.childCount; i++)
        {
            Tables.Add(_tableObj.transform.GetChild(i).gameObject);
        }

        TableBool = new List<bool>(new bool[Tables.Count]);

        
    }

    public GameObject GetTable()
    {
        for (int i = 0; i < TableBool.Count; i++)
        {
            if(!TableBool[i])
            {
                TableBool[i] = true;
                Tablenumber = i;

                return Tables[i];               
            }

        }

        return null;
    }

}
