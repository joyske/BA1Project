using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dolphin : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    private float currentSpeed;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float SecondsBetweenJumps = 0.1f;
    [SerializeField] float waterDepth = -2.5f;
    [SerializeField] float destroyDelay = 1f;

    [SerializeField] LayerMask hitLayers;
    

    private float timePassed = 0f;
    private Vector3 moveDirection;
    private Vector3 newPosition = Vector3.zero;

    private Rigidbody rb;
    private Animator anim;
    bool isJumping;

    void Start()
    {
        currentSpeed = moveSpeed;
        //StartCoroutine(StartDelay());
        
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveDirection = transform.forward;
        timePassed = 0f;      
    }

    IEnumerator StartDelay()
    {
        float delayTime = 3f;
        yield return new WaitForSeconds(delayTime);
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        TerrainCheck();
    }
    private void FixedUpdate()
    {
        SetRotation();

        timePassed += Time.deltaTime;
        if (timePassed > SecondsBetweenJumps && !isJumping)
        {
            rb.useGravity = true;
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = true;              
        }

        Vector3 currentVelocity = GetCurrentVelocity();
        rb.AddForce((moveDirection * currentSpeed - currentVelocity), ForceMode.VelocityChange);
        
        SetAnimation();

    }   

    void TerrainCheck()
    {
        RaycastHit hit;
        float distance = 4f;

        if(Physics.Raycast(transform.position,transform.forward, out hit, distance, hitLayers))
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
            transform.eulerAngles = new Vector3(0f, -transform.rotation.eulerAngles.y, 0f);

            moveDirection = transform.forward;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.green);
            Debug.DrawRay(transform.position, new Vector3(transform.forward.x, transform.forward.y - 1f, transform.forward.z) * distance, Color.yellow);
        }
    }

    private void SetAnimation()
    {
        Vector3 velocity = rb.velocity.normalized;
        float animY = velocity.y;
        anim.SetFloat("yVelocity", animY);
        //Debug.Log(velocity);
    }

    private void SetRotation()
    {
        Vector3 currentDirection = GetCurrentDirection();

        if (!IsAboveWater() && !isJumping)
        {
            currentDirection = moveDirection;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.useGravity = false;
        }
        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        transform.rotation = rotation;
    }

    private Vector3 GetCurrentDirection()
    {
        Vector3 lastPosition = newPosition;
        newPosition = transform.position;

        Vector3 currentDirection = newPosition - lastPosition;
        currentDirection.Normalize();

        return currentDirection;
    }

    private Vector3 GetCurrentVelocity()
    {
        Vector3 playerHorizontalVelocity = rb.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;

    }

    private bool IsAboveWater() 
    {
        if(transform.position.y > waterDepth)
        {            
            return true;
        }

        if(rb.velocity.y < 0.05f && isJumping)
        {
            timePassed = 0f;
            isJumping = false;
        }
        
        return false;

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Boat")
        {
            StartCoroutine(DestroyDolphin());
            Debug.Log("HIIIII");
        }
    }

    IEnumerator DestroyDolphin()
    {
        
        yield return new WaitForSeconds(destroyDelay);
        Destroy(this.gameObject);
    }
}
