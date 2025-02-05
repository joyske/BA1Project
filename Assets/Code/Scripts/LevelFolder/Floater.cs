using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    //Adjustable buoyancy parameters
    public Rigidbody rigidBodyRef;
    public float depthBeforeSub;
    public float displacementAmount;
    public int floaters;
    public float waterDrag;
    public float waterAngularDrag;

    //Parameters used for calculation
    [HideInInspector]
    public bool isSubmerged;
    public bool hitWater = false;
    private WaterSurface water;
    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    private void Start()
    {
        //Get references

        hitWater = false;
        if(rigidBodyRef == null)
        {
            rigidBodyRef = GetComponent<Rigidbody>();
        }
        water = GameObject.FindGameObjectWithTag("Ocean").GetComponent<WaterSurface>();
    }

    private void FixedUpdate()
    {
        //Apply proportional gravity to each floater point
        rigidBodyRef.AddForceAtPosition(new Vector3 (0, Physics.gravity.y / floaters, 0), transform.position, ForceMode.Acceleration);

        //Get water height at current position
        Search.startPositionWS = transform.position;
        water.ProjectPointOnWaterSurface(Search, out SearchResult);

        //If floater under water, apply buoyancy force
        if (transform.position.y < SearchResult.projectedPositionWS.y)
        {
            isSubmerged = true;
            hitWater = true;

            float displacementMulti = Mathf.Clamp01(SearchResult.projectedPositionWS.y - transform.position.y / depthBeforeSub) * displacementAmount;

            //Counteract gravity
            rigidBodyRef.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);

            //Add drag friction counter force
            rigidBodyRef.AddForce(displacementMulti * -rigidBodyRef.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

            //Orient around water surface
            rigidBodyRef.AddTorque(displacementMulti * -rigidBodyRef.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else { isSubmerged = false; }
    }
}
