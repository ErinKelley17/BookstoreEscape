using UnityEngine;

public class PlayerSpeedBoost : MonoBehaviour
{
    private float timer;
    private bool boosted = false;
    private float boostDuration;
    private AudioSource audioPlayer = null;
    private FirstPersonController script = null;
    private string objectTag = "CollisionObject";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        timer = 0.0f;

        //get player's AudioSource to the play object collection sound
        audioPlayer = this.GetComponent<AudioSource>();

        //get player controller object and its FirstPersonController script
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            script = playerObject.GetComponentInChildren<FirstPersonController>();
        } else
        {
            Debug.LogWarning("Must have a player controller with Tag of \"Player\"");
        }
        if (script == null || playerObject == null)
        {
            Debug.LogWarning("Must use the \"FirstPersonController\" prefab from" + 
                "the \"Modular First Person Controller\" package");
        }
        
        //get AudioSource from SpeedBoost Object
        audioPlayer = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //if speedBoost is on reset to original speed once boost duration ellapsed
        if (script != null && boosted && (timer >= boostDuration))
        {
            boosted = false;
            script.speedBoost(boosted);        //stop current speed boost
            script.restoreSprintKey();      //re-link sprinting to 'Shift' key
            script.speedBoost(true);        //restore ability for regular sprint
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        if(col.gameObject.tag == objectTag)
        {
            boostSpeed(col);
        }
    }

    private void boostSpeed(Collider col)
    {
        if (!script)
        {
            return;
        }

        timer = 0.0f;
        AudioClip playSound = null;

        // get object's speedboost info
        ObjectSpeedBoost objSpeedScript = col.gameObject.GetComponent<ObjectSpeedBoost>();
        
        if (objSpeedScript != null)
        {
            Debug.Log("Boosting speed...");
            playSound = objSpeedScript.speedBoostSound;
            float speedMultiplier = objSpeedScript.speedMultiplier;
            boostDuration = objSpeedScript.speedBoostDuration;

            boosted = true;
            script.initSpeedBoostOnly();
            script.speedBoost(boosted, speedMultiplier, this.boostDuration);

            if (objSpeedScript.destroyOnCollision)
            {
                Destroy(col.gameObject);
            }
        }

        //play object collection sounde
        if(playSound != null)
        {
            if (audioPlayer != null)
            {
                audioPlayer.PlayOneShot(playSound);
            } else
            {
                Debug.Log("Need to add an AudioSource component to the player controller.");
            }
        }
    }
}
