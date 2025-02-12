using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainUIButton : MonoBehaviour
{

    [SerializeField] private Button _activeButton;
    void Start()
    {
        _activeButton.onClick.AddListener(OnUI);
    }

    private void OnUI()
    {
        UI_Manager.Instance._mainUI.SetActive(true);
        UI_Manager.Instance.CloseAllUI();
    }
}
