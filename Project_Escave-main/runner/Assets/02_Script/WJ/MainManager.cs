using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public Transform[] characterSelectUIArray;
    public Transform[] styleUIArray;
    public Sprite lockIcon;
    public Text characterBestTxt;
    public Text totalBestTxt;
    public Text totalTxt;
    public Text styleTxt;
    public Image[] styleIconArray;
    public GameObject stylePopUp;
    public GameObject conditionPopUp;
    public GameObject settingObj;
    private CharacterData currentCharacter;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitAllCharacterSelectUI();
        totalBestTxt.text = GameManager.instance.TotalBest == 0 ? "기록 없음" : GameManager.instance.TotalBest.ToString();
        totalTxt.text = GameManager.instance.Total == 0 ? "기록 없음" : GameManager.instance.Total.ToString();
        UpdateCurrentCharacter(GameManager.instance.CharacterSelectIndex);
    }
    private void InitAllCharacterSelectUI()
    {
        for (int i = 0; i < GameManager.instance.characterDataArray.Length; i++)
        {
            InitCharacterSelectUI(i);
        }
    }
    public void InitCharacterSelectUI(int i)
    {
        if (GameManager.instance.characterDataArray[i].IsUnlock)
        {
            characterSelectUIArray[i].GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            characterSelectUIArray[i].GetChild(1).GetComponent<Text>().text = GameManager.instance.characterDataArray[i].name;
            characterSelectUIArray[i].GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            characterSelectUIArray[i].GetChild(0).GetComponent<Image>().gameObject.SetActive(false);
            characterSelectUIArray[i].GetChild(1).GetComponent<Text>().text = "잠김";
            characterSelectUIArray[i].GetChild(2).gameObject.SetActive(true);
        }
    }
    private void UpdateCurrentCharacter(int index)
    {
        if(currentCharacter == GameManager.instance.characterDataArray[index] || !GameManager.instance.characterDataArray[index].IsUnlock)
        {
            string txt;
            conditionPopUp.SetActive(!conditionPopUp.activeSelf);
            switch (index)
            {
                case 1:
                    txt = "누적 기록 2000 달성";
                    break;
                case 2:
                    txt = "누적 기록 4000 달성";
                    break;
                case 3:
                    txt = "누적 기록 8000 달성";
                    break;
                case 4:
                    txt = "누적 기록 10000 달성";
                    break;
                default:
                    txt = $"{index} : Index Error!";
                    conditionPopUp.SetActive(!conditionPopUp.activeSelf);
                    break;
            }      
            conditionPopUp.GetComponentInChildren<Text>().text = txt;
            return;
        }
        if(currentCharacter != null)
        {
            int i = System.Array.IndexOf(GameManager.instance.characterDataArray, currentCharacter);
            characterSelectUIArray[i].GetComponent<Outline>().enabled = false;
        }
        characterSelectUIArray[index].GetComponent<Outline>().enabled = true;
        currentCharacter = GameManager.instance.characterDataArray[index];
        characterBestTxt.text = currentCharacter.record == 0 ? "기록 없음" : currentCharacter.record.ToString();
        UpdateStyleUI();
    }
    private void UpdateStyleUI()
    {
        if (currentCharacter.StyleIndex >= 0)
        {
            styleTxt.text = currentCharacter.styleDataArray[currentCharacter.StyleIndex].name;
            foreach (var item in styleIconArray)
            {
                item.sprite = currentCharacter.styleDataArray[currentCharacter.StyleIndex].icon;
                item.color = Color.white;
            }
        }
        else
        {
            styleTxt.text = "칭호 없음";
            foreach (var item in styleIconArray)
            {
                item.color = Color.clear;
            }
        }
    }
    private void UpdateStylePopUpUI(bool isOpen)
    {
        if (!isOpen)
        {
            foreach (var item in styleUIArray)
            {
                item.gameObject.SetActive(isOpen);
                item.GetComponent<Outline>().enabled = false;
            }
            return;
        }
        for (int i = 0; i < currentCharacter.styleDataArray.Length; i++)
        {
            if (currentCharacter.styleDataArray[i].IsUnlock)
            {
                styleUIArray[i].GetChild(0).GetComponent<Text>().text = currentCharacter.styleDataArray[i].name;
                styleUIArray[i].GetChild(1).GetComponent<Image>().sprite = currentCharacter.styleDataArray[i].icon;
                styleUIArray[i].GetChild(2).GetComponent<Image>().sprite = currentCharacter.styleDataArray[i].icon;
            }
            else
            {
                styleUIArray[i].GetChild(0).GetComponent<Text>().text = "잠김";
                styleUIArray[i].GetChild(1).GetComponent<Image>().sprite = lockIcon;
                styleUIArray[i].GetChild(2).GetComponent<Image>().sprite = lockIcon;
            }
            styleUIArray[i].gameObject.SetActive(true);
        }
    }
    public void BtnEvt_SelectCharacter(int index)
    {
        UpdateCurrentCharacter(index);
    }
    public void BtnEvt_StylePopUp()
    {
        stylePopUp.SetActive(!stylePopUp.activeSelf);
        UpdateStylePopUpUI(stylePopUp.activeSelf);
    }
    public void BtnEvt_SelectStyle(int index)
    {
        SelectStyle(index);
    }
    public void BtnEvt_EquipeStyle()
    {
        UpdateStyleUI();
        //stylePopUp.SetActive(!stylePopUp.activeSelf);
        UpdateStylePopUpUI(stylePopUp.activeSelf);
    }
    private void SelectStyle(int index)
    {
        if (!currentCharacter.styleDataArray[index].IsUnlock)
        {
            conditionPopUp.SetActive(!conditionPopUp.activeSelf);
            conditionPopUp.GetComponentInChildren<Text>().text = currentCharacter.styleDataArray[index].condition;
            return;
        }
        if(currentCharacter.StyleIndex >= 0)
        {
            styleUIArray[currentCharacter.StyleIndex].GetComponent<Outline>().enabled = false;
        }
        styleUIArray[index].GetComponent<Outline>().enabled = true;
        currentCharacter.StyleIndex = index;
    }
    public void BtnEvt_ActiveConditionUI()
    {
        conditionPopUp.SetActive(!conditionPopUp.activeSelf);
    }
    public void BtnEvt_ActiveSetting()
    {
        settingObj.SetActive(!settingObj.activeSelf);
    }
}
