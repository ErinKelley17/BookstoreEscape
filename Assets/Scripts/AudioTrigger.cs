using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioFile;
    public bool playOnlyOnce = true;
    public float multiPlayDelay = 1;
    private AudioSource audioPlayer;
    private bool hasPlayed = false;
    private double timer = 0.0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = multiPlayDelay;
        audioPlayer = this.GetComponent<AudioSource>();
        if (audioPlayer == null && audioFile != null){
            Debug.LogWarning("Your " + this.gameObject.name  
                + "object must have an AudioSourceComponent to play Audio File");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (audioFile != null && col.gameObject.tag == "Player"
            && ((playOnlyOnce && !hasPlayed) || !playOnlyOnce)
            && (timer >= multiPlayDelay))
        {
            if (audioPlayer != null)
            {
                audioPlayer.PlayOneShot(audioFile);
            }
            hasPlayed = false;
            timer = 0.0;
        }
    }
}
