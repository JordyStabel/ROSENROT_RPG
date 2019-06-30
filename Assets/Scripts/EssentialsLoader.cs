using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {
    [SerializeField]
    private GameObject UIScreen;
    [SerializeField]
    private GameObject player;
    // Awake is called before the first frame update
    void Awake () {
        if (UIFade.instance == null) {
            Instantiate (UIScreen);
        }
        if (PlayerController.instance == null) {
            Instantiate (player);
        }
    }

    // Update is called once per frame
    void Update () {

    }
}