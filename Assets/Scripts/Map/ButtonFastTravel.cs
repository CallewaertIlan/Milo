using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFastTravel : MonoBehaviour
{
    [SerializeField] private FastTravelPlace fastTravelPlace;
    [SerializeField] private GameObject player;

    private FastTravel fastTravel;

    private void Start()
    {
        fastTravel = player.GetComponent<FastTravel>();
    }

    public void Travel()
    {
        fastTravel.placeToTravel = fastTravelPlace;
        fastTravel.ButtonPressTravel();
    }
}
