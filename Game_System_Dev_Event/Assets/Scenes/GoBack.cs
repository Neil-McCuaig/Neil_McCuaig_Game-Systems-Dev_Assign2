using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public void level1Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
