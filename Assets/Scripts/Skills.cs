using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Skills : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    private int nextImageIndex;

    private void Start()
    {
        nextImageIndex = 0;
        SetInteractable(false);

        foreach (Image image in images)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.5f;
            }
        }

        if (images.Count > 0)
        {
            images[0].GetComponent<CanvasGroup>().interactable = true;
        }
    }

    public void OnImageClick(Image clickedImage)
    {
        if (images[nextImageIndex] == clickedImage)
        {
            CanvasGroup canvasGroup = clickedImage.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1.0f;
            }
            nextImageIndex++;

            if (nextImageIndex >= images.Count)
            {
                Debug.Log("Toutes les images ont été cliquées !");
            }
            else
            {
                SetInteractable(false);
                images[nextImageIndex].GetComponent<CanvasGroup>().interactable = true;
            }
        }
    }

    private void SetInteractable(bool interactable)
    {
        foreach (Image image in images)
        {
            image.GetComponent<CanvasGroup>().interactable = interactable;
        }
    }
}