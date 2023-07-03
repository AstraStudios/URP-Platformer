using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickMainMenuFix : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject selectPanel;

    public bool speedRunMode = false;
    [SerializeField] TMP_Text speedrunCurr; // current speed run mode

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        selectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        speedrunCurr.text = speedRunMode.ToString();
    }

    public void SetSpeedRunMode()
    {
        speedRunMode = !speedRunMode;
    }
}
