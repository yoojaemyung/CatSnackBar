using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MainUpgradeButton : MonoBehaviour
{
    private Transform buttonPanel;
    [SerializeField] private Button _mainUIButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _buttonDestory;
    [SerializeField] private TMP_Text[] _TextList;
    [SerializeField] private Button[] _upgradeButton;
    [SerializeField] private GameObject _upImg;
    void Awake()
    {
        buttonPanel = this.transform;

        _exitButton.onClick.AddListener(ClosedUI);

        _upgradeButton = buttonPanel.GetComponentsInChildren<Button>();
        _TextList = buttonPanel.GetComponentsInChildren<TMP_Text>();

        _TextList = System.Array.FindAll(_TextList, text => text.gameObject.name.Contains("UpgradeCostText"));

        for (int i = 0; i < _upgradeButton.Length; i++)
        {
            int index = i;
            _upgradeButton[i].onClick.AddListener(() => OnUpgradeButtonClicked(index));
        }

        Manager.MoneyManager.MoneyChanged += UpdateUI;
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void OnUpgradeButtonClicked(int value)
    {
        switch(value)
        {
            case 0:
                {
                    CustomerSpawn(value);
                    unActiveUI(value);

                    break;
                }
            case 1:
                {
                    CustomerSpawn(value);
                    unActiveUI(value);
                    break;
                }
            case 2:
                {
                    _player.SetActive(true);
                    unActiveUI(value);
                    break;
                }
            case 3:
                {
                    Manager.FoodManager._menu[0].FoodSecond -= 1f;
                    unActiveUI(value);
                    break;
                }
            case 4:
                {
                    Manager.FoodManager._menu[0].FoodPrice *= 2;
                    unActiveUI(value);
                    break;
                }
        }
    }

    private void UpdateUI(int money = -1)
    {


        for (int i = 0; i < _upgradeButton.Length; i++)
        {

            int upgrdeCost = GetCost(i);

            if (Manager.MoneyManager.Money >= upgrdeCost)
            {
                _upgradeButton[i].interactable = true;
                _upgradeButton[i].image.color = Color.green;
                
            }
            else
            {
                _upgradeButton[i].interactable = false;
                _upgradeButton[i].image.color = Color.gray;
                
            }
        }

        
    }


    private int GetCost(int index)
    {
        if(int.TryParse(_TextList[index].text , out int cost))
        {
            return cost;
        }
        return 0;
    }

    private void ClosedUI()
    {
        UI_Manager.Instance._mainUI.gameObject.SetActive(false);
    }

    private void CustomerSpawn(int value)
    {
        CustomerSpawn customerSpawn = FindObjectOfType<CustomerSpawn>();
        customerSpawn?.TriggerDeleteEvent();

    }

    private void unActiveUI(int value)
    {
        Manager.MoneyManager.SpendMoney(GetCost(value));

        _buttonDestory[value].SetActive(false);
    }
}
