using System;
using UnityEngine;
using TMPro;

public class CarcinogenHandler : MonoBehaviour
{
    public static event Action OnCountChanged;

    public int totalCarcinogens = 3;

    private TextMeshProUGUI carcinogenText;

    public int CollectedCount { get; private set; } = 0;

    private void OnEnable()
    {
        CarcinogenScript.OnCarcinogenCollected += HandleCarcinogenCollected;
    }

    private void OnDisable()
    {
        CarcinogenScript.OnCarcinogenCollected -= HandleCarcinogenCollected;
    }

    private void Start()
    {
        GameObject uiObject = GameObject.FindGameObjectWithTag("CarcinogenCounterUI");
        carcinogenText = uiObject.GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    private void HandleCarcinogenCollected()
    {
        CollectedCount++;
        UpdateUI();
        OnCountChanged?.Invoke(); // fires only after count is fully updated
    }

    private void UpdateUI()
    {
        carcinogenText.text = $"Carcinogens: {CollectedCount}/{totalCarcinogens}";
    }

    public bool AllCollected()
    {
        return CollectedCount >= totalCarcinogens;
    }
}