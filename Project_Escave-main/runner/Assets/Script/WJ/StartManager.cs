using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartManager : MonoBehaviour
{
    public Image fadeOut;
    public Image sceneLoad;
    public GameObject[] backgroundObjArray;
    public Transform camera;
    public Text startText;
    public Text[] titleText;
    public Vector3 startPoint = new Vector3(-6.6f, 0, -10);
    private Color color;
    public Color[] cameraColor;
    private int arrayIndex = 0;
    private bool isReady;
    private void SuffelArray()
    {
        for (int i = 0; i < backgroundObjArray.Length; ++i)
        {
            int randA = Random.Range(0, backgroundObjArray.Length);
            int randB = Random.Range(0, backgroundObjArray.Length);
            var tempA = backgroundObjArray[randA];
            var tempB = cameraColor[randA];
            backgroundObjArray[randA] = backgroundObjArray[randB];
            backgroundObjArray[randB] = tempA;
            cameraColor[randA] = cameraColor[randB];
            cameraColor[randB] = tempB;
        }
        backgroundObjArray[arrayIndex].SetActive(true);
        Camera.main.backgroundColor = cameraColor[arrayIndex];
    }
    private void Awake()
    {
        SuffelArray();
        color = titleText[0].color;
        for (int i = 0; i < titleText.Length; i++)
        {
            titleText[i].color = Color.clear;
        }
        StartCoroutine(Co_ChangeBackground());
    }
    private IEnumerator Co_ChangeBackground()
    {
        fadeOut.DOColor(Color.clear, 1f);
        StartCoroutine(Co_ActiveText());
        while (true)
        {
            camera.position = startPoint;
            yield return new WaitForSeconds(11f);
            fadeOut.DOColor(Color.black, 2f);
            yield return new WaitForSeconds(2f);
            backgroundObjArray[arrayIndex++].SetActive(false);
            if(arrayIndex > backgroundObjArray.Length-1)
            {
                arrayIndex = 0;
            }
            backgroundObjArray[arrayIndex].SetActive(true);
            Camera.main.backgroundColor = cameraColor[arrayIndex];
            yield return new WaitForSeconds(1f);
            fadeOut.DOColor(Color.clear, 1f);
        }
    }
    private IEnumerator Co_ActiveText()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < titleText.Length; i++)
        {
            titleText[i].gameObject.SetActive(true);
            titleText[i].DOColor(color, 2f);
        }
        yield return new WaitForSeconds(1f);
        startText.DOColor(Color.white, 2f);
        isReady = true;
        while (true)
        {
            yield return new WaitForSeconds(4f);
            startText.DOColor(Color.clear, 1f);
            yield return new WaitForSeconds(1f);
            startText.DOColor(Color.white, 1f);
        }

    }
    private IEnumerator Co_LoadMainScene()
    {
        sceneLoad.DOColor(Color.black, 2f);
        yield return new WaitForSeconds(2f);
        ScnenLoadManager.LoadMainScene();
    }
    private void Update()
    {
        if (!isReady)
        {
            return;
        }
        if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("���� ����");
            StartCoroutine(Co_LoadMainScene());
            isReady = false;
        }
    }
    private void LateUpdate()
    {
        camera.position += Vector3.right * Time.deltaTime * 1;
    }
}
