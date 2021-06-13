using ObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace AudioManagement
{
    internal sealed class Sound : ISound, IObject<string>
    {
        private readonly IAudioSetting _setting;
        private readonly AudioSource _source;

        public bool IsPlaying
        {
            get;
            private set;
        }

        public Sound(IAudioSetting setting, GameObject parent, AudioClip clip)
        {
            _setting = setting;

            _source = parent.AddComponent<AudioSource>();
            _source.clip = clip;
        }

        void ISound.Play(bool loop)
        {
            IsPlaying = true;

            _source.loop = loop;
            _source.pitch = _setting.Pitch;
            _source.Play();

            if (!loop)
            {
                //TODO - wait for sound to end.
                var duration = _source.clip.length;
                CoroutineManager.Instance.StartCoroutine(Stop(duration));
            }
        }

        private IEnumerator Stop(float duration)
        {
            yield return new WaitForSeconds(duration);
            Release();
        }

        //TODO - turn into a partial class?
        public event Action Released;

        string IObject<string>.Key => _setting.Key;

        void IObject<string>.Init()
        {
            IsPlaying = false;
        }

        public void Release()
        {
            IsPlaying = false;

            _source.Stop();
            Released?.Invoke();
        }
    }
}