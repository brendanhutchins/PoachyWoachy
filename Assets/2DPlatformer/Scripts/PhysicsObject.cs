using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f; //scale gravity
    public LayerMask groundMask;
    public float distanceGrounded = 0.5f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f; //How much it takes to check for collisions
    protected const float shellRadius = 0.01f;

    // Store rigidbody component reference
    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); //Use Layer Matrix to set which layer overlap
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime; //use default gravity value * scaled gravity
        velocity.x = targetVelocity.x;

        //grounded = false;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 deltaPosition = velocity * Time.deltaTime; //Determine where next position will be based on gravity

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y; //Deals with vertical movement

        Movement(move, true); //Move object based on gravity calculations by moving rigidbody

        CalculateIsGrounded();
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        // Apply collision only if distance is greater than 0
        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    //grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        // Add movement vector to rigidbody to move object every frame
        rb2d.position = rb2d.position + move.normalized * distance;
    }
    void CalculateIsGrounded()
    {
        Ray2D r = new Ray2D(this.transform.position,Vector3.down);
        RaycastHit2D h;

        if ((h = Physics2D.Raycast(r.origin,r.direction,float.MaxValue,groundMask.value)).collider != null)
        {
            //Debug.Log(h.collider.gameObject.name);
            if (h.distance < distanceGrounded)
            {
                grounded = true;
            }

            else
            {
                grounded = false;
            }
        }
        else
        {
            grounded = false;
        }
    }
}
