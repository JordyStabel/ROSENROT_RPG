using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Animator playerAnimator;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool isAllowedToMove = true;

    // Awake is called before the first frame update
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    void Update () {
        // Move the player
        if (isAllowedToMove) {
            playerRigidbody.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * movementSpeed, Input.GetAxisRaw ("Vertical") * movementSpeed);
        } else {
            playerRigidbody.velocity = Vector2.zero;
        }

        // Set values for the animations
        playerAnimator.SetFloat ("moveX", playerRigidbody.velocity.x);
        playerAnimator.SetFloat ("moveY", playerRigidbody.velocity.y);

        if (Input.GetAxisRaw ("Horizontal") == 1 ||
            Input.GetAxisRaw ("Horizontal") == -1 ||
            Input.GetAxisRaw ("Vertical") == 1 ||
            Input.GetAxisRaw ("Vertical") == -1) {
            if (isAllowedToMove) {
                playerAnimator.SetFloat ("lastMoveX", Input.GetAxisRaw ("Horizontal"));
                playerAnimator.SetFloat ("lastMoveY", Input.GetAxisRaw ("Vertical"));
            }
        }

        // Constrain the camera within the actual map
        transform.position = new Vector3 (Mathf.Clamp (transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp (transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z);
    }

    public void SetBounds (Vector3 bottomLeft, Vector3 topRight) {
        bottomLeftLimit = bottomLeft + new Vector3 (.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3 (-.5f, -1f, 0f);
    }
}