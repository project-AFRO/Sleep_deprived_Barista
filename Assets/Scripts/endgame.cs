using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class endgame : MonoBehaviour
{
    [SerializeField] private GameObject barista;
    [SerializeField] private Score score;
    [SerializeField] private Score PlayerHighscore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private FileStream highscoreRead;
    [SerializeField] private FileStream highscoreWrite;

    [SerializeField]
    private int scr;



    StreamReader sr;
    StreamWriter sw;
    bool isNew;
    // Start is called before the first frame update
    void Start()
    {

        score = barista.GetComponent<barista>().playerscore;
        HighscoreRead();
        isNew = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        scr = (int)((score.timeSpent + score.costumersServed) / score.wallet);
        scoreText.text = "Game Over";
    }

    public void HighscoreRead()
    {
        highscoreRead = new FileStream("highscore.txt", FileMode.OpenOrCreate);
         sr = new StreamReader(highscoreRead);

        while (sr.Peek() != -1)
        {
           PlayerHighscore.timeSpent = float.Parse(sr.ReadLine());
           PlayerHighscore.costumersServed = int.Parse(sr.ReadLine());
           PlayerHighscore.costumersNotServed = int.Parse(sr.ReadLine());
           PlayerHighscore.wallet = float.Parse(sr.ReadLine());
        }
        
        sr.Close();
    }
    public void HighscoreWrite()
    {
        if(score.costumersServed > PlayerHighscore.costumersServed)
        {
            PlayerHighscore.costumersServed = score.costumersServed;
            isNew = true;
        }
        if (score.wallet > PlayerHighscore.wallet)
        {
            PlayerHighscore.wallet = score.wallet;
            isNew = true;
        }
        if (score.costumersNotServed < PlayerHighscore.costumersNotServed)
        {
            PlayerHighscore.costumersNotServed = score.costumersNotServed;
            isNew = true;
        }
        if(score.timeSpent > PlayerHighscore.timeSpent)
        {
            PlayerHighscore.timeSpent = score.timeSpent;
            isNew = true;
        }

        if (PlayerHighscore != null)
        {
            highscoreWrite = new FileStream("highscore.txt", FileMode.Append);
            sw = new StreamWriter(highscoreWrite);
            sw.WriteLine(PlayerHighscore.timeSpent);
            sw.WriteLine(PlayerHighscore.costumersServed);
            sw.WriteLine(PlayerHighscore.costumersNotServed);
            sw.WriteLine(PlayerHighscore.wallet);
        }
        sw.Close();
    }
    public Score getHighscore()
    {
        HighscoreRead();
        return PlayerHighscore;
    }
}
