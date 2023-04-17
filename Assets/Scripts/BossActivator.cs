using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject theBossBattle;

    public GameObject theHealthBar;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            theBossBattle.SetActive(true);
            theHealthBar.SetActive(true);

            gameObject.SetActive(false);

            AudioManager.instance.PlayBossMusic();
        }
    }
}
