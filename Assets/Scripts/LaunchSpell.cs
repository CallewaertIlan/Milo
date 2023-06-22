using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaunchSpell : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject cameraObject;

    private Entity entityScript;

    // Start is called before the first frame update
    void Start()
    {
        entityScript = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartSpell();
    }
    void StartSpell()
    {
        if (spellPrefab.GetComponent<Spell>().manaCost < entityScript.mana)
        {
            GameObject spell = Instantiate(spellPrefab, new Vector3(transform.position.x + cameraObject.transform.forward.x, transform.position.y + cameraObject.transform.forward.y, transform.position.z + transform.forward.z), transform.rotation);
            Spell spellScript = spell.GetComponent<Spell>();
            spellScript.SetDirection(cameraObject.transform.forward);
            entityScript.AddMana(-spellScript.manaCost);
            entityScript.SetTimeLastManaUse(Time.time);
        }
        else
        {
            Debug.Log(spellPrefab.GetComponent<Spell>().manaCost);
            Debug.Log(entityScript.mana);
        }
    }
}