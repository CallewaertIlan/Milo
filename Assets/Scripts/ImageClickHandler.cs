using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour, IPointerClickHandler
{
    private Skills skills;

    private void Start()
    {
        skills = transform.parent.GetComponent<Skills>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Image clickedImage = GetComponent<Image>();
        skills.OnImageClick(clickedImage);
    }
}