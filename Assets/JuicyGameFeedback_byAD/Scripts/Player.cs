using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] FeedBack_Base PlayerJumpFeedbacks;
    private Rigidbody rb;
    public float jumpForce;
    private bool isGrounded;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float maxDistance;
    [SerializeField] Vector3 RayOriginOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnPlayerJumped()
    {
        print("Player Jumped");
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(rb.position + RayOriginOffset, Vector3.down, out RaycastHit hit, maxDistance, GroundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime);
            PlayerJumpFeedbacks.Play();

        }
    }
}
