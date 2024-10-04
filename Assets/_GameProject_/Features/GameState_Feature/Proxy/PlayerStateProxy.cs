using R3;
using System;

namespace WKosArch.GameState_Feature
{
    public class PlayerStateProxy
    {
        public int Id { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<PlayerWalletProxy> WalletProxy;

        public PlayerStateProxy(PlayerState originPlayerState)
        {
            Id = originPlayerState.Id;
            Name = new ReactiveProperty<string>(originPlayerState.Name);
            WalletProxy = new ReactiveProperty<PlayerWalletProxy>(new PlayerWalletProxy(originPlayerState.Wallet));

            Name.Skip(1).Subscribe(value => originPlayerState.Name = value);
        }
    }
}