using UnityEngine;

public class PlayerShooter : MonoBehaviour{

    public Rigidbody bullet;
    public float power = 1500;
    public AudioClip throwSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fire ball when left mouse button pressed
        if (Input.GetButtonUp("Fire1"))
        {
            //create bullet
            Rigidbody instance = Instantiate(bullet, transform.position, 
                transform.rotation) as Rigidbody;

            Physics.IgnoreCollision(instance.gameObject.GetComponent<Collider>(),
                transform.root.gameObject.GetComponent<Collider>());

            //fire bullet in camera direction
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            instance.AddForce(fwd * power);

            //play throw sound
            AudioSource audio = instance.GetComponent<AudioSource>();
            audio.PlayOneShot(throwSound);

            //destroy bullet after 1s
            Destroy(instance.gameObject, 1.0f);
        }
    }
}
