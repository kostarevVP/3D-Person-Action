using UnityEngine;

[CreateAssetMenu(fileName = "FModEvents", menuName = "Game/Configs/FMod/FModEvents")]
public class FModEventsConfig : ScriptableObject
{
    public FModSoundConfig[] FModSoundConfigs => _fModSoundConfigs;

    [SerializeField]
    private FModSoundConfig[] _fModSoundConfigs;

}
