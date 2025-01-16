using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    private GridData gridData;
    void Awake()
    {
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            
            gridData.placedObjects.Clear();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            
            Destroy(gridData.gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
