using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakemeterSliderControl : MonoBehaviour
{

    [SerializeField] private Slider wake;
    [SerializeField] private GameObject barista;
    [SerializeField] private barista baristaScript; 
    // Start is called before the first frame update
    void Start()
    {
        baristaScript = barista.GetComponent<barista>();
        
    }

    public void maxWakeSliderValue()
    {
        wake.maxValue = barista.GetComponent<barista>().getMaxWakeLevel();
        sliderControl();
    }
    public void sliderControl()
    {
        wake.value = barista.GetComponent<barista>().getCurrentWakeLevel();
    }
}
