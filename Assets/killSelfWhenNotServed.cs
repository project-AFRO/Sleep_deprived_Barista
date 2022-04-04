using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSelfWhenNotServed : MonoBehaviour
{
    float timer = 0;
    public int time_to_leave = 30;
    public doSpawnCostumer costumerScript;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        costumerScript = door.GetComponent<doSpawnCostumer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time_to_leave)
        {
            // do something here and then destroy self
            Destroy(gameObject);
            costumerScript.setNumOfCostumers(costumerScript.getNumOfCostumers()-1);
        }
    }
}
