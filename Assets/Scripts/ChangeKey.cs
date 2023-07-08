using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour, IPointerDownHandler
{
    public enum GameAction
    {
        Forward,
        Back,
        Right,
        Left,
        Inventory
    }

    public GameAction gameAction;

    private bool listeningForKey = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartListeningForKey();
    }

    private void StartListeningForKey()
    {
        listeningForKey = true;
        StartCoroutine(KeyListener());
    }

    private System.Collections.IEnumerator KeyListener()
    {
        while (listeningForKey)
        {
            yield return null;

            if (Input.anyKeyDown)
            {
                KeyCode newKey = GetPressedKey();

                bool keyAlreadyAssigned = CheckIfKeyAlreadyAssigned(newKey);

                if (!keyAlreadyAssigned)
                {
                    ModifyKey(newKey);
                    Debug.Log("Nouvelle touche : " + newKey);
                }
                else
                {
                    Debug.Log("La touche est déjà attribuée à une autre action !");
                }

                StopListeningForKey();
                yield break;
            }
        }

        StartListeningForKey();
    }

    private KeyCode GetPressedKey()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }

        return KeyCode.None;
    }

    private bool CheckIfKeyAlreadyAssigned(KeyCode newKey)
    {
        SettingsManager settingsManager = SettingsManager.Instance;

        if (newKey == settingsManager.forwardKey || newKey == settingsManager.backKey || newKey == settingsManager.rightKey || newKey == settingsManager.leftKey || newKey == settingsManager.inventoryKey)
        {
            return true;
        }

        return false;
    }

    private void ModifyKey(KeyCode newKey)
    {
        SettingsManager settingsManager = SettingsManager.Instance;

        switch (gameAction)
        {
            case GameAction.Forward:
                settingsManager.forwardKey = newKey;
                break;
            case GameAction.Back:
                settingsManager.backKey = newKey;
                break;
            case GameAction.Right:
                settingsManager.rightKey = newKey;
                break;
            case GameAction.Left:
                settingsManager.leftKey = newKey;
                break;
            case GameAction.Inventory:
                settingsManager.inventoryKey = newKey;
                break;
        }
    }

    private void StopListeningForKey()
    {
        listeningForKey = false;
    }
}