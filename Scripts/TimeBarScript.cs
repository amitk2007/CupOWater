using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarScript : MonoBehaviour
{
    public float TTL;
    public Image lifeBar;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        //get TTL From Level Data
        TTL = TTL == 0 ? 10 : TTL;
        ///they does this work and the other one don't?
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.rectTransform.anchorMax = new Vector2((startTime + TTL - Time.time) / TTL, 1);
        lifeBar.rectTransform.localScale = new Vector3(1, 1, 1);

        if (startTime + TTL <= Time.time)
        {
            //print("Level Failed");
            LevelManager.LevelFinished(false);
        }
    }
}
