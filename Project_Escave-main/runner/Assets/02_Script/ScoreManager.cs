using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject[] NumberImage;
    public Sprite[] Number;


    // Update is called once per frame
    private void Update()
    {
        int temp = DataManager.Instance.score / 100;
        NumberImage[0].GetComponent<Image>().sprite = Number[temp];

        int temp2 = DataManager.Instance.score % 100;

        temp2 = temp2 / 10;
        NumberImage[1].GetComponent<Image>().sprite = Number[temp2];

        int temp3 = DataManager.Instance.score % 10;
        NumberImage[2].GetComponent<Image>().sprite = Number[temp3];
    }
}
