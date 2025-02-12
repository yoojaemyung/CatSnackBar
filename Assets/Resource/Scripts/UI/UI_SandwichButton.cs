using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SandwichButton : MonoBehaviour
{
    [SerializeField] private Button _sandWichMenuButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _firstMenuButton;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _sliderfill;

    [SerializeField] private TMP_Text _upgradeLevelText;
    [SerializeField] private TMP_Text _sandWichPriceText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _upgradeCostText;

    [SerializeField] private GameObject _upImg;
    [SerializeField] private GameObject[] _starImg;
    [SerializeField] private GameObject _moneyUI;

    private int _upgradeCost = 2;
    private int _upgradeLevel = 1;
    private int _maxLevel = 20;
    private Animation _sandwichAni;
    

    [SerializeField] private GameObject _sandWichImg;


    private void Start()
    {    
        _sandWichMenuButton.onClick.AddListener(() => UI_Manager.Instance.ShowMenuUI());
        _upgradeButton.onClick.AddListener(UpgradeMenu);
        _firstMenuButton.onClick.AddListener(AddButton);

        _upgradeCostText = _upgradeButton.GetComponentInChildren<TMP_Text>();
        _sandwichAni = _sandWichMenuButton.GetComponent<Animation>();

        Manager.MoneyManager.MoneyChanged += UpdateUpgradeUI;      
    }

    private void UpdateUpgradeUI(int money = -1)
    {
        if (_upgradeLevel == _maxLevel)
        {
            _upgradeCostText.text = "Max";
            _upgradeButton.interactable = false;
            _upgradeButton.image.color = Color.gray;

            _upImg.SetActive(false);
            return;
        }      
        if (Manager.MoneyManager.Money >= _upgradeCost)
        {
            _upgradeButton.interactable = true;
            _upgradeButton.image.color = Color.green;
            _upImg.SetActive(true);
        }
        else
        {
            _upgradeButton.interactable = false;
            _upgradeButton.image.color = Color.gray;
            _upImg.SetActive(false);
        }
        _upgradeCostText.text = _upgradeCost.ToString();
        _sandWichPriceText.text = Manager.FoodManager._menu[0].FoodPrice.ToString();
        _timeText.text = $"{Manager.FoodManager._menu[0].FoodSecond} S";
    }
    private void UpgradeMenu()
    {
        if (_upgradeLevel >= _maxLevel)
            return;

        if (_upgradeLevel == 9)
            Manager.FoodManager._menu[0].FoodPrice += 15; // 샌드위치 가격
        else Manager.FoodManager._menu[0].FoodPrice += 3;

        if (Manager.MoneyManager.SpendMoney(_upgradeCost))
        {
            _upgradeCost += 3;

            if (_upgradeLevel < _maxLevel)
            {
                _upgradeLevel++;
                UpdateSlider();
            }
            _upgradeLevelText.text = $"레벨  {_upgradeLevel}";

            if (_upgradeLevel == 10)
                _starImg[0].SetActive(true);
            else if (_upgradeLevel == _maxLevel)
                _starImg[1].SetActive(true);
            
            UpdateUpgradeUI();
        }
    }

    private void AddButton()
    {
        UI_Manager.Instance.FirstCheck = true;

        UI_Manager.Instance.ShowMenuUI();

        Manager.MoneyManager.SpendMoney(2);

        _sandWichMenuButton.onClick.AddListener(PlayAnimaiton); // 버튼에 애니메이션 실행추가

        _sandWichImg.SetActive(true);
        _moneyUI.SetActive(true);
    }

    private void PlayAnimaiton()
    {
        if (_sandwichAni.IsPlaying("SandwichButton"))
        {
            _sandwichAni.Stop("SandwichButton");
        }

        _sandwichAni.Play("SandwichButton");
    }

    private void UpdateSlider()
    {
        _slider.value = (float)_upgradeLevel / _maxLevel;
        _sliderfill.SetActive(true);
    }
}
