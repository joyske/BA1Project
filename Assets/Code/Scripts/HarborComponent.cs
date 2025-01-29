using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Linq;

public class HarborComponent : MonoBehaviour
{
    public float fadeDistance = 200.0f;
    public float minWaveHeightAlpha = 0.0f;
    public float maxWaveHeightAlpha = 1.0f;

    public bool isTargetHarbor;

    private float currentWaveHeightAlpha;
    private float currentDistance;
    private float defaultFirstBand;
    private float defaultSecondBand;

    public GameObject goalCylinder;

    private WaterSurface water;
    private GameObject playerRef;

    private List<HarborComponent> harborComponents;
    private HarborComponent closestHarbor;


    void Start()
    {
        closestHarbor = GetComponent<HarborComponent>();

        water = GameObject.FindGameObjectWithTag("Ocean").GetComponent<WaterSurface>();
        
        playerRef = GameObject.FindGameObjectWithTag("Boat");

        defaultFirstBand = water.largeBand0Multiplier;
        defaultSecondBand = water.largeBand1Multiplier;

        if (!isTargetHarbor) { Destroy(goalCylinder); }
        else { harborComponents = FindObjectsByType<HarborComponent>(FindObjectsSortMode.None).ToList(); }        
    }

    void Update()
    {
        currentDistance = Vector3.Distance(transform.position, playerRef.transform.position);

        if (isTargetHarbor)
        {
            for (int i = 0; i < harborComponents.Count; i++) { if (harborComponents[i].currentDistance < closestHarbor.currentDistance) { closestHarbor = harborComponents[i]; } }

            if (playerRef == null) { playerRef = GameObject.FindGameObjectWithTag("Boat"); }
            else
            {
                if (closestHarbor.currentDistance < closestHarbor.fadeDistance)
                {
                    currentWaveHeightAlpha = (closestHarbor.currentDistance / closestHarbor.fadeDistance);
                    if (currentWaveHeightAlpha > closestHarbor.maxWaveHeightAlpha) { currentWaveHeightAlpha = closestHarbor.maxWaveHeightAlpha; }
                    if (currentWaveHeightAlpha < closestHarbor.minWaveHeightAlpha) { currentWaveHeightAlpha = closestHarbor.minWaveHeightAlpha; }
                }
                else { currentWaveHeightAlpha = closestHarbor.maxWaveHeightAlpha; }

                water.largeBand0Multiplier = defaultFirstBand * currentWaveHeightAlpha;
                water.largeBand1Multiplier = defaultSecondBand * currentWaveHeightAlpha;
            }
        }
    }
}
