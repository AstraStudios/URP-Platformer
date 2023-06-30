using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMainMenuFix : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject selectPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
