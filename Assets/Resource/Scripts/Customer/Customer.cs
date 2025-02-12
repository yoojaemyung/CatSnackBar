using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private enum CustomerState
    {
        Moving,
        Waiting,
        Out
    }
    private CustomerState state = CustomerState.Moving;

    public GameObject CustomerMytable;
    public GameObject SandwichIcon;
    public Transform OutPosition;

    private float _interactionDistance = 0.001f;
    private float _speed = 1f;

    private Table _targetTable;
    private Transform _tablePos;
    private Food _orderFood;
    private Animator _customerAni;

    private int _tablenumber;

    private void Start()
    {
        _customerAni = GetComponent<Animator>();
        _customerAni.SetBool("Movebool", true);
        this.transform.rotation = Quaternion.Euler(0, 180, 0);

        SandwichIcon = this.transform.Find("SandwichIcon").gameObject;

        GameObject outPosObject = GameObject.Find("OutPos");
        if (outPosObject != null)
        {
            OutPosition = outPosObject.transform;
        }
        _targetTable = GameObject.FindObjectOfType<Table>();

        OrderFood();
        if (_targetTable != null)
        {
            CustomerMytable = _targetTable.GetTable();
            _tablenumber = _targetTable.Tablenumber;
            if(CustomerMytable != null)
            {
                _tablePos = CustomerMytable.transform;
            }
        }
    }
    private void Update()
    {
        switch(state)
        {
            case CustomerState.Moving:
                if(UI_Manager.Instance.FirstCheck)
                MoveToTable(); // 테이블로 이동
                break;

            case CustomerState.Waiting:
                if (Manager.FoodManager.WaitingCustomer.Count == 0)
                {
                    SandwichIcon.SetActive(false);
                    state = CustomerState.Out;
                    _customerAni.SetBool("Movebool", true);
                    Debug.Log("1번");
                }
                else if (!_targetTable.TableBool[_tablenumber])
                {
                    state = CustomerState.Out;
                    SandwichIcon.SetActive(false);
                    _customerAni.SetBool("Movebool", true);
                    Debug.Log("2번");
                }
                break;
            case CustomerState.Out:
                _tablePos = OutPosition;
                state = CustomerState.Moving;
                break;

        }
    }

    private void MoveToTable()
    {
        // 현재 위치와 목표 위치를 Vector2로 변환하여 이동 처리
        Vector2 customerPosition = transform.position;
        Vector2 targetPosition = _tablePos.position;

        Vector2 TargetPosition = new Vector2(targetPosition.x - 0.03f, targetPosition.y + 0.5f);

        // 목표 위치로 이동
        transform.position = Vector2.MoveTowards(customerPosition, TargetPosition, _speed * Time.deltaTime);

        float distance = Vector2.Distance(customerPosition, TargetPosition);

        // 목표 위치에 도착했는지 판단
        if (distance <= _interactionDistance)
        {
            SandwichIcon.SetActive(true); // 샌드위치 이미지 생성
            SetCustomerAni();

            if (_tablePos == OutPosition)
            {
                Destroy(this.gameObject);
                return;
            }

            state = CustomerState.Waiting;


            Manager.FoodManager.WaitingOrder.Add(_orderFood.FoodName);
            Manager.FoodManager.WaitingTablePos.Add(_tablePos); // 주문 리스트 테이블 포스 추가
            Manager.FoodManager.WaitingCustomer.Add(_tablenumber);
        }
    }


    private void OrderFood()
    {
        _orderFood = Manager.FoodManager.GetRandomFood();      
    }


    private void SetCustomerAni()
    {           
        _customerAni.SetBool("Movebool", false);
    }


    //private void TurnRotation()
    //{

    //    if (this.transform.position.x < this._tablePos.position.x)
    //    {

    //        this.transform.rotation = Quaternion.Euler(0, 180, 0);
    //    }
    //    else
    //    {
    //        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //}
}
