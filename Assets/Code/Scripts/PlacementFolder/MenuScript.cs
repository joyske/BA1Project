using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] PlacementSystem placementSystem;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartSimulation()
    {
        placementSystem.StartSimulation();
    }

    public void ResetSimulation()
    {
        placementSystem.ResetObjects();
    }
}
