using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private int count;

    //used for jumping
    private bool isGrounded;
    private float jumpSpeed = 50f;

    float objectHeight = 0;
    int winCount = 0;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;

    public Vector3 lastVelocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        MeshRenderer mr = GetComponent<MeshRenderer>();
        objectHeight = mr.bounds.extents.y;

        winCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }
    private void Update()
    {
        lastVelocity = rb.velocity;
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float moveUp = 0;

        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, objectHeight + .01f);

        if(hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
        
        if(isGrounded == true)
        {
            moveUp = Input.GetAxis("Jump");
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUp * jumpSpeed, moveVertical);

        rb.AddForce(movement * speed);

        rb.velocity /= 1.1f;

        //if player falls off edge, respawn in center
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0f, .5f, 0f);
        }     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= winCount)
        {
            winText.text = "You Win!";
        }
    }
}
