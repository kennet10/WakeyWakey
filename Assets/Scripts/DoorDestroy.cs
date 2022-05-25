using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    [SerializeField] public List<string> m_LeversTrue = new List<string>();
    [SerializeField] public List<string> m_LeversFalse = new List<string>();

    private GameObject LeverT1, LeverT2, key, LeverF1;
    private Interaction lever_it1, lever_it2, lever_if1;


    // Start is called before the first frame update
    void Start()
    {
        foreach (string key in m_LeversTrue)
        {
            if (LeverT1 == null)
            {
                LeverT1 = GameObject.Find(key);
                lever_it1 = LeverT1.GetComponent<Interaction>();
            }
            else if (LeverT2 == null)
            {
                LeverT2 = GameObject.Find(key);
                lever_it2 = LeverT2.GetComponent<Interaction>();
            }
        }

        if (m_LeversFalse.Count != 0)
        {
            foreach (string nkey in m_LeversFalse)
            {
                LeverF1 = GameObject.Find(nkey);
                lever_if1 = LeverF1.GetComponent<Interaction>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LeverT2 != null)
        {
            if (lever_it1.lever_trigger == true && lever_it2.lever_trigger == true && lever_if1.lever_trigger == false)
            {
                Debug.Log("Door has been destroyed");
                Destroy(gameObject);

            }
        }
        else
        {
            if (lever_it1.lever_trigger == true)
            {
                Debug.Log("Door has been destroyed");
                Destroy(gameObject);

            }
        }
    }
}