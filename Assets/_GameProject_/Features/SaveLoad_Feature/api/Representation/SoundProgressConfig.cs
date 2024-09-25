using UnityEngine;

[CreateAssetMenu(menuName = "Game/ProgressConfig/SoundProgress Config", fileName = "New SoundProgressConfig")]
public class SoundProgressConfig : ScriptableObject
{
    public bool MasterOn;
    [Range(0f, 1f)]
    public float MasterVolume;
    [Space]
    public bool MusicOn;
    [Range(0f, 1f)]
    public float MusicVolume;
    [Space]
    public bool SfxOn;
    [Range(0f, 1f)]
    public float SfxVolume;
    [Space]
    public bool UiOn;
    [Range(0f, 1f)]
    public float UiVolume;
}
