using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Explode();

            AudioManager.instance.PlaySFX(3);

            PlayerHealthController.instance.DealDamage();
        }
    }

    public void Explode()
    {
        Destroy(gameObject);

        Instantiate(explosion, transform.position, transform.rotation);
    }
}
