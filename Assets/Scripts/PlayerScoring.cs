using UnityEngine;

public class PlayerScoring : MonoBehaviour
{
    private int score;
    private string targetTag = "CollisionObject";
    private AudioSource audioPlayer = null;
    private UIController uiScript = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        audioPlayer = this.GetComponent<AudioSource>();

        uiScript = FindObjectOfType<UIController>();
        if (uiScript == null)
        {
            Debug.LogError("Could not find UIController script");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        if (col.gameObject.tag == targetTag)
        {
            collectObject(col);
        }
    }

    void collectObject(Collider col)
    {
        AudioClip playSound = null;

        //add object's value to the score
        ObjectScoring objectScript = col.gameObject.GetComponent<ObjectScoring>();
        if (objectScript != null)
        {
            score += objectScript.value;
            playSound = objectScript.collectSound;

            Debug.Log("Score: " + score);

            if (uiScript != null)
            {
                uiScript.UpdateScore(score);
            }

            if (objectScript.destroyOnCollision)
            {
                Destroy(col.gameObject);
            }
        }

        if (playSound != null)
        {
            if (audioPlayer != null)
            {
                audioPlayer.PlayOneShot(playSound);
            }
            else
            {
                Debug.LogWarning("Need to add AudioSource Component to the player controller");
            }
        }
    }
}
