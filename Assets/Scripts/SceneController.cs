using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public void ToStart()
    {
        SceneManager.LoadScene("Start");
    }

    public void ToStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void ToStage(string inStageName)
    {
        SceneManager.LoadScene(inStageName);
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        // WebPlayerの際の処理を記述
		//Application.OpenURL("http://www.yahoo.co.jp/");
#else
		Application.Quit();
#endif
    }
}
