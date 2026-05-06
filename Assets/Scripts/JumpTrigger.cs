using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public float jumpMultiplier = 3f;
    public AudioClip jumpBoostSound;
    private float origPower;
    private FirstPersonController script = null;
    private bool boosted = false;
    private AudioSource audioPlayer = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");   
        
        //get player controller object and its FirstPersonController script
        if (playerObject != null)
        {
            script = playerObject.GetComponentInChildren<FirstPersonController>();
        } else
        {
            Debug.LogWarning("Must have a player controller with Tag of \"Player\"");
        }
        if (script != null && playerObject != null)
        {
            origPower = script.jumpPower;
        } else
        {
            Debug.LogWarning("Must use the \"FirstPersonController\" prefab from" + 
                "the \"Modular First Person Controller\" package");
        }
        
        //get AudioSource from JumpTrigger area
        audioPlayer = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(script.jumpKey) && boosted)
        {
            if(audioPlayer != null && jumpBoostSound != null)
            {
                audioPlayer.PlayOneShot(jumpBoostSound);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(script != null)
            {
                boosted = true;
                script.jumpPower = origPower * jumpMultiplier;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(script != null)
            {
                boosted = false;
                script.jumpPower = origPower;
            }
        }
    }
}
