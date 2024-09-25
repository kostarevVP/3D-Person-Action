using System;
using WKosArch.Domain.Features;

public interface ISaveLoadFeature : IFocusPauseFeature
{
    event Action OnSaveProgressStarted;

    public GameProgressData LoadProgress();
    public void SaveProgress();
}
