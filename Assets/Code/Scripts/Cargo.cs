using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cargo : MonoBehaviour
{
    private PlayerHUD playerHUD;
    private Floater floater;
    private bool subtractedCargo;

    void Awake()
    {
        
        floater = GetComponent<Floater>();

        if(SceneManager.GetActiveScene().buildIndex  == 0)
        {
            floater.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent <Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().mass = 1f;
            return;
        }
        playerHUD = GameObject.FindWithTag("HUD").GetComponent<PlayerHUD>();
        floater.enabled = true;
        GetComponent<Rigidbody>().useGravity = false;

        if (GetComponent<SphereCollider>())
        {
            transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (floater.hitWater && !subtractedCargo)
        {
            playerHUD.currentCargoAmount -= 1;
            subtractedCargo = true;
        }
    }
}
