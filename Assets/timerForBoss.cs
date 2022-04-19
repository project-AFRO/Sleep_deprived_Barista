using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class timerForBoss : MonoBehaviour
{
    float timer = 0;
    public GameObject barista;
    public int max_strikes = 3;
    public int strikes = 0;
    [SerializeField] private TextMeshProUGUI strikeUI;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        //strikeUI.text = "Strikes:- "+ strikes.ToString();
    }


        // Update is called once per frame
        void Update()
    {

        //strikeUI.SetText("Strikes:- " + strikes.ToString());

        if (barista.GetComponent<barista>().isSleeping)
        {
            timer += Time.deltaTime;
        }
        
        Debug.Log(timer.ToString());

        if (timer > (30 + Random.Range(5, 10)))
        {
            Instantiate(boss,door.transform);
            strikes++;
            timer = 0;
        }

        if (strikes >= max_strikes)
        {
            barista.GetComponent<barista>().gameOver = true;
            SceneManager.LoadScene(2);
        }
    }
}
