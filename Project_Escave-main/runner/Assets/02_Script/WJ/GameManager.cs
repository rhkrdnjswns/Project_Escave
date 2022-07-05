using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharacterData[] characterDataArray;
    private int totalBest;
    private int characterSelectIndex;

    public int CharacterSelectIndex { get => characterSelectIndex; set => characterSelectIndex = value; }
    public int TotalBest { get => totalBest; set => totalBest = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        instance.characterDataArray[0].IsUnlock = true; // ���� ��� ĳ���� ��� ������ ���ķ� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < instance.characterDataArray.Length; i++)
            {
                instance.characterDataArray[i].IsUnlock = true;
                MainManager.instance.InitCharacterSelectUI(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < instance.characterDataArray.Length; i++)
            {
                instance.characterDataArray[i].IsUnlock = false;
                MainManager.instance.InitCharacterSelectUI(i);
            }
            instance.characterDataArray[0].IsUnlock = true;
            MainManager.instance.InitCharacterSelectUI(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < instance.characterDataArray.Length; i++)
            {
                for (int j = 0; j < instance.characterDataArray[i].styleDataArray.Length; j++)
                {
                    instance.characterDataArray[i].styleDataArray[j].IsUnlock = true;
                }
            }
            //instance.characterDataArray[0].styleDataArray[0].IsUnlock = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 0; i < instance.characterDataArray.Length; i++)
            {
                for (int j = 0; j < instance.characterDataArray[i].styleDataArray.Length; j++)
                {
                    instance.characterDataArray[i].styleDataArray[j].IsUnlock = false;
                }
                instance.characterDataArray[i].StyleIndex = -1;
            }
        }
    }
}
