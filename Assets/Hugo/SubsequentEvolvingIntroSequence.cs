using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubsequentEvolvingIntroSequence : MonoBehaviour, IIntroSequence
{
    public float lineDelay = 0.5f; // unscaled seconds between each line

    private GameObject evolvingPanel;
    private GameObject line1;
    private GameObject line2;
    private GameObject line3;
    private GameObject line4;
    private GameObject continuePrompt;

    private bool waitingForContinue = false;
    private bool sequenceRunning = false;
    private string sceneToLoad;

    private void Start()
    {
        evolvingPanel = GameObject.FindGameObjectWithTag("SubsequentEvolvingTextUI");
        line1 = evolvingPanel.transform.Find("Line1").gameObject;
        line2 = evolvingPanel.transform.Find("Line2").gameObject;
        line4 = evolvingPanel.transform.Find("Line4").gameObject;
        line3 = evolvingPanel.transform.Find("Line3").gameObject;
        continuePrompt = evolvingPanel.transform.Find("ContinuePrompt").gameObject;

        evolvingPanel.SetActive(false);
    }

    private void Update()
    {
        if (waitingForContinue && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            evolvingPanel.SetActive(false);
            waitingForContinue = false;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void PlaySequence(string nextScene)
    {
        if (sequenceRunning) return;

        sequenceRunning = true;
        sceneToLoad = nextScene;
        Time.timeScale = 0f;
        evolvingPanel.SetActive(true);
        StartCoroutine(RevealSequence());
    }

    private IEnumerator RevealSequence()
    {
        line1.SetActive(false);
        line2.SetActive(false);
        line4.SetActive(false);
        line3.SetActive(false);
        continuePrompt.SetActive(false);

        line1.SetActive(true);
        yield return new WaitForSecondsRealtime(lineDelay);

        line2.SetActive(true);
        yield return new WaitForSecondsRealtime(lineDelay);
        
        line4.SetActive(true);
        yield return new WaitForSecondsRealtime(lineDelay);

        line3.SetActive(true);
        yield return new WaitForSecondsRealtime(lineDelay);
        

        continuePrompt.SetActive(true);
        waitingForContinue = true;
    }
}