using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Style",menuName ="Scriptable/Style")]
public class StyleData : ScriptableObject
{
    public string name;
    public Sprite icon;
    public string condition;
    private bool isUnlock;

    public bool IsUnlock { get => isUnlock; set => isUnlock = value; }
}
