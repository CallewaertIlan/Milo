using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    [SerializeField] private Canvas fastTravelCanvas;
    [SerializeField] private GameObject progressBar;
    public FastTravelPlace placeToTravel;

    [SerializeField] private float timeBeforeTp;
    [SerializeField] private float timeAfterTp;

    [SerializeField] private Material dissapearMaterial;
    private Material baseMaterial;

    private Renderer rendererFastTravel;
    private MaterialPropertyBlock propertyBlock;

    private float timeStartTp;
    private float timeWasTp;
    private MovePlayer moveScript;
    private OpenMap openMapScript;

    private bool isTp;

    private bool wasTp;

    // Start is called before the first frame update
    void Start()
    {
        isTp = false;
        wasTp = false;
        moveScript = GetComponent<MovePlayer>();
        openMapScript = GetComponent<OpenMap>();

        // R�cup�rer le Renderer attach� � l'objet
        rendererFastTravel = GetComponent<Renderer>();
        baseMaterial = rendererFastTravel.material;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (isTp)
        {
            if (moveScript.IsMoving())
            {
                ActiveTp(false);
                wasTp = false;
            }

            Travel();
            RefreshBar();
            SetShaderAnimation((Time.time - timeStartTp) / timeBeforeTp);
        }
        else if (wasTp)
        {
            Debug.Log((Time.time - timeWasTp) / timeAfterTp);
            SetShaderAnimation(1 - ((Time.time - timeWasTp) / timeAfterTp));

            if (Time.time - timeWasTp > timeAfterTp) wasTp = false;
        }
        else
        {
            rendererFastTravel.material = baseMaterial;
        }
    }

    private void Travel()
    {
        if (timeStartTp + timeBeforeTp <= Time.time)
        {
            transform.position = placeToTravel.fastTravelPosition;
            fastTravelCanvas.gameObject.SetActive(false);
            ActiveTp(false);
            wasTp = true;
            timeWasTp = Time.time;
            placeToTravel = null;
        }
    }

    private void ActiveTp(bool tp)
    {
        isTp = tp;
        fastTravelCanvas.gameObject.SetActive(tp);
    }

    private void RefreshBar()
    {
        progressBar.transform.localScale = new Vector3(
            (Time.time - timeStartTp) * 10 / timeBeforeTp,
            progressBar.transform.localScale.y,
            progressBar.transform.localScale.z
            );
    }

    private void SetShaderAnimation(float progress)
    {
        rendererFastTravel.material = dissapearMaterial;

        // Cr�er un nouveau MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();

        // R�cup�rer l'ID de la propri�t� "Intensity" du shader graph
        int progressID = Shader.PropertyToID("_Progress");
        int baseColorID = Shader.PropertyToID("_Base_Color");

        propertyBlock.SetColor(baseColorID, baseMaterial.color);
        propertyBlock.SetFloat(progressID, progress);
        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
    }

    public void ButtonPressTravel()
    {
        openMapScript.OpenTheMap(false);
        ActiveTp(true);
        timeStartTp = Time.time;
        wasTp = false;
    }
}
