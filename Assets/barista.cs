using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barista : MonoBehaviour
{
    [SerializeField] private int baseSpeedZ;
    [SerializeField] private int baseSpeedX;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxWake;
    private float forwardmovement;
    private float sideMovement;
    [SerializeField] private bool isForcedSleeping;
    [SerializeField] private float wallet;
    [SerializeField] private int cupsDrank;
    [SerializeField] private int cupsDispensed;
    [SerializeField] private int speedFactor;
    [SerializeField] private float currentWake;
    [SerializeField] private bool canDispense;
    [SerializeField] private bool isSleeping;
    [SerializeField] private bool isPlayerChoosenSleeping;
    [SerializeField] private int choosenSleepFactor;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        baseSpeedX = 100;
        baseSpeedZ = 100;
        speedFactor = 10;

        wallet = 0;

        cupsDrank = 0;
        cupsDispensed = 0;

        
        
        forwardmovement = 0;
        sideMovement = 0;
        
        canDispense = false;

        maxWake = 1000;
        currentWake = 1000;
        isForcedSleeping = false;
        isPlayerChoosenSleeping = false;
        isSleeping = false;
        choosenSleepFactor = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !isForcedSleeping)
        {
            forwardmovement = Input.GetAxisRaw("Vertical") * speedFactor * baseSpeedX * Time.deltaTime;
        }
        else
        {
            forwardmovement = 0;
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && !isForcedSleeping)
        {
            sideMovement = -Input.GetAxisRaw("Horizontal") * speedFactor * baseSpeedZ * Time.deltaTime;
        }
        else
        {
            sideMovement = 0;
        }
        rb.velocity = new Vector3(forwardmovement, 0, sideMovement);
        
        if (Input.GetKeyDown(KeyCode.F) && canDispense && cupsDispensed < 4)
        {

            cupsDispensed++;
            Debug.Log(cupsDispensed);
        }
        else if (Input.GetKeyDown(KeyCode.E) && canDispense)
        {
            wallet -= 5.0f;
            cupsDrank++;
            //currentWake = AddTillMaxWake();
            Debug.Log(cupsDrank);
            Debug.Log(cupsDispensed);
        }
        if (Input.GetKeyDown(KeyCode.E) && cupsDispensed > 0)
        {
            cupsDispensed--;
            cupsDrank++;
            wallet -= 5;
            Debug.Log(cupsDrank);
            Debug.Log(wallet);
        }
        
        if (!isSleeping)
        WakeMetter();
       
        if (Input.GetKeyDown(KeyCode.Z) && !isSleeping)
        {
            isPlayerChoosenSleeping = true;
            Sleep();
        }else if(Input.GetKeyDown(KeyCode.Z) && isPlayerChoosenSleeping &&isSleeping)
        {
            isPlayerChoosenSleeping = false;
        }
        if (currentWake <= 0)
        {
            isForcedSleeping = true;
            Sleep();
        }
        if (isSleeping)
        {
            Sleep();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dispenser")
        {
            canDispense = true;

            Debug.Log("dispenser");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "dispenser")
        {
            canDispense = false;
            Debug.Log("noDispenser");
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "costumer" && cupsDispensed > 0)
        {
            cupsDispensed--;
            wallet += 10;
            Destroy(collision.gameObject);
            Debug.Log(cupsDispensed);
        }
    }

    /*private float AddTillMaxWake()
    {
        float i = currentWake;
        while (i < maxWake) i += 5*Time.deltaTime;
        return i;
    }*/
    private void WakeMetter() {
        if(currentWake > 0)
        currentWake -= Time.deltaTime * cupsDrank * cupsDrank;
       //Debug.Log("wakemeter " + currentWake);
        
    }

    private void Sleep()
    {
        if (currentWake < maxWake && isForcedSleeping)
        {
            isSleeping = true;
            currentWake += Time.deltaTime / cupsDrank;
            cupsDrank -= (int)(Time.deltaTime);
        }else if (currentWake < maxWake && isPlayerChoosenSleeping)
        {
            isSleeping = true;
            currentWake += choosenSleepFactor*Time.deltaTime / cupsDrank;
            cupsDrank -= (int)(choosenSleepFactor*Time.deltaTime);
        }
        if (currentWake >= maxWake)
        {
            isSleeping = false;
            isForcedSleeping = false;
            isPlayerChoosenSleeping = false;
            cupsDrank = 0;
        }

        Debug.Log(currentWake);
        Debug.Log("sleeping? "+isSleeping);
    }
}
