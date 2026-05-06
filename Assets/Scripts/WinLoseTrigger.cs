using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseTrigger : MonoBehaviour
{
    private UIController uiScript = null;
    private float timer;
    private bool resetReady = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0.0f;
        uiScript = FindObjectOfType<UIController>();
        if (uiScript == null)
        {
            Debug.LogError("Could not find UIController script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (resetReady && timer >= 4)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            resetReady = false;
        }
        else if (resetReady && timer >= 3)
        {
            uiScript.UpdateWinLose("Resetting in ...1");
        }
        else if (resetReady && timer >= 2)
        {
            uiScript.UpdateWinLose("Resetting in ...2");
        }
        else if (resetReady && timer >= 1)
        {
            uiScript.UpdateWinLose("Resetting in ...3");
        }


    }

    void OnTriggerEnter(Collider col)
    {
        //update UI Message w/Delay
        if (col.gameObject.tag == "Player")
        {
            uiScript.UpdateWinLose("You Win!");
            resetReady = true;
            timer = 0.0f;
        }


    }
}
