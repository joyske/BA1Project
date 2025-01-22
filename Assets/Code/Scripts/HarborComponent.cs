using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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

    private WaterSurface water;
    private GameObject playerRef;


    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Ocean").GetComponent<WaterSurface>();
        playerRef = GameObject.FindGameObjectWithTag("Boat");

        defaultFirstBand = water.largeBand0Multiplier;
        defaultSecondBand = water.largeBand1Multiplier;
    }

    void Update()
    {
        if (playerRef == null) { playerRef = GameObject.FindGameObjectWithTag("Boat"); }
        else
        {
            currentDistance = Vector3.Distance(transform.position, playerRef.transform.position);

            if (currentDistance < fadeDistance)
            {
                currentWaveHeightAlpha = (currentDistance / fadeDistance);
                if (currentWaveHeightAlpha > maxWaveHeightAlpha) { currentWaveHeightAlpha = maxWaveHeightAlpha; }
                if (currentWaveHeightAlpha < minWaveHeightAlpha) { currentWaveHeightAlpha = minWaveHeightAlpha; }
            }
            else { currentWaveHeightAlpha = maxWaveHeightAlpha;}

            water.largeBand0Multiplier = defaultFirstBand * currentWaveHeightAlpha;
            water.largeBand1Multiplier = defaultSecondBand * currentWaveHeightAlpha;
        }

        Debug.Log("CurrentDistance: " + currentDistance);
        Debug.Log("WaveHeightAlpha: " + currentWaveHeightAlpha);
    }
}
