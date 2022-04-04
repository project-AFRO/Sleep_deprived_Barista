using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tray : MonoBehaviour
{
    [SerializeField] private GameObject barista;
    [SerializeField] GameObject cup1;
    [SerializeField] GameObject cup2;
    [SerializeField] GameObject cup3;
    [SerializeField] GameObject cup4;
    GameObject[] cups;
    int i;
  
    [SerializeField] private int localYOffset;

    private void Start()
    {
        i = 0;
        cups = new GameObject[4];
        cups[0] = cup1;
        cups[1] = cup2;
        cups[2] = cup3;
        cups[3] = cup4;
    }

    void Update()
    {
        foreach (coffeecups cup in barista.GetComponent<barista>().cupsInHand) 
        {
            //if (cup != null) 
            {
                cups[i].GetComponent<coffeecups>().setChosentype(cup.getChosentype());
                i++;
            } 
        }
        i = 0;
    }    
}
