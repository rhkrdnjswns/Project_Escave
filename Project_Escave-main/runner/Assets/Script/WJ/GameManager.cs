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
        instance.characterDataArray[0].IsUnlock = true; // 추후 모든 캐릭터 언락 데이터 정렬로 변경
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            instance.characterDataArray[1].IsUnlock = true;
            MainManager.instance.InitCharacterSelectUI(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            instance.characterDataArray[0].styleDataArray[0].IsUnlock = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            instance.characterDataArray[0].styleDataArray[0].IsUnlock = false;
            instance.characterDataArray[0].StyleIndex = -1;
        }
    }
}
