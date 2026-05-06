using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    private UIController uiScript = null;
    public void Awake()
    {
        uiScript = FindObjectOfType<UIController>();
        if (uiScript == null)
        {
            Debug.LogError("Could not find UIController script");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);
        if (uiScript != null)
        {
            uiScript.UpdateHealth(health);
        }
        if (health <= 0)
        {
            Debug.Log("Player Dead");
        }
    }
}
