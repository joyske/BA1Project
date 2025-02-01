using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBackgroundSwitcher : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite GreenBackground;
    public Sprite RedBackground;
    bool active = true;

    public void ToggleBackground()
    {
        if (active)
        {
            backgroundImage.sprite = RedBackground;
        }
        else
        {
            backgroundImage.sprite = GreenBackground;
        }
        active = !active;
    }
}
