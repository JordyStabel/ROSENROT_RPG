using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player
        playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, Input.GetAxisRaw("Vertical") * movementSpeed);

        // Set values for the animations
        playerAnimator.SetFloat("moveX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat("moveY", playerRigidbody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
