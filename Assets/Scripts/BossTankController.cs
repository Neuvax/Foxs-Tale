using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTankController : MonoBehaviour
{
    public enum bossStates { shooting, hurt, moving, ended };
    public bossStates currentStates;
    public Transform theBoss;
    public Transform theBar;
    public Animator anim;

    public GameObject bossActivator;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;

    public GameObject hitBox;

    [Header("Health")]
    public int health = 5;
    public Slider healthBar;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;

    private Vector3  initialPosition, initialScale;
    private int initialHealth;
    private float initialTimeBetweenShots, initialtimeBetweenMines;
    // Start is called before the first frame update
    void Start()
    {
        currentStates = bossStates.shooting;

        initialPosition = transform.position;
        initialHealth = health;
        initialScale = transform.localScale;

        initialTimeBetweenShots = timeBetweenShots;
        initialtimeBetweenMines = timeBetweenMines;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case bossStates.shooting:

            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;

                var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.transform.localScale = theBoss.localScale;

                AudioManager.instance.PlaySFX(2);
            }
                break;

            case bossStates.hurt:

                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentStates = bossStates.moving;

                        mineCounter = 0;

                        if(isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            theBar.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            AudioManager.instance.PlaySFX(3);

                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();
                            
                            currentStates = bossStates.ended;
                        }
                    }
                }
                break;

            case bossStates.moving:

                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;

                        moveRight = false;

                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);

                        moveRight = true;

                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if(mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }
        //Boss Health Bar
        healthBar.value = health;

        // Restart boss battle
        if(PlayerHealthController.instance.currentHealth <= 0)
        {

            AudioManager.instance.StopBossMusic();

            health = initialHealth;

            timeBetweenShots = initialTimeBetweenShots;
            timeBetweenMines = initialtimeBetweenMines;

            theBoss.transform.position = initialPosition;

            theBoss.transform.localScale = initialScale;

            moveRight = false;

            BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
            if(mines.Length > 0)
            {
                foreach(BossTankMine foundMine in mines)
                {
                    foundMine.Explode();
                }
            }

            gameObject.SetActive(false);
            theBar.gameObject.SetActive(false);
            bossActivator.gameObject.SetActive(true);
        }
    }

    public void TakeHit()
    {
        currentStates = bossStates.hurt;

        hurtCounter = hurtTime;

        AudioManager.instance.PlaySFX(0);

        anim.SetTrigger("Hit");

        //Delete mines when boss is damaged (Optional)
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if(mines.Length > 0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        health--;

        if(health <= 0)
        {
            isDefeated = true;
        } else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentStates = bossStates.shooting;

        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
