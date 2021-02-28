﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void ClickButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
