using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Dolphin : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float SecondsBetweenJumps = 0.1f;
    [SerializeField] float waterDepth = -2.5f;

    private float timePassed = 0f;
    private Vector3 moveDirection;
    private Vector3 newPosition = Vector3.zero;

    private Rigidbody rb;
    private Animator anim;
    bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveDirection = transform.forward;
        timePassed = 0f;      
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
        rb.AddForce((moveDirection * moveSpeed - currentVelocity), ForceMode.VelocityChange);
        
        SetAnimation();

    }   

    private void SetAnimation()
    {
        Vector3 velocity = rb.velocity.normalized;
        float animY = velocity.y;
        anim.SetFloat("yVelocity", animY);
        Debug.Log(velocity);
    }

    private void SetRotation()
    {
        Vector3 currentDirection = GetCurrentDirection();

        if (!IsPlayerAboveWater() && !isJumping)
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

    private bool IsPlayerAboveWater() 
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

}
