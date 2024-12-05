using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class TimeInOtherDimension : MonoBehaviour
{
    [SerializeField] private float maxTime; //en segundos
    
    private GameObject timeGUI;

    private float chrono = 0;
    private string timeText;
    private int minutes;
    private int seconds;
    // Start is called before the first frame update
    void Start()
    {
        timeGUI = GameObject.Find("XR Origin (XR Rig)");
    }

    // Update is called once per frame
    void Update()
    {
        chrono += Time.deltaTime;
        ManageTime();
        if(minutes == 0 && seconds == 0)
        {
            SceneManager.LoadScene("AR");
        }
    }

    public void ManageTime()
    {
        minutes = (int) ((maxTime - chrono) / 60);
        seconds = (int) ((maxTime - chrono) % 60);
        Debug.Log("Minutes: " + minutes + " Seconds: " + seconds);
        string secondsString = seconds.ToString();
        if (secondsString.Length == 1)
        {
            secondsString = "0" + secondsString;
        }
        timeText = minutes + ":" + secondsString;

        if (seconds <= 10)
        {
            timeGUI.GetComponent<AudioSource>().Play();
        }

        if (seconds <= 10 && (seconds % 2) == 0 )
        {
            timeGUI.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            timeGUI.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        timeGUI.GetComponentInChildren<TextMeshProUGUI>().text = timeText;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        chrono = 0;
    }
}
