using UnityEngine;
using UnityEngine.Events;

class DebugInput : MonoBehaviour
{
    [System.Serializable]
    public struct InputEntry
    {
        public KeyCode code;
        public UnityEvent onKeyDown;
    }

    public InputEntry[] entries;

    void Update()
    {
        foreach (var entry in entries)
        {
            if (Input.GetKeyDown(entry.code))
            {
                entry.onKeyDown.Invoke();
            }
        }
    }
}