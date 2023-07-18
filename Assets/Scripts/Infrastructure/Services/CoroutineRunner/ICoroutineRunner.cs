using System.Collections;
using UnityEngine;

namespace Scripts.Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}