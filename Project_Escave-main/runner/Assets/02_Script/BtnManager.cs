using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject SettingPanel;

    MapMove gmst;
    Character aa;
    private GameObject Map;

    // Start is called before the first frame update
    void Start()
    {
        gmst = GameObject.Find("Map").GetComponent<MapMove>();
        aa = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        GameOver.SetActive(false);
        SettingPanel.SetActive(false);
        Obstacle.gameOver = false ;
        gmst.GameStart();
        gmst.mapSpeed = 9f;
        Time.timeScale = 1f;
    }

    public void SettingBtn()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void SettingClose()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GoMain()
    {
        Time.timeScale = 1;
        //æ¿¿Ãµø
    }

    public void SlidBt()
    {
        aa.SlidBtn();
    }

    public void SlidBtnU()
    {
        aa.SlidBtnUp();
    }

    public void Jump_Bt()
    {
        aa.Jump_Btn();
    }
}
