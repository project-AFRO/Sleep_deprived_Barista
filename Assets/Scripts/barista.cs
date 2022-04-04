using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barista : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxWakeLevel;
    [SerializeField] private bool isForcedSleeping;
    [SerializeField] private float wallet;
    [SerializeField] private int cupsDrank;
    [SerializeField] private int cupsDispensed;
    [SerializeField] private float currentWakeLevel;
    [SerializeField] private bool canDispense;
    [SerializeField] public bool isSleeping;
    [SerializeField] private bool isPlayerChoosenSleeping;
    [SerializeField] private int choosenSleepFactor;

    [SerializeField] private GameObject doorSpawner;
    [SerializeField] private doSpawnCostumer spawnCostumerScript;
    [SerializeField] private WakemeterSliderControl wakeSliderUi;

    private dispensers dispenser;

    [SerializeField] private byte coffeeType;
    public coffeecups[] cupsInHand;
    
    private float xValue;
    private float zValue;

    private float timeTime;
    [SerializeField] private byte trayCapacity;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        wallet = 0;

        spawnCostumerScript = doorSpawner.GetComponent<doSpawnCostumer>();

        cupsDrank = 0;
        cupsDispensed = 0;

        trayCapacity = 4;
        cupsInHand = new coffeecups[trayCapacity];
        for(int i = 0; i < trayCapacity; i++)
        {
            cupsInHand[i] = new coffeecups(3);
            cupsInHand[i].setChosentype(3);
        }

        canDispense = false;

        maxWakeLevel = 400;
        currentWakeLevel = maxWakeLevel;
        wakeSliderUi.maxWakeSliderValue();

        isForcedSleeping = false;
        isPlayerChoosenSleeping = false;
        isSleeping = false;

        choosenSleepFactor = 3;

        moveSpeed = 2000f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach(coffeecups cup in cupsInHand)
            {
                Debug.Log(cup.getChosentype());
            }
        }
        wakeSliderUi.sliderControl();
        timeTime = Time.time;
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.F) && canDispense && cupsDispensed < trayCapacity)
        {
            addCuptoHand();
            cupsDispensed++;
           
        }
        else if (Input.GetKeyDown(KeyCode.E) && canDispense)
        {
            wallet -= 5.0f;
            cupsDrank++;
            currentWakeLevel = maxWakeLevel;
            //currentWake = AddTillMaxWake();
           
        }
        if (Input.GetKeyDown(KeyCode.E) && cupsDispensed > 0)
        {
            currentWakeLevel = maxWakeLevel;
            Destroy(cupsInHand[--cupsDispensed]);
            cupsDrank++;
            wallet -= 5;
           
        }

        if (!isSleeping && !isForcedSleeping)
            WakeMetter();

        if (Input.GetKeyDown(KeyCode.Z) && !isSleeping)
        {
            isPlayerChoosenSleeping = true;
            Sleep();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isPlayerChoosenSleeping && isSleeping)
        {
            isPlayerChoosenSleeping = false;
            isSleeping = false;
        }
        if (currentWakeLevel <= 0)
        {
            isForcedSleeping = true;
            Sleep();
        }
        if (isSleeping)
        {
            Sleep();
        }

        if (Random.Range(0 , maxWakeLevel) / currentWakeLevel >= 2)
        {
            dropCups();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dispenser")
        {
            canDispense = true;
            dispenser = other.GetComponent<dispensers>();
            coffeeType = dispenser.GetDispansertype();
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "dispenser")
        {
            canDispense = false;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "costumer" && cupsDispensed > 0)
        {
            foreach(coffeecups coffee in cupsInHand) {
                if(collision.gameObject.GetComponent<costumer>().getCoffeeTypeRequested() == coffee.getChosentype()) 
                { 
                    cupsDispensed--;
                    Destroy(collision.gameObject);
                    wallet += coffee.getPrice();
                    coffee.setChosentype(3);
                    spawnCostumerScript.setNumOfCostumers(spawnCostumerScript.getNumOfCostumers() - 1);
                    sortHand();
                }
                
                
                
                
            }
        }
    }

    /*private float AddTillMaxWake()
    {
        float i = currentWake;
        while (i < maxWake) i += 5*Time.deltaTime;
        return i;
    }*/
    private void WakeMetter()
    {
        if (currentWakeLevel > 0)
            currentWakeLevel -= Time.deltaTime*2 * (cupsDrank * cupsDrank + 1);
        if(currentWakeLevel < 0)
        {
            isForcedSleeping = true;
        }
        //Debug.Log("wakemeter: " + currentWakeLevel);

    }

    private void Sleep()
    {
        if (currentWakeLevel < maxWakeLevel && isForcedSleeping)
        {
            isSleeping = true;
            currentWakeLevel += Time.deltaTime * 3 / (cupsDrank+1);
            cupsDrankReduction();
        }
        else if (currentWakeLevel < maxWakeLevel && isPlayerChoosenSleeping)
        {
            isSleeping = true;
            currentWakeLevel += choosenSleepFactor*3 * Time.deltaTime / (cupsDrank+1);
            cupsDrankReduction(choosenSleepFactor);
        }
        if(currentWakeLevel > 200 && isForcedSleeping)
        {
            isForcedSleeping = false;
            isPlayerChoosenSleeping = true;
        }
        else if (currentWakeLevel >= maxWakeLevel)
        {
            isSleeping = false;
            isForcedSleeping = false;
            isPlayerChoosenSleeping = false;
        }

        //Debug.Log(currentWakeLevel);
        //Debug.Log("sleeping? " + isSleeping);
    }
    void MovePlayer()
    {
        //multiply by negative 1 to move in opposite direction
        if (!isSleeping)
        {
            xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        }
        else
        {
            xValue = 0;
            zValue = 0;
        }
        rb.velocity = new Vector3(zValue, 0f, -xValue);
    }

    
    private void cupsDrankReduction()
    {
        if (cupsDrank > 0   )
        {
            if ((Time.time) % 15 == 0)
            {
                cupsDrank--;
                timeTime++;
            }
        }
        else
        {
            cupsDrank = 0;
           
        }
    }

    private void cupsDrankReduction(float x)
    {
        
        if (cupsDrank > 0)
        {
            if ((Time.time) % 15 == 0)
            {
                cupsDrank -= (int)x;
                timeTime++;
            }
        }
            
        else
        {
            cupsDrank = 0;
        }
    }

    public void dropCups()
    {
        wallet -= cupsDispensed*5;
        cupsDispensed = 0;
        foreach(coffeecups cup in cupsInHand)
        {
            cup.setChosentype(3);
        }
    }

    private void sortHand()
    {
        coffeecups previousCup = cupsInHand[0];
        coffeecups temp = null;
        for (int i=1; i < trayCapacity; i++)
        {
            if (cupsInHand[i].getChosentype() < previousCup.getChosentype())
            {
                Debug.Log("moved");
                temp = previousCup;
                cupsInHand[i - 1] = cupsInHand[i];
                cupsInHand[i] = temp;
                previousCup = cupsInHand[i];
            }
        }
        if (temp != null)
        {
            sortHand();
        }
    }

    public void addCuptoHand()
    {
       cupsInHand[cupsDispensed].setChosentype(coffeeType);
        Debug.Log(cupsInHand[cupsDispensed].getChosentype());
        Debug.Log(cupsDispensed);
    }

    public int getCupsDrank() { return cupsDrank; }
    public float getCurrentWakeLevel() { return currentWakeLevel; }

    public float getMaxWakeLevel() { return maxWakeLevel; }

    public byte GetCoffeecupsInhand(int x)
    {
        if (x == 0) { return cupsInHand[0].getChosentype(); }
        else if (x == 1) { return cupsInHand[1].getChosentype(); }
        else if (x == 2) { return cupsInHand[2].getChosentype(); }
        else if (x == 3) { return cupsInHand[3].getChosentype(); }
        else return 3;
    }
    public bool getSleepState()
    {
        return isSleeping;
    }
}


