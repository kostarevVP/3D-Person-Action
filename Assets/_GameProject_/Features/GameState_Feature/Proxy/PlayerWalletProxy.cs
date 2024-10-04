using R3;
using System;

namespace WKosArch.GameState_Feature
{
    public class PlayerWalletProxy
    {
        public string HardCurrencyName { get; }
        public string SoftCurrencyName { get; }
        public ReactiveProperty<int> HardCurrency { get; }
        public ReactiveProperty<int> SoftCurrency { get; }

        public PlayerWalletProxy(PlayerWallet originPlayerWallet)
        {
            HardCurrencyName = originPlayerWallet.HardCurrencyName;
            SoftCurrencyName = originPlayerWallet.SoftCurrencyName;
            HardCurrency = new ReactiveProperty<int>(originPlayerWallet.HardCurrency);
            SoftCurrency = new ReactiveProperty<int>(originPlayerWallet.SoftCurrency);

            HardCurrency.Skip(1).Subscribe(value => originPlayerWallet.HardCurrency = value);
            SoftCurrency.Skip(1).Subscribe(value => originPlayerWallet.SoftCurrency = value);
        }
    }
}