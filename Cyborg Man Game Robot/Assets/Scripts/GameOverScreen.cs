﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RespawnPlayer()
    {
       
    }

    public void ReturnToMenu()
    {
        GameObject.Find("samplePlayer").GetComponent<newController>().BackToMenu();
    }
}
