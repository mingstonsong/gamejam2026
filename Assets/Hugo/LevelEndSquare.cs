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
    private IIntroSequence introSequence; // any script implementing IIntroSequence

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        countText = GetComponentInChildren<TextMeshPro>();
        introSequence = GetComponent<IIntroSequence>();
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
                introSequence.PlaySequence(nextSceneName);
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}