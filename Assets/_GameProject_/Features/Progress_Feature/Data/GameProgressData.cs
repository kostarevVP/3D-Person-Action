using System;
using Unity.Mathematics;

[Serializable]
public class GameProgressData
{
    public GlobaProgressData GlobalProgress;
    public SceneProgressData SceneProgressData;
    public int SavedSceneIndex;

    public GameProgressData()
    {
        GlobalProgress = new();
        SceneProgressData = new();
    }
}

[Serializable]
public class GlobaProgressData
{
    public SoundSettingsData SoundProgressData;
    public GlobaProgressData()
    {
        SoundProgressData = new();
    }

}

[Serializable]
public class SoundSettingsData
{
    public float MasterVolume;
    public bool MasterOn;
    public float MusicVolume;
    public bool MusicOn;
    public float SfxVolume;
    public bool SfxOn;
    public float UiVolume;
    public bool UiOn;
}

[Serializable]
public class SceneProgressData
{
    public int SceneIndex;
    public PlayerProgressData PlayerProgressData;

    public SceneProgressData()
    {
        PlayerProgressData = new();
    }
}

[Serializable]
public class PlayerProgressData
{
    public LocalTransformData LocalTransformData;
}

[Serializable]
public class LocalTransformData
{
    public float3 Position;
    public quaternion Rotation;
    public float Scale;

    public LocalTransformData(float3 position, quaternion rotation, float scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
}