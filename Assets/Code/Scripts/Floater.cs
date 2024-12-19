using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBodyRef;
    public float depthBeforeSub;
    public float displacementAmount;
    public int floaters;
    public float waterDrag;
    public float waterAngularDrag;
    private WaterSurface water;
    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    private void Start()
    {
        water = GameObject.FindGameObjectWithTag("Ocean").GetComponent<WaterSurface>();
    }

    private void FixedUpdate()
    {
        rigidBodyRef.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

        Search.startPositionWS = transform.position;

        water.ProjectPointOnWaterSurface(Search, out SearchResult);

        if(transform.position.y < SearchResult.projectedPositionWS.y)
        {
            float displacementMulti = Mathf.Clamp01(SearchResult.projectedPositionWS.y - transform.position.y / depthBeforeSub) * displacementAmount;
            rigidBodyRef.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);
            rigidBodyRef.AddForce(displacementMulti * -rigidBodyRef.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBodyRef.AddTorque(displacementMulti * -rigidBodyRef.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
