using ObjectPool;
using System;
using UnityEngine;

namespace AudioManagement
{
    public sealed partial class AudioManager
    {
        private sealed class Sound : ISound, IObject<string>
        {
            private readonly IAudioSetting _setting;
            private readonly AudioSource _source;

            public Sound(IAudioSetting setting, GameObject parent, AudioClip clip)
            {
                _setting = setting;

                _source = parent.AddComponent<AudioSource>();
                _source.clip = clip;
            }

            void ISound.Play(bool loop)
            {
                _source.loop = loop;
                _source.pitch = _setting.Pitch;
                _source.Play();
            }

            //TODO - turn into a partial class?
            public event Action Released;

            string IObject<string>.Key => _setting.Key;

            void IObject<string>.Init()
            {
            }

            void IObject<string>.Release()
            {
                _source.Stop();
            }
        }
    }
}  
