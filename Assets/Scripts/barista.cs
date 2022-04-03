using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barista : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxWakeLevel;
    [SerializeField] private bool isForcedSleeping;
    [SerializeField] private float wallet;
    [SerializeField] private int cupsDrank;
    [SerializeField] private int cupsDispensed;
    [SerializeField] private float currentWakeLevel;
    [SerializeField] private bool canDispense;
    [SerializeField] private bool isSleeping;
    [SerializeField] private bool isPlayerChoosenSleeping;
    [SerializeField] private int choosenSleepFactor;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        wallet = 0;

        cupsDrank = 0;
        cupsDispensed = 0;


        canDispense = false;

        maxWakeLevel = 1000;
        currentWakeLevel = 1000;
        isForcedSleeping = false;
        isPlayerChoosenSleeping = false;
        isSleeping = false;
        choosenSleepFactor = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isPlayerChoosenSleeping && isSleeping)
        {
            isPlayerChoosenSleeping = false;
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
        if (collision.gameObject.tag == "costumer" && cupsDispensed > 0)
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
    private void WakeMetter()
    {
        if (currentWakeLevel > 0)
            currentWakeLevel -= Time.deltaTime * cupsDrank * cupsDrank;
        //Debug.Log("wakemeter " + currentWake);

    }

    private void Sleep()
    {
        if (currentWakeLevel < maxWakeLevel && isForcedSleeping)
        {
            isSleeping = true;
            currentWakeLevel += Time.deltaTime / cupsDrank;
            cupsDrank -= (int)(Time.deltaTime);
        }
        else if (currentWakeLevel < maxWakeLevel && isPlayerChoosenSleeping)
        {
            isSleeping = true;
            currentWakeLevel += choosenSleepFactor * Time.deltaTime / cupsDrank;
            cupsDrank -= (int)(choosenSleepFactor * Time.deltaTime);
        }
        if (currentWakeLevel >= maxWakeLevel)
        {
            isSleeping = false;
            isForcedSleeping = false;
            isPlayerChoosenSleeping = false;
            cupsDrank = 0;
        }

        Debug.Log(currentWakeLevel);
        Debug.Log("sleeping? " + isSleeping);
    }
    void MovePlayer()
    {
        //multiply by negative 1 to move in opposite direction
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(zValue, 0f, -xValue);
    }
}
