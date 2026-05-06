using UnityEngine;

public class TeleportationTrigger : MonoBehaviour
{
    public GameObject teleportTarget;
    public AudioClip teleportSound;
    private Vector3 pos;
    private Quaternion rot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (teleportTarget != null)
        {
            pos = teleportTarget.transform.position;
            rot = teleportTarget.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Missing Teleportation Target");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (teleportTarget != null)
        {
            AudioSource audioPlayer = col.gameObject.GetComponent<AudioSource>();
            if (audioPlayer != null && teleportSound != null)
            {
                audioPlayer.PlayOneShot(teleportSound);
            }

            col.gameObject.transform.position = pos;
            col.gameObject.transform.rotation = rot;
        }

    }
}
