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
    private bool hitGround = false;
    private AudioSource splashSound;
    public List<AudioClip> audioClips;
    private AudioClip currentClip;

    private BoxCollider boatColliderZone;
    private Transform player;

    void Awake()
    {
        
        floater = GetComponent<Floater>();

        if(SceneManager.GetActiveScene().buildIndex  == 1)
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

        splashSound = GetComponent<AudioSource>();
        splashSound.pitch = Random.Range(0.85f, 1.15f);

        if (GetComponent<SphereCollider>())
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = true;
        }

        if (GetComponent<MeshCollider>())
        {
            if (GetComponent<BoxCollider>())
            {
                transform.GetComponent<BoxCollider>().enabled = false;
                GetComponent<MeshCollider>().enabled = true;
            }
      
        }

        player = GameObject.FindWithTag("Boat").transform;
        boatColliderZone = player.GetChild(player.childCount - 1).GetComponent<BoxCollider>();


    }

    // Update is called once per frame
    void Update()
    {
        if (((floater.hitWater && leftBoat) || hitGround) && !subtractedCargo)
        {
            playerHUD.currentCargoAmount -= 1;
            subtractedCargo = true;

            currentClip = audioClips[Random.Range(0, audioClips.Count)];
            splashSound.clip = currentClip;
            splashSound.Play();

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

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer == 7)
        {
            if (leftBoat)
            {
                hitGround = true;
            }
        }
    }
}
