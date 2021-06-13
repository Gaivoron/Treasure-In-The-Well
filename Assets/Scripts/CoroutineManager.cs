using System.Collections;
using UnityEngine;
//TODO - move to a different file
public sealed class CoroutineManager : MonoBehaviour, ICoroutineManager
{
    private static ICoroutineManager _instance;

    public static ICoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var instance = new GameObject(nameof(CoroutineManager)).AddComponent<CoroutineManager>();
                DontDestroyOnLoad(instance.gameObject);
                _instance = instance;
            }

            return _instance;
        }
    }

    Coroutine ICoroutineManager.StartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
}
