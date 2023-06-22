using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFastTravel : MonoBehaviour
{
    [SerializeField] private FastTravelPlace fastTravelPlace;
    [SerializeField] private GameObject player;

    [SerializeField] private Sprite positionSelected;
    [SerializeField] private Sprite positionUnselected;

    private FastTravel fastTravel;
    private Image img;

    private void Start()
    {
        fastTravel = player.GetComponent<FastTravel>();
        img = GetComponent<Image>();
    }

    private void Update()
    {
        if (fastTravel.GetFastTravelPlace() == fastTravelPlace) img.sprite = positionSelected;
        else img.sprite = positionUnselected;
    }

    public void SelectFastTravel()
    {
        fastTravel.SetFastTravelPlace(fastTravelPlace);
    }
}
