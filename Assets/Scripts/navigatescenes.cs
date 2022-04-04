using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigatescenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void manageScene(int x) 
    {
        SceneManager.LoadScene(x);
    }
}
