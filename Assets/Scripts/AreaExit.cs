using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {
    [SerializeField]
    private string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance areaEntrance;

    // Start is called before the first frame update
    void Start () {
        areaEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            SceneManager.LoadScene (areaToLoad);
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}