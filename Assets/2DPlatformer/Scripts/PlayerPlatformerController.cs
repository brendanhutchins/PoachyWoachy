using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float constantRightwardMotion;
    public Text countText;


    public AudioClip CoinSound; //coin sound

    public AudioSource CoinSource;

    public AudioClip RainbowSound; //rainbow coin sound

    public AudioSource RainbowSource;


    public GameObject CompleteLevelUI;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int count;              //Integer to store the number of pickups collected so far.
    public bool slide;

    public AudioClip JumpSound; // jump sound
    public AudioSource JumpSource; // jump source

    public AudioClip SlideSound; //slide sound
    public AudioSource SlideSource;//slide source
   
    // Use this for initialization 
    void Awake () 
    {
        CoinSource.clip = CoinSound;// coin sound
        //Initialize count to zero.
        count = 0;
        CompleteLevelUI.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText();
        JumpSource.clip = JumpSound;
        SlideSource.clip = SlideSound;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = constantRightwardMotion;
        if (Input.GetButtonDown("Jump") && grounded)
        {

            velocity.y = jumpTakeOffSpeed;
            JumpSource.Play();
        }

        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
        {
            slide = true;
            SlideSource.Play();
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow) && grounded)
        {
            slide = false;
        }

        if (!grounded)
        {
            slide = false;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetBool("Slide", slide);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;
            CoinSource.Play();
            //Update the currently displayed count by calling the SetCountText function.
            SetCountText();
        }
        else if (other.gameObject.CompareTag("BigPickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 10;
            RainbowSource.Play();

            SetCountText();
        }

    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "C o u n t : " + count.ToString();
    }




    public void CompleteLevel() //this function is for the scene transition on game completion.
    {
        CompleteLevelUI.SetActive(true);

    }
    
}


