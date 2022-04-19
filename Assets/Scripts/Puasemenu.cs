using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puasemenu : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;

    public void Resume()
    {
        pausemenu.SetActive(false);
    }

    public void Pause()
    {
        pausemenu.SetActive(true);
    }
}
