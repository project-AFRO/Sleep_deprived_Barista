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

    private coffeecups[] coffeeOnTray;
    // Start is called before the first frame update
    void Start()
    {
        coffeeOnTray = new coffeecups[4];
        coffeeOnTray = barista.GetComponent<barista>().GetCoffeecupsInhand();

        Debug.Log(coffeeOnTray[0].getChosentype());
        Debug.Log(coffeeOnTray[1].getChosentype());
        Debug.Log(coffeeOnTray[2].getChosentype());
        Debug.Log(coffeeOnTray[3].getChosentype());

        cup1.GetComponent<coffeecups>().setChosentype(coffeeOnTray[0].getChosentype());
        cup2.GetComponent<coffeecups>().setChosentype(coffeeOnTray[1].getChosentype());
        cup3.GetComponent<coffeecups>().setChosentype(coffeeOnTray[2].getChosentype());
        cup4.GetComponent<coffeecups>().setChosentype(coffeeOnTray[3].getChosentype());
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
