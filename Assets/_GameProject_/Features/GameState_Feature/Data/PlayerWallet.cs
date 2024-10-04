using System;

namespace WKosArch.GameState_Feature
{
    [Serializable]
    public class PlayerWallet
    {
        public string HardCurrencyName;
        public string SoftCurrencyName;
        public int HardCurrency;
        public int SoftCurrency;
    }
}