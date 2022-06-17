using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance
        {
            get
            {
                if (m_instance == null)
                {
                    var go = new GameObject("COROUTINE MANAGER");
                    m_instance = go.AddComponent<Coroutines>();
                    DontDestroyOnLoad(go);
            }
                return m_instance;
            }
        }
        private static Coroutines m_instance;
        public static Coroutine StartRoutine(IEnumerator enumerator)
        {
            return _instance.StartCoroutine(enumerator);
        }
        public static void StopRoutine(IEnumerator routine)
        {
            _instance.StopCoroutine(routine);
        }

    internal static void StartRoutine(object v)
    {
        throw new NotImplementedException();
    }
}
