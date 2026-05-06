using UnityEngine;

public class PlayerHello : MonoBehaviour
{
    public bool loop = true;
    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        if (loop)
        {
            Debug.Log("Countdown to start of game");

            for (int i = 10; i > 0; i--)
            {
                Debug.Log("    " + i);
            }
        }
        else
        {
            Debug.Log("Hello Player");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        count += 1;
        Debug.Log("Frame: " + count);
    }
}
