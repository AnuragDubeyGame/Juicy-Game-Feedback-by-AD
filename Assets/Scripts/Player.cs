using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ADFeedbacks playerjuice;
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
    private void Update()
    {
        isGrounded = Physics.Raycast(rb.position + RayOriginOffset, Vector3.down, out RaycastHit hit, maxDistance, GroundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime);
            playerjuice.Play();
        }
    }
}
