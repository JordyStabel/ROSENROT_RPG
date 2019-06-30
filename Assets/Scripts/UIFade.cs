using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

    public static UIFade instance;

    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private float fadeSpeed;

    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;

    // Start is called before the first frame update
    void Start () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (shouldFadeToBlack) {
            fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards (fadeImage.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeImage.color.a == 1f)
                shouldFadeToBlack = false;
        }

        if (shouldFadeFromBlack) {
            fadeImage.color = new Color (fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards (fadeImage.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeImage.color.a == 0f)
                shouldFadeFromBlack = false;
        }
    }
    public void FadeToBlack () {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack () {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
}