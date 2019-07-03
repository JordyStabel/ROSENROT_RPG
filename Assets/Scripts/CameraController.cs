using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;

    public Tilemap map;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    public int musicIndex;
    private bool musicStarted;

    // Start is called before the first frame update
    void Start()
    {
        followTarget = PlayerController.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = map.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        // Set world bounds for the player
        PlayerController.instance.SetBounds(map.localBounds.min, map.localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        transform.position = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);

        // Constrain the camera within the actual map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z);

        if (!musicStarted)
        {
            AudioManager.instance.PlayerBackgroundMusic(musicIndex);
            musicStarted = true;
        }
    }
}