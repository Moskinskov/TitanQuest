using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Stat
    {
        [SerializeField] int baseValue;

        public int GetValue
        {
            get { return baseValue; }
        }
    }
}