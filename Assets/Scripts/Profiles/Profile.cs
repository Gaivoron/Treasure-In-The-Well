using System;
using System.Collections.Generic;
using UnityEngine;

namespace Profiles
{
    [Serializable]
    public sealed class Profile
    {
        [SerializeField] private List<Level> _levels;

        [Serializable]
        private struct Level
        {
            [SerializeField] private int _index;
            [SerializeField] private int _reward;
            [SerializeField] private float _time;
        }
    }
}