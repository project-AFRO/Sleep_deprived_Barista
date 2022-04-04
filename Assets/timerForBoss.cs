using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerForBoss : MonoBehaviour
{
    float timer = 0;
    public GameObject barista;
    public int max_strikes = 3;
    public int strikes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (barista.GetComponent<barista>().isSleeping)
        {
            timer += Time.deltaTime;
        }
        
        Debug.Log(timer.ToString());
        
        if (timer > 10)
        {
            strikes++;
            timer = 0;
        }

        if (strikes >= max_strikes)
        {
            barista.GetComponent<barista>().gameOver = true;
        }
    }
}
