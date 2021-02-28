using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIVisible : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameFinishPanel;
    [SerializeField] private UnityEvent _openInventory;
    [SerializeField] private UnityEvent _openMainMenu;

    private void Start()
    {
        _inventory.SetActive(false);
        _mainMenu.SetActive(false);
        _gameFinishPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _openInventory.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _openMainMenu.Invoke();
        }
    }

}
