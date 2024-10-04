using UnityEngine;

namespace WKosArch.GameState_Feature
{
    public class Mapping<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public Mapping(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
