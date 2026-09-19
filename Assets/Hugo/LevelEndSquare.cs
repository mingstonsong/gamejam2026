using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEndSquare : MonoBehaviour
{
    public string nextSceneName;

    [Range(0f, 1f)]
    public float transparency = 0.5f;

    private SpriteRenderer sr;
    private TextMeshPro countText;
    private CarcinogenHandler carcinogenHandler;
    private EvolvingIntroSequence introSequence; // optional, only present on this one scene's instance

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        countText = GetComponentInChildren<TextMeshPro>();
        introSequence = GetComponent<EvolvingIntroSequence>();
    }

    private void OnEnable()
    {
        CarcinogenHandler.OnCountChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        CarcinogenHandler.OnCountChanged -= UpdateDisplay;
    }

    private void Start()
    {
        GameObject handlerObject = GameObject.FindGameObjectWithTag("CarcinogenHandler");
        carcinogenHandler = handlerObject.GetComponent<CarcinogenHandler>();

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        Color baseColor = carcinogenHandler.AllCollected() ? Color.green : Color.red;
        sr.color = new Color(baseColor.r, baseColor.g, baseColor.b, transparency);

        countText.text = $"{carcinogenHandler.CollectedCount}/{carcinogenHandler.totalCarcinogens}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && carcinogenHandler.AllCollected())
        {
            if (introSequence != null)
            {
                // This scene has a special intro sequence — let it handle everything
                introSequence.PlaySequence(nextSceneName);
            }
            else
            {
                // Every other scene: just load immediately, as before
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}