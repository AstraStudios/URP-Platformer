using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] TMP_Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
