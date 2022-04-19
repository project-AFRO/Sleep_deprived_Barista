using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float timeSpent;
    public int costumersServed;
    public int costumersNotServed;
    public float wallet;
    [SerializeField] GameObject barista;

    // Start is called before the first frame update
    void Start()
    {
        wallet = barista.GetComponent<barista>().getWallet();
        costumersServed = 0;
        costumersNotServed = 0;
        timeSpent = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        wallet = barista.GetComponent<barista>().getWallet();
    }

}