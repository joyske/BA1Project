using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinController : MonoBehaviour
{
    private Dolphin dolphin;
    private SkinnedMeshRenderer meshRenderer;

    void Start()
    {
        dolphin = GetComponent<Dolphin>(); 
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ToggleDolphins(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "DolphinSpawnZone")
        {
            ToggleDolphins(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "DolphinSpawnZone")
        {
            ToggleDolphins(false);
        }
    }

    private void ToggleDolphins(bool inView)
    {
            dolphin.enabled = inView;
            meshRenderer.enabled = inView;
    }
}
