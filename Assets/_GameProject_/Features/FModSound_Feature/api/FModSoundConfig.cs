using UnityEngine;
using System;
using FMODUnity;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "FModSoundConfig", menuName = "Game/Configs/FMod/FModSoundConfig")]
public class FModSoundConfig : ScriptableObject
{
    public EventReference Name;
    public FloatParameterDefinition[] FloatParameters;
    [Space]
    [Header("Dont change Id its generate automaticly")]
    public int Id;

    [NonSerialized]
    public int RuntimeIndex;



#if UNITY_EDITOR
    private void Reset()
    {
        var sounds = AssetDatabase.FindAssets("t:FModSoundConfig");
        var firstAvailableId = 1;
        foreach (var soundGuid in sounds)
        {
            var sound = AssetDatabase.LoadAssetAtPath<FModSoundConfig>(
                AssetDatabase.GUIDToAssetPath(soundGuid));
            if (sound.Id >= firstAvailableId)
                firstAvailableId = sound.Id + 1;
        }

        Id = firstAvailableId;
    }
#endif
}
[Serializable]
public class FloatParameterDefinition
{
    public string Name;
    public float DefaultValue;

    [NonSerialized]
    public int RuntimeIndex;
}

