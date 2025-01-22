using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHUDManager : MonoBehaviour
{
    public Transform PauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
