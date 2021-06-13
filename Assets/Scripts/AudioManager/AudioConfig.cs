using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement
{
    [CreateAssetMenu(fileName = "SO_audioConfig", menuName = "AudioConfig")]
    public sealed class AudioConfig : ScriptableObject
    {
        [SerializeField] private AudioData[] _sounds;

        private IReadOnlyDictionary<string, IAudioSetting> _collection = null;

        public IAudioSetting this[string key]
        {
            get
            {
                if (Collection.TryGetValue(key, out var data))
                {
                    return data;
                }

                return null;
            }
        }

        private IReadOnlyDictionary<string, IAudioSetting> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = CreateCollection();
                }

                return _collection;
            }
        }

        private IReadOnlyDictionary<string, IAudioSetting> CreateCollection()
        {
            var collection = new Dictionary<string, IAudioSetting>();
            foreach (var sound in _sounds)
            {
                collection.Add(sound.Key, sound);
            }

            return collection;
        }

        [Serializable]
        private struct AudioData : IAudioSetting
        {
            [SerializeField] private string _key;

            [SerializeField] ValueRange _pitch;

            [SerializeField] private AudioClip[] _clips;

            public string Key => _key;
            float IAudioSetting.Pitch => _pitch.Value;
            IEnumerable<AudioClip> IAudioSetting.Clips => _clips;
        }

        [Serializable]
        private struct ValueRange
        {
            [Range(-3, 3)]
            [SerializeField]
            private float _minValue;

            [Range(-3, 3)]
            [SerializeField]
            private float _maxValue;

            public float Value => UnityEngine.Random.Range(_minValue, _maxValue);
        }
    }
}