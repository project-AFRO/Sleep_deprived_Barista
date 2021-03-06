using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeecups : MonoBehaviour
{
    private string[] coffeeTypes;
    [SerializeField]private byte chosenType;
    private string coffeeTypeName;
    [SerializeField] private Material latte;
    [SerializeField] private Material black;
    [SerializeField] private Material esspresso;
    [SerializeField] private Material invis;
    [SerializeField] private MeshRenderer cupMesh;
    private byte price;
    
    void Start()
    {
        chosenType = 3;
        coffeeTypes = new string[4];
        coffeeTypes[0] = "latte";
        coffeeTypes[1] = "black";
        coffeeTypes[2] = "espresso";
        coffeeTypes[3] = "";
    }

    // Update is called once per frame
    void Update()
    {
        coffeeTypeName = coffeeTypes[chosenType];
        if (chosenType == 0) 
        { 
            cupMesh.material = latte;
            price = 10;
            
        }
        else if (chosenType == 1) 
        {
            cupMesh.material = black; 
            price = 15; 
        }
        else if (chosenType == 2) 
        { 
            cupMesh.material = esspresso;
            price = 5;
        }
        else
        {
            price = 0;
            cupMesh.material = invis;
        }
        
    }

    public void setChosentype(byte x)
    {
        chosenType = x;
        setprice(x);
    }
    public byte getChosentype()
    {
        return chosenType;
    }
    public byte getPrice()
    {
        return price;
    }
    public void setprice(int x)
    {
        if (chosenType == 0)
        {
            
            price = 10;

        }
        else if (chosenType == 1)
        {
            
            price = 15;
        }
        else if (chosenType == 2)
        {
            
            price = 5;
        }
        else
        {
            price = 0;
            
        }
    }
}
