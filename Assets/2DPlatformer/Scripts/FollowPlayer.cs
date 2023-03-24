using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class FollowPlayer : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float constantRightwardMotion;

    private SpriteRenderer spriteRenderer;
    //private Animator animator;
    public bool slide;


    public GameObject player;       //Public variable to store a reference to the player game object

    // Use this for initialization 
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = constantRightwardMotion;

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));

        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        targetVelocity = move * maxSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        if (other.gameObject.CompareTag("Jumper"))
        {
            velocity.y = jumpTakeOffSpeed;
        }
    }
}


