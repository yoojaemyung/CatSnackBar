using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager Instance { get; private set; }

    [SerializeField] private List<GameObject> _uiPanels;
    [SerializeField] private GameObject _buttonRemove;

    [SerializeField] private GameObject _firstUI;
    [SerializeField] private GameObject _menuUI; // 메뉴 UI 패널 추가
    [SerializeField] public GameObject _mainUI;

    private GameObject _activeUI;

    public bool FirstCheck = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            CloseAllUI();
        }
    }


    public void ToggleUI(GameObject uiToActivate)
    {
        CloseAllUI();

        if (_activeUI != null)
            _activeUI.SetActive(true);

        uiToActivate.SetActive(true);
        _activeUI = uiToActivate;


    }

    public void CloseAllUI()
    {
        foreach (var panel in _uiPanels)
            panel.SetActive(false);

        _activeUI = null;

    }


    public void ShowMenuUI()
    {
        if (!FirstCheck)
        {
            ToggleUI(_firstUI);
        }
        else
        {
            ToggleUI(_menuUI);
        }

    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    public void RemoveButton()
    {
        // 버튼을 비활성화하거나 삭제합니다.
        _buttonRemove.SetActive(false); // 버튼을 비활성화할 경우
        // 또는
        // Destroy(buttonToRemove); // 버튼을 완전히 삭제할 경우

        // 부모 객체에 Layout을 업데이트하도록 합니다.
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
