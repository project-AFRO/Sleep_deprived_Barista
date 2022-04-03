using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tray : MonoBehaviour
{
    [SerializeField] private GameObject barista;
    [SerializeField] private barista baristaScript;
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] GameObject loc1;
    [SerializeField] GameObject loc2;
    [SerializeField] GameObject loc3;
    [SerializeField] GameObject loc4;
    GameObject[] loc;
     GameObject[] cupOfCoffee;
    [SerializeField] private int localYOffset;

    private coffeecups[] coffeeOnTray;
    // Start is called before the first frame update
    void Start()
    {
        baristaScript = barista.GetComponent<barista>();
        cupOfCoffee = new GameObject[4];
        loc = new GameObject[4];
        loc[1] = loc2;
        loc[0] = loc1;
        loc[2] = loc3;
        loc[3] = loc4;
        displayCupsOnTray();
        coffeeOnTray = baristaScript.GetCoffeecupsInhand();
    }

    // Update is called once per frame
    void Update()
    {
        transformCupsLocation();
    }

    public void transformCupsLocation()
    {
        for(int i =0; i < 4; i++)
        {
            cupOfCoffee[i].GetComponent<coffeecups>().transform.position = loc[i].transform.position;
        }
        
    }
    public void displayCupsOnTray()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("cupShwon");
            cupOfCoffee[i] = Instantiate(cupPrefab, loc[i].transform.position,Quaternion.EulerRotation(0,0,0)) ;
            cupOfCoffee[i].GetComponent<coffeecups>().setChosentype(coffeeOnTray[i].getChosentype());
        }
    }
    public void destroyCupsOnTray()
    {
        foreach (GameObject cup in cupOfCoffee)
        {
            Destroy(cup);
        }
    }
    
}
