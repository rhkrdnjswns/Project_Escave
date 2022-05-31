using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;

    MapMove gmst;

    // Start is called before the first frame update
    void Start()
    {
        gmst = GameObject.Find("Map").GetComponent<MapMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        GameOver.SetActive(false);
        Obstacle.gameOver = false ;
        gmst.GameStart();
        Time.timeScale = 1f;
    }
}
