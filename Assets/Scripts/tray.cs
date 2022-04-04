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
  
    [SerializeField] private int localYOffset;
    // Update is called once per frame
    void Update()
    {
        cup1.GetComponent<coffeecups>().setChosentype(barista.GetComponent<barista>().cupsInHand[0].getChosentype());
        cup2.GetComponent<coffeecups>().setChosentype(barista.GetComponent<barista>().cupsInHand[1].getChosentype());
        cup3.GetComponent<coffeecups>().setChosentype(barista.GetComponent<barista>().cupsInHand[2].getChosentype());
        cup4.GetComponent<coffeecups>().setChosentype(barista.GetComponent<barista>().cupsInHand[3].getChosentype());
    }
    
    
}
