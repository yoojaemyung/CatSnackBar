using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWichStation : MonoBehaviour
{
    public bool Canuse = false;
    public bool MakeSandwich = false;

    private Animator _sandWichAni;

    private void Awake()
    {
        _sandWichAni = GetComponent<Animator>();
    }
    private void Update()
    {
        if (MakeSandwich)
        {
            _sandWichAni.SetBool("Sandwichbool", true);
        }
        else _sandWichAni.SetBool("Sandwichbool", false);
    }
}
