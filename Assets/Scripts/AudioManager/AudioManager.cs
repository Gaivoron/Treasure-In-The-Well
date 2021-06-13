using ObjectPool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioManagement
{
    public sealed partial class AudioManager : MonoBehaviour, IAudioManager
    {
        public static IAudioManager Instance
        {
            get;
            private set;
        }

        [SerializeField] private AudioConfig _config;

        //TODO - delete.
        [SerializeField] private AudioSource Music;

        private IPool<string, Sound> _pool;

        private IPool<string, Sound> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new Pool<string, Sound>(Create);
                }

                return _pool;
            }
        }

        ISound IAudioManager.Get(string key)
        {
            return Pool.Get(key);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //TODO - delete?
        private void Start()
        {
            MusicPlay();
        }

        private void MusicPlay()
        {
            Music.playOnAwake = false;
        }

        private IEnumerable<Sound> Create(string key)
        {
            var data = _config[key];
            return data.Clips.Select((clip) => new Sound(data, gameObject, clip)).ToArray();
        }
    }
}  