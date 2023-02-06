using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ADFeedbacks playerjuice;
    private Rigidbody rb;
    public float jumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime);
            playerjuice.Play();
        }
    }
}
