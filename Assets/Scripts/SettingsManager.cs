using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public event Action<KeyCode, KeyCode, KeyCode, KeyCode, KeyCode> OnKeyChanged;

    private static SettingsManager instance;

    public KeyCode forwardKey = KeyCode.Z;
    public KeyCode backKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode inventoryKey = KeyCode.I;

    public Text forwardText;
    public Text backText;
    public Text rightText;
    public Text leftText;
    public Text inventoryText;

    public static SettingsManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        UpdateKeyTexts();
        // Debug.Log(forwardKey);
    }

    private void KeyChanged()
    {
        if (OnKeyChanged != null)
        {
            OnKeyChanged(forwardKey, backKey, rightKey, leftKey, inventoryKey);
        }
    }

    private void UpdateKeyTexts()
    {
        forwardText.text = forwardKey.ToString();
        backText.text = backKey.ToString();
        rightText.text = rightKey.ToString();
        leftText.text = leftKey.ToString();
        inventoryText.text = inventoryKey.ToString();

        KeyChanged();
    }

}