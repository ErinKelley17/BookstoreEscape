using UnityEngine;
using UnityEngine.UIElements;
public class UIController : MonoBehaviour
{
    private Label scoreLabel;
    private Label winLoseLabel;
    private ProgressBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDoc = GetComponent<UIDocument>();
        VisualElement root = uiDoc.rootVisualElement;

        //Query the label by its name
        scoreLabel = root.Q<Label>("score");
        healthBar = root.Q<ProgressBar>("health");

        if (scoreLabel != null)
        {
            string currentScoreText = scoreLabel.text;
            Debug.Log("Current score text:" + currentScoreText);

            UpdateScore(0);
        }
        else
        {
            Debug.LogError("Score Label not found in UXML");
        }

        if (healthBar != null)
        {
            float healthVal = healthBar.value;
            Debug.Log("Current health value:" + healthVal);

            UpdateHealth(100);
        }
        else
        {
            Debug.LogError("Health Bar not found in UXML");
        }

        winLoseLabel = root.Q<Label>("winLose");
        if (winLoseLabel != null)
        {
            UpdateWinLose("");
        }
        else
        {
            Debug.LogError("winLoseLabel not found in UXML");
        }
    }

    public void UpdateScore(int newScore)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + newScore.ToString();
        }
    }
    public void UpdateHealth(float newHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = newHealth;
        }
    }

    public void UpdateWinLose(string newText)
    {
        if (winLoseLabel != null)
        {
            winLoseLabel.text = newText;
        }
    }
}
