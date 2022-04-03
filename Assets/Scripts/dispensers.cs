using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispensers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private byte dispenserType;
    [SerializeField] private string dispenserName;
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (dispenserType == 0)
        {
            dispenserName = "latte coffee";
        }
        else if (dispenserType == 1)
        {
            dispenserName = "black coffee";
        }
        else if (dispenserType == 2)
        {
            dispenserName = "espresso coffee";
        }
    }

    public byte GetDispansertype()
    {
        return dispenserType;
    }
    public string GetDispenserName()
    {
        return dispenserName;
    }
}
