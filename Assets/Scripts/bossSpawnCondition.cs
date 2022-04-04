using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawnCondition : MonoBehaviour
{
    [SerializeField] doSpawnCostumer roundScript;
    [SerializeField] GameObject barista;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject bar;
    [SerializeField] GameObject door;
    [SerializeField] private bool isSleeping;
    [SerializeField] private int count;
    [SerializeField] private int NumStrikes;
    [SerializeField] private int maxStrikes;
    [SerializeField] private int roundCaught;
    [SerializeField] private int currentRound;
    private Vector3 bossFinalLocation;
    [SerializeField] bool isBossIntiated;
    [SerializeField] bool isLeaving;
    [SerializeField] bool isGamelost;
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        isSleeping = false;
        isBossIntiated = false;
        isGamelost = false;

        StartCoroutine(Counter());

        count = 0;
        NumStrikes = 0;
        maxStrikes = 5;
        roundCaught = 0;
        currentRound = 0;
        bossFinalLocation = bar.transform.position + new Vector3(4, 0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
        isSleeping = barista.GetComponent<barista>().isSleeping;
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
            if( NumStrikes<maxStrikes && roundCaught != currentRound)
            {
                if (!isBossIntiated)
                {
                    boss = Instantiate(bossPrefab, transform.position + new Vector3(1, 0, 1), transform.rotation); 
                    isBossIntiated = true;
                }
                else
                {
                    if (!isLeaving)
                    {
                        boss.transform.position = Vector3.MoveTowards(transform.position, bossFinalLocation, NumStrikes+1);
                        if (boss.transform.position == bossFinalLocation && isSleeping)
                        {
                            Debug.Log("you got caught sleeping");
                            isLeaving = true;
                            NumStrikes++;
                            roundCaught = currentRound;
                            count = 0;
                        }
                    }
                    else if (isLeaving)
                    {
                        boss.transform.position = Vector3.MoveTowards(transform.position, door.transform.position, NumStrikes+1);
                        if (boss.transform.position == door.transform.position)
                        {
                            isBossIntiated = false;
                            isLeaving = false;
                        }
                    }
                }

            }
            else if(NumStrikes >= maxStrikes)
            {
                Debug.Log("you lost");
            }
            
        }
    }
}
