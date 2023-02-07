using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        grounded,
        air
    }
    [SerializeField] FeedBack_Base PlayerJumpFeedbacks;
    PlayerState state;
    private void Start()
    {
        state = PlayerState.grounded;
    }
    public void OnPlayerJumped()
    {
        print("Player Jumped");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == PlayerState.grounded)
        {
            state = PlayerState.air;
            PlayerJumpFeedbacks.Play();
            StartCoroutine(setstatetogrounded());
        }
    }
    private IEnumerator setstatetogrounded()
    {
        yield return new WaitForSeconds(.5f);
        state = PlayerState.grounded;
    }
}
