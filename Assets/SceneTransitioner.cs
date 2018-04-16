using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {

    private GameObject canvas = null;
    private CanvasFader fader = null;

	// Use this for initialization
	void Start () {
        this.canvas = this.transform.Find("Canvas").gameObject;
        this.fader = this.canvas.GetComponent<CanvasFader>();

        this.fader.FadeOut(() => {
            this.canvas.SetActive(false);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneTransition(string sceneName)
    {
        this.canvas.SetActive(true);
        this.fader.FadeIn(() => {
            SceneManager.LoadScene(sceneName);
        });
    }
}
