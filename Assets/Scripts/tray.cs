using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tray : MonoBehaviour
{
    [SerializeField] private GameObject barista;
    public barista baristaScript;
    [SerializeField] GameObject cup1;
    [SerializeField] GameObject cup2;
    [SerializeField] GameObject cup3;
    [SerializeField] GameObject cup4;
  
    [SerializeField] private int localYOffset;

    private coffeecups coffee1;
    private coffeecups coffee2;
    private coffeecups coffee3;
    private coffeecups coffee4;
    // Start is called before the first frame update
    void Start()
    {

        coffee1.setChosentype(baristaScript.GetCoffeecupsInhand(0));
        coffee2.setChosentype(baristaScript.GetCoffeecupsInhand(1));
        coffee3.setChosentype(baristaScript.GetCoffeecupsInhand(2));
        coffee4.setChosentype(baristaScript.GetCoffeecupsInhand(3));

        cup1.GetComponent<coffeecups>().setChosentype(3);
        cup2.GetComponent<coffeecups>().setChosentype(3);
        cup3.GetComponent<coffeecups>().setChosentype(3);
        cup4.GetComponent<coffeecups>().setChosentype(3);
    }

    // Update is called once per frame
    void Update()
    {
        coffee1.setChosentype(baristaScript.GetCoffeecupsInhand(0));
        coffee2.setChosentype(baristaScript.GetCoffeecupsInhand(1));
        coffee3.setChosentype(baristaScript.GetCoffeecupsInhand(2));
        coffee4.setChosentype(baristaScript.GetCoffeecupsInhand(3));
    }
    
    
}
