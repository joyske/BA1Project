using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class NPCData : ScriptableObject
{
    public List<NPCDialogueData> NPCDialogueData = new List<NPCDialogueData>();
}

[Serializable]
public class NPCDialogueData
{
    [field: SerializeField]
    public int SceneID { get; private set; }

    [field: SerializeField]
    public string OneStarText { get; private set; }

    [field: SerializeField]
    public string TwoStarText { get; private set; }

    [field: SerializeField]
    public string ThreeStarText { get; private set; }

    [field: SerializeField]
    public string MissionText { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public Sprite Sprite { get; private set; }
}



      