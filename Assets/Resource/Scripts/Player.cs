using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : StateMachine
{
    private int _count = 0;


    public float Speed = 1f; 
    public float Second = 1f; //코루틴 대기시간

    public bool MakeFood = false;
    public bool Waiting = false;
    
    private float _interactionDistance = 0.0001f;
    private bool _isCoroutineRunning = false; // 코루틴 대기처리

    public GameObject TargetTable;
    public Transform TargetPos;

    public string FoodName;
     
    public GameObject[] Sandwich;
    public GameObject SandwichImg;

    [HideInInspector]
    public Idle IdleState;
    public Move MoveState;
    public Waiting WaitingState;

    public CustomerSpawn CustomerSpawn;
    public Table PlayerTable;
    public Animator PlayerAnimator;

    void Awake()
    {

        IdleState = new Idle(this);
        MoveState = new Move(this);
        WaitingState = new Waiting(this);

        PlayerAnimator = GetComponent<Animator>();
        CustomerSpawn = FindObjectOfType<CustomerSpawn>();
        PlayerTable = FindObjectOfType<Table>();

    }


    void OnGUI()
    {
        //if (_currentState != null)
        //{
        //    // GUI 텍스트 스타일 설정
        //    GUIStyle style = new GUIStyle();
        //    style.fontSize = 13; // 글꼴 크기
        //    style.normal.textColor = Color.white; // 글꼴 색상
        //    style.alignment = TextAnchor.UpperLeft; // 텍스트 정렬

        //    // 현재 상태를 화면의 왼쪽 상단에 표시
        //    GUI.Label(new Rect(10, 10, 200, 30), _currentState._name, style);
        //}

        //if (_money != null)
        //{
        //    // GUI 텍스트 스타일 설정
        //    GUIStyle style = new GUIStyle();
        //    style.fontSize = 13; // 글꼴 크기
        //    style.normal.textColor = Color.white; // 글꼴 색상
        //    style.alignment = TextAnchor.UpperLeft; // 텍스트 정렬

        //    // 현재 상태를 화면의 왼쪽 상단에 표시
        //    GUI.Label(new Rect(10, 10, 200, 30), _money.ToString(), style);
        //}
    }



    public void MoveToTable()
    {
        Vector2 playerPosition = transform.position;
        Vector2 targetPosition = TargetPos.position;
        Vector2 targetPositionAdjusted = new Vector2(targetPosition.x, targetPosition.y - 0.2f);

        transform.position = Vector2.MoveTowards(playerPosition, targetPositionAdjusted, Speed * Time.deltaTime);
        float distance = Vector2.Distance(playerPosition, targetPositionAdjusted);

        if (distance <= _interactionDistance)
        {
            if (!MakeFood && !_isCoroutineRunning)
            {
                if(TargetPos != TargetTable.transform) // 목적지가 테이블이 아닐때, 조리대 앞일때
                {
                    this.PlayerAnimator.SetBool("Cookbool", true);

                    switch(FoodName)
                    {
                        case "Sandwich":
                            Sandwich[_count].GetComponent<SandWichStation>().MakeSandwich = true; // 샌드위치
                            break;
                    }
                    
                }     
                if (TargetPos == TargetTable.transform) // 주문받기
                {
                    this.PlayerAnimator.SetBool("Orderbool", true); 

                }
                _isCoroutineRunning = true;    
                StartCoroutine(WaitAndMove(Second));
            }
            else if (MakeFood)
            {
                MakeFood = false;
                int num = Manager.FoodManager.WaitingCustomer[0];
                PlayerTable.TableBool[num] = false;
                Second = 1f;

                if (FoodName != null)
                {
                    switch (FoodName)
                    {
                        case "Sandwich":
                            Manager.MoneyManager.AddMoney(Manager.FoodManager._menu[0].FoodPrice); 
                            FoodName = null;// 음식주문완료 조건 여기다가 하면 될듯?
                            SandwichImg.SetActive(false);
                            break;

                    }

                }
                Manager.FoodManager.WaitingCustomer.Remove(num);
                CustomerSpawn.TriggerDeleteEvent();
            }
        }
    }

    IEnumerator WaitAndMove(float second)
    {
        yield return new WaitForSeconds(second);

        this.PlayerAnimator.SetBool("Orderbool", false);
        
        // 코루틴 끝나면 상태리셋
        _isCoroutineRunning = false;

        if (second > 1 && !MakeFood) // 음식완성하고 돌아가는길
        {
            MakeFood = true;

            TargetPos = TargetTable.transform;

            SandwichImg.SetActive(true); // 샌드위치 이미지 생성

            Sandwich[_count].GetComponent<SandWichStation>().Canuse = false;
            Sandwich[_count].GetComponent<SandWichStation>().MakeSandwich = false;
            _count = 0;
            this.PlayerAnimator.SetBool("Cookbool", false);
        }
        else
            FindCookingPlace(FoodName);

        TurnRotation();
    }

    public void FindCookingPlace(string name)
    {
        switch (name)
        {
            case "Sandwich":

                if (TargetPos.name != FoodName)
                {

                    for (int i = 0; i < Sandwich.Length; i++)
                    {
                        SandWichStation station = Sandwich[i].GetComponent<SandWichStation>();


                        if (!station.Canuse)
                        {
                            station.Canuse = true;
                            _count = i;
                            TargetPos = Sandwich[_count].transform;
                            Second = Manager.FoodManager._menu[0].FoodSecond;
                            Waiting = false;

                            break;
                        }
                        else Waiting = true;
                    }
                }
                break;

        }
    }


    public void TurnRotation()
    {

        if (this.transform.position.x < this.TargetPos.position.x)
        {

            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
