using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScnenLoadManager// : MonoBehaviour
{
    public static void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
