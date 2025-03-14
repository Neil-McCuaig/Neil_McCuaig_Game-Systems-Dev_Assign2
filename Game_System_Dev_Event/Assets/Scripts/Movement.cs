using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    //Health stuff
    public int maxHealth = 100;
    public int currentHealth;

    public int enemyCount = 6;

    public HealthBar healthBar;

    //Movement stuff

    float moveSpeed = 20;
    float rotationSpeed = 4;
    float runningSpeed;
    float vaxis, haxis;
    public bool isJumping, isJumpingAlt, isGrounded = false;
    Vector3 movement;

    void Start()
    {
        Debug.Log("Initialized: (" + this.name + ")");

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void FixedUpdate()
    {
        /*  Controller Mappings */
        vaxis = Input.GetAxis("Vertical");
        haxis = Input.GetAxis("Horizontal");
        isJumping = Input.GetButton("Jump");
        isJumpingAlt = Input.GetKey(KeyCode.Joystick1Button0);

        //Simplified...
        runningSpeed = vaxis;


        if (isGrounded)
        {
            movement = new Vector3(0, 0f, runningSpeed * 8);        // Multiplier of 8 seems to work well with Rigidbody Mass of 1.
            movement = transform.TransformDirection(movement);      // transform correction A.K.A. "Move the way we are facing"
        }
        else
        {
            movement *= 0.70f;                                      // Dampen the movement vector while mid-air
        }

        GetComponent<Rigidbody>().AddForce(movement * moveSpeed);   // Movement Force


        if ((isJumping || isJumpingAlt) && isGrounded)
        {
            Debug.Log(this.ToString() + " isJumping = " + isJumping);
            GetComponent<Rigidbody>().AddForce(Vector3.up * 150);
        }



        if ((Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && !isJumping && isGrounded)
        {
            if (Input.GetAxis("Vertical") >= 0)
                transform.Rotate(new Vector3(0, haxis * rotationSpeed, 0));
            else
                transform.Rotate(new Vector3(0, -haxis * rotationSpeed, 0));

        }


        if (currentHealth <= 0)
        {
            GameOver();
        }

        if (enemyCount <= 0) 
        {
            victory();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        //Colliding with enemies deals damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            TakeDamage(20);

            enemyCount = enemyCount - 1;
        }
    }
    
    //Let's the player jump
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    //Sends you to either the game over or victory screens
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void victory()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}