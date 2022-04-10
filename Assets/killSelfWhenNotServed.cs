using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killSelfWhenNotServed : MonoBehaviour
{
    public AudioSource angry1;
    public AudioSource angry2;
    public AudioSource angryhigh;
    [SerializeField] AudioClip angy1;
    [SerializeField] AudioClip angy2;
    [SerializeField] AudioClip angyHi;
    float timer = 0;
    public int time_to_leave;
    public doSpawnCostumer costumerScript;
    public GameObject door;
    public int costumersNotServered;
    [SerializeReference] int rnd;
    // Start is called before the first frame update
    void Start()
    {
        rnd = (int)Random.Range(1, 4);
        angry2.clip = angy2;
        angry1.clip = angy1;
        angryhigh.clip = angyHi;
        costumerScript = door.GetComponent<doSpawnCostumer>();
        costumersNotServered = 0;
        time_to_leave = Random.Range(30, 70);
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time_to_leave)
        {

            
            if (rnd ==1) { angry1.Play(); } else if (rnd == 2) { angry2.Play(); } else { angryhigh.Play(); }
            costumersNotServered++;
            // do something here and then destroy self
            Destroy(gameObject);
            costumerScript.setNumOfCostumers(costumerScript.getNumOfCostumers()-1);
        }
    }
}
