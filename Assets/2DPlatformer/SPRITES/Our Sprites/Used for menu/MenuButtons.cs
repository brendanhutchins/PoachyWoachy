using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour {

    public void startGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void exitGame()
    {
        Debug.Log("Has quit");
        Application.Quit();
    }

}
