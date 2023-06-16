using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class FirstSkill : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<GameObject> gameObjects;
    [SerializeField] CanvasGroup canvasGroup;
    bool activateScript = false;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.5f;
        DisableScripts();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        activateScript = true;
        DisableScripts();
    }

    private void DisableScripts()
    {
        foreach (GameObject obj in gameObjects)
        {
            ImageClickHandler ImageClickHandlerScript = obj.GetComponent<ImageClickHandler>();

            if (ImageClickHandlerScript != null)
            {
                ImageClickHandlerScript.enabled = false;
            }

            if (activateScript)
            {
                ImageClickHandlerScript.enabled = true;
            }
        }
    }
}