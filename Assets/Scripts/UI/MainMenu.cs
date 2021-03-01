using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _controlPanel;
    private bool _menuIsOpen;
    private bool _controlPanelIsOpen;

    public void OpenCloseMenu()
    {
        Time.timeScale = 0f;
        _menuIsOpen = !_menuIsOpen;
        _mainMenu.SetActive(_menuIsOpen);
        Cursor.visible = true;
        if (!_menuIsOpen)
        {
            Cursor.visible = false;
            ContiniousGame();
        }
    }

    private void ContiniousGame()
    {
        Time.timeScale = 1f;
    }

    private void ControlPanel()
    {
        _controlPanelIsOpen = !_controlPanelIsOpen;
        _controlPanel.SetActive(_controlPanelIsOpen);
    }

}
