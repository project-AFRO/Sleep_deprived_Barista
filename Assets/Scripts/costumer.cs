using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class costumer : MonoBehaviour
{

    [SerializeField] private byte coffeeTypeRequested;
    [SerializeField] private GameObject coffeecup;
    // Start is called before the first frame update
    void Start()
    {
        coffeeTypeRequested = (byte)Random.Range(0,3);
       
    }

    // Update is called once per frame
    void Update()
    {
        coffeecup.GetComponent<coffeecups>().setChosentype(coffeeTypeRequested);
    }
    public byte getCoffeeTypeRequested()
    {
        return coffeeTypeRequested;
    }
}
