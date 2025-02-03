using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Linq;
using System.Runtime.CompilerServices;

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

    private float waveHeightAlpha;
    private int harborsInRange;
    private float lowestWaveHeightAlpha;

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
            if (playerRef == null) { playerRef = GameObject.FindGameObjectWithTag("Boat"); }
            else { calculateCurrentWaveHeightAlpha(); }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void calculateCurrentWaveHeightAlpha()
    {
        currentWaveHeightAlpha = 1;
        lowestWaveHeightAlpha = 1;

        for (int i = 0; i < harborComponents.Count; i++)
        {
            if (harborComponents[i].currentDistance < harborComponents[i].fadeDistance)
            {
                waveHeightAlpha = ((harborComponents[i].maxWaveHeightAlpha - harborComponents[i].minWaveHeightAlpha) * (harborComponents[i].currentDistance / harborComponents[i].fadeDistance) + harborComponents[i].minWaveHeightAlpha);
                if (waveHeightAlpha > harborComponents[i].maxWaveHeightAlpha) { waveHeightAlpha = harborComponents[i].maxWaveHeightAlpha; }
                if (waveHeightAlpha < harborComponents[i].minWaveHeightAlpha) { waveHeightAlpha = harborComponents[i].minWaveHeightAlpha; }
                if (waveHeightAlpha < lowestWaveHeightAlpha) { lowestWaveHeightAlpha = waveHeightAlpha; }
                
            }

            
            if (i == harborComponents.Count - 1) 
            { 
                currentWaveHeightAlpha = lowestWaveHeightAlpha;

                water.largeBand0Multiplier = defaultFirstBand * currentWaveHeightAlpha;
                water.largeBand1Multiplier = defaultSecondBand * currentWaveHeightAlpha;
            }
        }
    }
}
