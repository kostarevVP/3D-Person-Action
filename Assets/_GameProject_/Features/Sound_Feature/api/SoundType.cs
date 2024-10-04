using System;

[Serializable]
public enum SoundType
{
    Unknown = 0,

    #region UI
    Ok = 1,
    Cancel = 2,
    #endregion

    #region SFX
    VehicleEngine = 3,
    #endregion

    #region Music
    LoadGameMusic = 4,
    #endregion
}
