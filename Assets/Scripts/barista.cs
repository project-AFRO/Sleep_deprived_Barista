using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] public Score playerscore;
    [SerializeField] private bool isPaused;
    [SerializeField] private costumer costumersGameObj;
    [SerializeField]killSelfWhenNotServed killSelfWhenNotServed;

    [SerializeField]private TextMeshProUGUI tips;

    [SerializeField] private int costumersServed;

    [SerializeField] public bool gameOver;

    [SerializeField] private GameObject doorSpawner;
    [SerializeField] private doSpawnCostumer spawnCostumerScript;
    [SerializeField] private WakemeterSliderControl wakeSliderUi;

    public AudioSource drop;
    public AudioSource drink;
    public AudioSource snore;
    public AudioClip drp;
    public AudioClip drk;
    public AudioClip snr;

    [SerializeField] GameObject pauseMenu;

    private dispensers dispenser;

    [SerializeField] private byte coffeeType;
    public coffeecups[] cupsInHand;
    
    private float xValue;
    private float zValue;

    private float timeTime;
    [SerializeField] private byte trayCapacity;
    string x;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        drop.clip = drp;
        drink.clip = drk;
        snore.clip = snr;

        gameOver = false;

        pauseMenu.GetComponent<Puasemenu>().Resume();

        rb = gameObject.GetComponent<Rigidbody>();

        wallet = 0;

        spawnCostumerScript = doorSpawner.GetComponent<doSpawnCostumer>();

        cupsDrank = 0;
        cupsDispensed = 0;

        trayCapacity = 4;
        cupsInHand = new coffeecups[trayCapacity];
        for(int i = 0; i < trayCapacity; i++)
        {
            cupsInHand[i] = new coffeecups();
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

        costumersServed = 0;

        moveSpeed = 1000f;

        playerscore = new Score();
        x = ("tips" + wallet.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {


        if (gameOver) 
            return;
        x = '$'+wallet.ToString();
        tips.text = x;

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
            cupsInHand[cupsDispensed].setChosentype(coffeeType);
            cupsDispensed++;
        }
        else if (Input.GetKeyDown(KeyCode.E) && canDispense)
        {
            wallet -= 5.0f;
            cupsDrank++;
            currentWakeLevel = maxWakeLevel;
            drink.Play();
            //currentWake = AddTillMaxWake();
           
        }
        if (Input.GetKeyDown(KeyCode.E) && cupsDispensed > 0)
        {
            wallet -= cupsInHand[--cupsDispensed].getPrice() * 2 / 4;
            currentWakeLevel = maxWakeLevel;
            cupsInHand[cupsDispensed].setChosentype(3);
            cupsDrank++;
            drink.Play();
        }

        if (!isSleeping && !isForcedSleeping)
            WakeMetter();

        if (Input.GetKeyDown(KeyCode.Z) && !isSleeping)
        {
            isPlayerChoosenSleeping = true;
            Sleep();
            snore.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isPlayerChoosenSleeping && isSleeping)
        {
            isPlayerChoosenSleeping = false;
            isSleeping = false;
            snore.Stop();
        }
        if (currentWakeLevel <= 0 && !isSleeping)
        {
            isForcedSleeping = true;
            Sleep();
            snore.Play();
        }
        if (isSleeping)
        {
            Sleep();
            
        }

        if (Random.Range(0 , maxWakeLevel) / currentWakeLevel >= 8 && cupsDispensed>0 && !isSleeping)
        {
            dropCups();
            drop.Play();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            pauseMenu.GetComponent<Puasemenu>().Pause();
            pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
            pauseMenu.GetComponent<Puasemenu>().Resume();
            pause();
        }

        

        playerscore.wallet = wallet;
        playerscore.timeSpent = timeTime;
        playerscore.costumersServed = costumersServed;
        playerscore.costumersNotServed = killSelfWhenNotServed.costumersNotServered;

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
            bool deletedCup = false;
            foreach(coffeecups coffee in cupsInHand) {
                if(collision.gameObject.GetComponent<costumer>().getCoffeeTypeRequested() == coffee.getChosentype() && !deletedCup ) 
                {
                    killSelfWhenNotServed = collision.gameObject.GetComponent<killSelfWhenNotServed>();
                    Destroy(collision.gameObject);
                    cupsDispensed--;
                    coffee.setprice(coffee.getChosentype());
                    wallet += coffee.getPrice();
                    coffee.setChosentype(3);
                    spawnCostumerScript.setNumOfCostumers(spawnCostumerScript.getNumOfCostumers() - 1);
                    sortHand();
                    deletedCup = true;
                    costumersServed++;
                    if (costumersServed % 10 == 0 && killSelfWhenNotServed.costumersNotServered > 0)
                    {
                        killSelfWhenNotServed.costumersNotServered--;
                    }
                    break;
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
            snore.Stop();
        }
        wakeSliderUi.sliderControl();

        //Debug.Log(currentWakeLevel);
        //Debug.Log("sleeping? " + isSleeping);
    }
    void MovePlayer()
    {
        //multiply by negative 1 to move in opposite direction
        if (!isSleeping)
        {
            xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed *( 1+(currentWakeLevel/maxWakeLevel));
            zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed * (1 + (currentWakeLevel / maxWakeLevel));
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
        if (cupsDrank > 0)
        {
            if ((int)(Time.time) % 15 == 0)
            {
                cupsDrank-=(int)(timeTime*Time.deltaTime);
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
            if ((int)(Time.time) % 15 == 0)
            {
                cupsDrank -= (int)(timeTime * Time.deltaTime*x);
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
        coffeecups temp = null;
        for (int i=0; i < trayCapacity-1; i++)
        {
            if (cupsInHand[i].getChosentype() == 3 && cupsInHand[i+1].getChosentype() != 3)
            {
                cupsInHand[i].setChosentype(cupsInHand[i + 1].getChosentype()); 
                cupsInHand[i + 1].setChosentype(3);
                temp = cupsInHand[i];
                Debug.Log("moved hand 1");
            }
        }
        if (temp != null)
        {
            sortHand();
        }
        else
        {
            Debug.Log("sorted");
        }
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
    public Score GetPlayerScore()
    {
        playerscore.wallet = wallet;
        playerscore.timeSpent = timeTime;
        playerscore.costumersServed = costumersServed;
        return playerscore;
    }
    public float getWallet()
    {
        return wallet;
    }

    public void pause()
    {
        if (isPaused) 
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
}


