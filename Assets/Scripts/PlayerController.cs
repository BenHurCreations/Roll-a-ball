using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    // GUI text variables
    public TextMeshProUGUI countText;
    public GameObject winText;
    public TextMeshProUGUI highScoreText;
    public static float highScore = 1500;
    private int count;

    // Movement variables
    private Rigidbody rb;
    public float speed = 0;
    private float movementX;
    private float movementY;

    // Jump variables
    private Vector3 jump;
    public float jumpForce = 2.0f;
    private bool isGrounded;

    // Lightning variables
    public GameObject lightVolume;

    // Particle system variables
    public GameObject effect1;
    public GameObject effect2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.SetActive(false);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetHighScoreText()
    {
        highScoreText.text = "High score: \n" + highScore.ToString("0.00") + "(s)";
    }


    // This function is called every frame and checks if the GameObject touched the ground
    void OnCollisionStay()
    {
        isGrounded = true;
    }


    void OnJump()
    {
        if (isGrounded && rb.position.y == 0.5)
        {
            rb.AddForce(jump * jumpForce);
            isGrounded = false;
        }
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        string name = collision.collider.name;
        if (name == "Cube" || name == "Cube (1)" || name == "Cube (2)" || name == "Cube (3)")
        {
            rb.AddForce(0.0f, 400.0f, 0.0f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            effect1.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            effect1.SetActive(false);
            effect1.SetActive(true);
            float x = Random.Range(-9.5f, 9.5f);
            float y = Random.Range(0.5f, 2f);
            float z = Random.Range(-9.5f, 9.5f);
            other.gameObject.transform.position = new Vector3(x, y, z);
            count = count + 1;
            SetCountText();
        }
        
        if (count == 15)
        {
            winText.SetActive(true);
            float winTime = Time.timeSinceLevelLoad;
            if (winTime < highScore)
            {
                highScore = winTime;
            }
            SetHighScoreText();
        }

        if (other.gameObject.CompareTag("Capsule"))
        {
            effect2.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            effect2.SetActive(false);
            effect2.SetActive(true);
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(2.0f, 3.0f);
            float z = Random.Range(-5f, 5f);
            other.gameObject.transform.position = new Vector3(x, y, z);
            if (lightVolume.activeSelf == true)
            {
                lightVolume.SetActive(false);
            }
            else
            {
                lightVolume.SetActive(true);
            }
        }
    }

    float GetHighScore()
    {
        return highScore;
    }
}
