using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void Button_Start()
    {
        SceneManager.LoadScene("InGame");
    }

    public void Button_Exit()
    {
        Application.Quit();
    }
}
