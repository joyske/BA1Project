using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cargo : MonoBehaviour
{
    private PlayerHUD playerHUD;
    private Floater floater;
    private bool subtractedCargo;
    private bool leftBoat = false;

    private BoxCollider boatColliderZone;
    private Transform player;

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
            transform.GetComponent<BoxCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = true;
        }

        player = GameObject.FindWithTag("Boat").transform;
        boatColliderZone = player.GetChild(player.childCount - 1).GetComponent<BoxCollider>();


    }

    // Update is called once per frame
    void Update()
    {
        if (floater.hitWater && !subtractedCargo && leftBoat)
        {
            playerHUD.currentCargoAmount -= 1;
            subtractedCargo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "SafeZone")
        {
            leftBoat = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SafeZone" && !subtractedCargo)
        {
            leftBoat = false;
        }
    }
}
