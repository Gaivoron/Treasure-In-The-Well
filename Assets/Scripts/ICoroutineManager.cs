using System.Collections;
using UnityEngine;

public interface ICoroutineManager
{
    Coroutine StartCoroutine(IEnumerator routine);
}