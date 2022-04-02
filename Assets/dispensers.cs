using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispensers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private byte dispenserType;
    [SerializeField] private string[] dispenserName;
    void Start()
    {
        dispenserName[3];
        dispenserName = new string["latte", "espresso", "black"];

    }

    // Update is called once per frame
    void Update()
    {
     if (dispenserType == 0)
        {

        }
        
        
           
    }

    public byte GetDispansertype()
    {
        return dispenserType;
    }
}
