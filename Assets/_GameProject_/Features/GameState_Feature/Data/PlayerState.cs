using System;

namespace WKosArch.GameState_Feature
{
    [Serializable]
    public class PlayerState
    {
        public int Id;
        public string Name;
        public PlayerWallet Wallet;
    }

    [Serializable]
    public class PlayerLevel
    {
        public int CurrentLevel;
    }
}