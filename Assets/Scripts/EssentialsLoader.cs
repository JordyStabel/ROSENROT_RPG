using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject UIScreen;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject audioManager;

    [SerializeField]
    private GameObject gameManager;

    // Awake is called before the first frame update
    void Awake()
    {
        if (UIFade.instance == null)
        {
            Instantiate(UIScreen);
        }
        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}