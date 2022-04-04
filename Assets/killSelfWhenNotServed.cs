using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSelfWhenNotServed : MonoBehaviour
{
    float timer = 0;
    public int time_to_leave = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time_to_leave)
        {
            // do something here and then destroy self
            Destroy(gameObject);
        }
    }
}
