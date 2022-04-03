using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slep : MonoBehaviour
{
    [SerializeField] barista baristaScript;
    private bool isSleeping;
    private int count;
    private int NumStrikes;
    private int maxStrikes;
    // Start is called before the first frame update
    void Start()
    {
        isSleeping = false;
        StartCoroutine(Counter());
        count = 0;
        NumStrikes = 0;
        maxStrikes = 5;
    }

    // Update is called once per frame
    void Update()
    {
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
            if(Random.RandomRange(0 , count/30) == 1 && NumStrikes<maxStrikes)
            {
                NumStrikes++;
                Debug.Log("you got caught sleeping");
            }else if(NumStrikes >= maxStrikes)
            {
                Debug.Log("you lost");
            }
        }
    }
}
