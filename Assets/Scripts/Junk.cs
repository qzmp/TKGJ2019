using System;
using UnityEngine.Events;

namespace Junk
{
    [Serializable]
    public class BoolEvent : UnityEvent<bool> {}

    [Serializable]
    public class FloatEvent : UnityEvent<float> {}
}