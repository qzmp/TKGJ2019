using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode : MonoBehaviour
{
    public static bool IsEasyMode;

    public void SetEasyMode(bool value)
    {
        IsEasyMode = value;
    }

	private void Start()
	{
		GetComponent<Toggle>().isOn = IsEasyMode;
	}
}
