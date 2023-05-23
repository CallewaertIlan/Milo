using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBar : MonoBehaviour
{
    [SerializeField] private GameObject HUDPrefab;
    private GameObject HUD;
    private Recoltable rec;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.Instantiate(HUDPrefab, transform);
        HUD.transform.position = transform.position;
        rec = HUD.GetComponentInParent<Recoltable>();
    }

    private void Update()
    {
        float pourcentProgress = rec.progressTimeCollect * 100 / rec.timeToCollect;

        HUD.GetComponent<HarvestBar>().EditSize(pourcentProgress);
    }
}
