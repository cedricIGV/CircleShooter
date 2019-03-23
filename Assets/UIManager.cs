using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject startScreen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(false);
        startScreen.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }


	public void pauseControl(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
	}

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        pauseScreen.SetActive(false);
    }

    public void showPaused()
    {
        pauseScreen.SetActive(true);
    }

    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void hideStart()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void showStart()
    {
        startScreen.SetActive(true);
    }
}
