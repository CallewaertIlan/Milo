using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBar : MonoBehaviour
{
    [SerializeField] private GameObject HUDPrefab;
    [SerializeField] private GameObject characterCamera;
    private GameObject HUD;
    private Recoltable rec;
    private HarvestBar harvestBar;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Instantiate(HUDPrefab, transform);
        HUD.transform.position = transform.position;
        rec = HUD.GetComponentInParent<Recoltable>();
        harvestBar = HUD.GetComponent<HarvestBar>();
    }

    private void Update()
    {
        float pourcentProgress = rec.progressTimeCollect * 100 / rec.timeToCollect;

        // affiche la bar de chargement seulement si elle n'est pas égale a 0 
        if (pourcentProgress == 0) HUD.gameObject.SetActive(false);
        else HUD.gameObject.SetActive(true);

        // change la taille de la bar 
        harvestBar.EditSize(pourcentProgress);

        // mets la bar dans la direction de la caméra
        HUD.transform.LookAt(characterCamera.transform.position);
    }
}
