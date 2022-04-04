using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakemeterSliderControl : MonoBehaviour
{

    [SerializeField] private Slider wake;
    [SerializeField] private GameObject barista;
    [SerializeField] private barista baristaScript;
    [SerializeField] private Image canvasOverveiw;
    // Start is called before the first frame update
    void Start()
    {
        baristaScript = barista.GetComponent<barista>();
    }

    public void maxWakeSliderValue()
    {
        wake.maxValue = barista.GetComponent<barista>().getMaxWakeLevel();
        sliderControl();
        canvasOverveiw.color = new Color32(0, 0, 0, 0);
    }
    public void sliderControl()
    {
        wake.value = barista.GetComponent<barista>().getCurrentWakeLevel();
        canvasOverveiw.color = new Color32(0, 0, 0, (byte)(150-barista.GetComponent<barista>().getCurrentWakeLevel()/4));
    }
}
