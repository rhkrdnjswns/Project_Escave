using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameManager : MonoBehaviour
{
    public ScrollUtility scrollSpeed; // 부스터 먹었을 때 맵 스크롤 스피드 조절
    public int score;
    public GameObject[] mapArray;
    public GameObject gameOverObj;
    public GameObject currentMap;
    public GameObject previousMap;
    public Image fadeImage;
    public float hpDelay;
    public float hpDecreaseValue;
    private float currentHp;
    public Image hpBar;

    private float scoreUpDelay;
    public static InGameManager instance;

    private void Awake()
    {
        scoreUpDelay = 0.5f;
        instance = this;
        ChangeMap();
    }
    private void Start()
    {
        currentHp = Player.instance.hp;
        StartCoroutine(Co_Score());
        StartCoroutine(Co_UpdateHp());
    }
    public void GameOver()
    {
        //Time.timeScale = 0;
        Debug.Log("Game Over");
        //gameOverObj.SetActive(true);
    }

    public void ChangeMap()
    {
        StartCoroutine(Co_Fade());
        if (currentMap != null)
        {
            previousMap = currentMap;
        }
        currentMap = SelectRandomMap();
        currentMap.SetActive(true);
        if (previousMap != null)
        {
            previousMap.SetActive(false);
            if (previousMap == mapArray[0])
            {
                previousMap.transform.GetChild(1).position = new Vector3(18, -0.8f);
            }
            else
            {
                previousMap.transform.GetChild(1).position = new Vector3(0, -0.8f);
            }
        }
    }
    private IEnumerator Co_Fade()
    {
        fadeImage.DOFade(1, 1f);
        yield return new WaitForSeconds(1.2f);
        fadeImage.DOFade(0, 1f);
    }
    private GameObject SelectRandomMap()
    {
        int rand = Random.Range(0, mapArray.Length);
        if (currentMap == mapArray[rand])
        {
            return SelectRandomMap();
        }
        else
        {
            return mapArray[rand];
        }
    }

    public void BoosterUtility(float speed, bool isBooster)
    {
        scrollSpeed.speed = speed;
        if (isBooster)
        {
            scoreUpDelay = 0.25f;
        }
        else
        {
            scoreUpDelay = 0.5f;
        }
    }

    private IEnumerator Co_Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreUpDelay);
            score++;
            print(score);
        }
    }
    private IEnumerator Co_UpdateHp()
    {
        while (true)
        {
            yield return new WaitForSeconds(hpDelay);
            currentHp -= hpDecreaseValue;
            hpBar.fillAmount = 1 / Player.instance.hp * currentHp;
            print(currentHp);
        }
    }
}
