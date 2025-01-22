using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{

    private GameManagement gameManagement;

    public void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            gameManagement.LoadPlacementScene();
        }
    }
}
