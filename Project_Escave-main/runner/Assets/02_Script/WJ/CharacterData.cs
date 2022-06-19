using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable/Character")]
public class CharacterData : ScriptableObject
{
    public string name;
    public int record;
    public Sprite texture;
    public StyleData[] styleDataArray;
    private bool isUnlock;
    private int styleIndex = -1;

    public bool IsUnlock { get => isUnlock; set => isUnlock = value; }
    public int StyleIndex { get => styleIndex; set => styleIndex = value; }
}
