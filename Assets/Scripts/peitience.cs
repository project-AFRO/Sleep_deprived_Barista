using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peitience : MonoBehaviour
{
    [SerializeField] int costumerPeitience;
    [SerializeField] bool ispeitient;
    // Start is called before the first frame update
    void Start()
    {
        ispeitient = true;
        costumerPeitience = 15;
        StartCoroutine(count());
    }

    // Update is called once per frame
    void Update()
    {
        count();
    }
    IEnumerator count() {
        yield return new WaitForSeconds(Random.Range(costumerPeitience * 3 / 4, costumerPeitience * 5 / 4));
        ispeitient = false;
    }
    
    public bool becameImpatient()
    {
        return ispeitient;
    }
}
