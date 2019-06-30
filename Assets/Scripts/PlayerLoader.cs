using UnityEngine;

public class PlayerLoader : MonoBehaviour {
    public GameObject player;

    // Awake is called before the first frame update
    private void Awake () {
        if (PlayerController.instance == null) {
            Instantiate (player);
        }
    }
}