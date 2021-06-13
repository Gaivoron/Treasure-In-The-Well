using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement
{
    public interface IAudioSetting
    {
        string Key { get; }
        float Pitch { get; }
        IEnumerable<AudioClip> Clips { get; }
    }
}