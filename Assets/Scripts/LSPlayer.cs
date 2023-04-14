using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    public bool levelLoading;

    public LSManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)
        {
            if(Input.GetAxisRaw("Horizontal") > .5f)
            {
                if(currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }

            if(Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if(currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            if(Input.GetAxisRaw("Vertical") > .5f)
            {
                if(currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }

            if(Input.GetAxisRaw("Vertical") < -.5f)
            {
                if(currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIManager.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    theManager.LoadLevel();

                    AudioManager.instance.StopSFXLS();
                    AudioManager.instance.PlaySFX(11);
                    
                }
            } else if(currentPoint.isLevel && currentPoint.levelToLoad != "" && currentPoint.isLocked)
            {
                LSUIManager.instance.ShowInfoLock();
            }
        }

        
    }

    public void SetNextPoint(MapPoint nextPoint) 
    {
        currentPoint = nextPoint;

        LSUIManager.instance.HideInfo();
    }
}
