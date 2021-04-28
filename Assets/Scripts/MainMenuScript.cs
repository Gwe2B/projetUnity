using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    public void ReportBug() {
        Application.OpenURL("https://github.com/Gwe2B/projetUnity/issues");
    }

    public void StartGame() {
        SceneManager.LoadScene("Stage1");
    }
}
