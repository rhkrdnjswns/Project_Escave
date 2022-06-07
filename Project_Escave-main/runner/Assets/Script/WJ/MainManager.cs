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
    public Text styleTxt;
    public Image[] styleIconArray;
    public GameObject stylePopUp;
    public GameObject styleConditionPopUp;
    private CharacterData currentCharacter;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InitAllCharacterSelectUI();
        totalBestTxt.text = GameManager.instance.TotalBest == 0 ? "��� ����" : GameManager.instance.TotalBest.ToString();
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
            characterSelectUIArray[i].GetChild(0).GetComponent<Image>().sprite = GameManager.instance.characterDataArray[i].texture;
            characterSelectUIArray[i].GetChild(1).GetComponent<Text>().text = GameManager.instance.characterDataArray[i].name;
        }
        else
        {
            characterSelectUIArray[i].GetChild(0).GetComponent<Image>().sprite = lockIcon;
            characterSelectUIArray[i].GetChild(1).GetComponent<Text>().text = "���";
        }
    }
    private void UpdateCurrentCharacter(int index)
    {
        if(currentCharacter == GameManager.instance.characterDataArray[index] || !GameManager.instance.characterDataArray[index].IsUnlock)
        {
            return;
        }
        if(currentCharacter != null)
        {
            int i = System.Array.IndexOf(GameManager.instance.characterDataArray, currentCharacter);
            characterSelectUIArray[i].GetComponent<Outline>().enabled = false;
        }
        characterSelectUIArray[index].GetComponent<Outline>().enabled = true;
        currentCharacter = GameManager.instance.characterDataArray[index];
        characterBestTxt.text = currentCharacter.record == 0 ? "��� ����" : currentCharacter.record.ToString();
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
            }
        }
        else
        {
            styleTxt.text = "Īȣ ����";
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
                styleUIArray[i].GetChild(0).GetComponent<Text>().text = "���";
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
        stylePopUp.SetActive(!stylePopUp.activeSelf);
        UpdateStylePopUpUI(stylePopUp.activeSelf);
    }
    private void SelectStyle(int index)
    {
        if (!currentCharacter.styleDataArray[index].IsUnlock)
        {
            styleConditionPopUp.SetActive(!styleConditionPopUp.activeSelf);
            styleConditionPopUp.GetComponentInChildren<Text>().text = currentCharacter.styleDataArray[index].condition;
            return;
        }
        if(currentCharacter.StyleIndex >= 0)
        {
            styleUIArray[currentCharacter.StyleIndex].GetComponent<Outline>().enabled = false;
        }
        styleUIArray[index].GetComponent<Outline>().enabled = true;
        currentCharacter.StyleIndex = index;
    }
    public void BtnEvt_ActiveStyleConditionUI()
    {
        styleConditionPopUp.SetActive(!styleConditionPopUp.activeSelf);
    }
}
