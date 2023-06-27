using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CollectBar : MonoBehaviour
{
    [SerializeField] private GameObject HUDPrefab;
    [SerializeField] private GameObject characterCamera;
    private GameObject HUD;
    private Recoltables rec;
    private HarvestBar harvestBar;
    private MovePlayer movePlayer;
    private bool playerIsMoving;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Instantiate(HUDPrefab, transform);
        HUD.transform.position = transform.position;
        rec = HUD.GetComponentInParent<Recoltables>();
        harvestBar = HUD.GetComponent<HarvestBar>();
        movePlayer = FindObjectOfType<MovePlayer>();
        characterCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        float pourcentProgress = rec.progressTimeCollect * 100 / rec.timeToCollect;

        if (pourcentProgress > 0 && !movePlayer.IsMoving())
        {
            HUD.gameObject.SetActive(true);
            // Change la taille de la barre
            harvestBar.EditSize(pourcentProgress);
        }
        else
        {
            HUD.gameObject.SetActive(false);
            if (movePlayer.IsMoving())
            {
                rec.CancelCollect(); // Annule la récolte en cours
            }
        }

        // Oriente la barre dans la direction de la caméra
        HUD.transform.LookAt(characterCamera.transform.position);
    }

    public bool IsMoving()
    {
        return playerIsMoving;
    }

    public void PlayerIsMoving(bool isMoving)
    {
        playerIsMoving = isMoving;
    }
}