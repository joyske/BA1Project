using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenHUD : MonoBehaviour
{
    public Transform optionsMenu;
    public void OpenOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
