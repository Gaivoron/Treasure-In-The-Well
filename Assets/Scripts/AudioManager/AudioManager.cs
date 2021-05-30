using ObjectPool;
using System;
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
            return data.Clips.Select((clip) => new Sound(data, gameObject, clip));
        }
    }
}  
 
namespace ObjectPool
{
    public sealed class Pool<TKey, TObject> : IPool<TKey, TObject> where TObject : IObject<TKey>
    {
        private readonly Func<TKey, IEnumerable<TObject>> _factory;
        private readonly IDictionary<TKey, List<TObject>> _available = new Dictionary<TKey, List<TObject>>();
        private readonly IList<TObject> _used = new List<TObject>();

        public Pool(Func<TKey, IEnumerable<TObject>> factory)
        {
            _factory = factory;
        }

        TObject IPool<TKey, TObject>.Get(TKey key)
        {
            if (!_available.TryGetValue(key, out var items))
            {
                items = new List<TObject>();
                _available.Add(key, items);
            }

            if (items.Count < 1)
            {
                IEnumerable<TObject> batch;
                try
                {
                    batch = Create(key);
                }
                catch
                {
                    return default;
                }

                items.AddRange(batch);
            }

            return Get(items);
        }

        private IEnumerable<TObject> Create(TKey key)
        {
            var items = _factory(key);
            if (items == null || items.Count() == 0)
            {
                throw new Exception("No objects to intialize pool!");
            }

            foreach (var item in items)
            {
                item.Release();
            }

            return items;
        }

        private TObject Get(List<TObject> items)
        {
            //TODO - to random extensions.
            var index = UnityEngine.Random.Range(0, items.Count);
            var item = items[index];
            items.RemoveAt(index);

            item.Init();
            _used.Add(item);
            item.Released += OnReleased;

            return item;

            void OnReleased()
            {
                item.Released -= OnReleased;
                _used.Remove(item);
                if (_available.TryGetValue(item.Key, out var available))
                {
                    available.Add(item);
                }
            }
        }
    }

    //TODO - move to another folder
    public interface IPool<TKey, TObject> where TObject : IObject<TKey>
    {
        TObject Get(TKey key);
    }

    public interface IObject<T>
    {
        event Action Released;

        T Key { get; }

        void Init();
        void Release();
    }
}