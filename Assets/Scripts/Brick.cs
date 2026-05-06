using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool disappearOnCollision = false;
    private string objectTag = "Bullet";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider col = collision.collider;
        if(col.gameObject.tag == objectTag && disappearOnCollision)
        {
            Destroy(this.gameObject);
        }
    }
}
