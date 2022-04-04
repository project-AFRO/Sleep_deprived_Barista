using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawnCondition : MonoBehaviour
{
    [SerializeField] doSpawnCostumer roundScript;
    private bool isSleeping;
    private int count;
    private int NumStrikes;
    private int maxStrikes;
    private int roundCaught;
    private int currentRound;
    // Start is called before the first frame update
    void Start()
    {
        isSleeping = false;
        StartCoroutine(Counter());
        count = 0;
        NumStrikes = 0;
        maxStrikes = 5;
        roundCaught = 0;
        currentRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentRound = roundScript.getRound();
        Counter();
    }

    IEnumerator Counter()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        count++;
            Boss();
    }
    public void Boss()
    {
        if(isSleeping == true)
        {
            if(Random.Range(0 , count/30) == 1 && NumStrikes<maxStrikes && roundCaught != currentRound)
            {
                NumStrikes++;
                roundCaught = currentRound;

                Debug.Log("you got caught sleeping");
            }
            else if(NumStrikes >= maxStrikes)
            {
                Debug.Log("you lost");
            }
            
        }
    }
}