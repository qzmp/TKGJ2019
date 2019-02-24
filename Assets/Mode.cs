using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour
{
    public static bool IsEasyMode;

    public void SetEasyMode(bool value)
    {
        IsEasyMode = value;
    }
}
