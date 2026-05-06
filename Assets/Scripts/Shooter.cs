using UnityEngine;

public class Shooter : MonoBehaviour{

    public float moveSpeed = 5;
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
        // moving camera position via arrows, a, s, d, w
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(h, v, 0);

        //fire ball when left mouse button pressed
        if (Input.GetButtonUp("Fire1"))
        {
            //create bullet
            Rigidbody instance = Instantiate(bullet, transform.position, 
                transform.rotation) as Rigidbody;

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
