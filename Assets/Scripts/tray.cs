using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tray : MonoBehaviour
{
    [SerializeField] private GameObject barista;
    [SerializeField] private barista baristaScript;
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] GameObject cup1;
    [SerializeField] GameObject cup2;
    [SerializeField] GameObject cup3;
    [SerializeField] GameObject cup4;
    GameObject[] cups;
     GameObject[] cupOfCoffee;
    [SerializeField] private int localYOffset;

    private coffeecups[] coffeeOnTray;
    // Start is called before the first frame update
    void Start()
    {
        baristaScript = barista.GetComponent<barista>();
        cupOfCoffee = new GameObject[4];
        cups = new GameObject[4];
        cups[1] = cup2;
        cups[0] = cup1;
        cups[2] = cup3;
        cups[3] = cup4;
        
        coffeeOnTray = barista.GetComponent<barista>().GetCoffeecupsInhand();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        cup1.GetComponent<coffeecups>().setChosentype(coffeeOnTray[0].GetComponent<coffeecups>().getChosentype());
        cup2.GetComponent<coffeecups>().setChosentype(coffeeOnTray[1].GetComponent<coffeecups>().getChosentype());
        cup3.GetComponent<coffeecups>().setChosentype(coffeeOnTray[2].GetComponent<coffeecups>().getChosentype());
        cup4.GetComponent<coffeecups>().setChosentype(coffeeOnTray[3].GetComponent<coffeecups>().getChosentype());


    }
    
    
}
