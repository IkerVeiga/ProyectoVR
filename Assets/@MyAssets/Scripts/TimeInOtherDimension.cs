using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class TimeInOtherDimension : MonoBehaviour
{
    [SerializeField] private float maxTime; //en segundos

    private GameObject xrOrigin;

    private float chrono = 0;
    private string timeText;
    private int minutes;
    private int seconds;

    private AudioSource watchBeep;
    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GameObject.Find("XR Origin (XR Rig)");
        watchBeep = xrOrigin.GetComponent<AudioSource>();
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
        //Debug.Log("Minutes: " + minutes + " Seconds: " + seconds);
        string secondsString = seconds.ToString();
        if (secondsString.Length == 1)
        {
            secondsString = "0" + secondsString;
        }
        timeText = minutes + ":" + secondsString;

        if (minutes == 0 && seconds == 11)
        {
            Debug.Log(watchBeep);
            watchBeep.Play();
        }

        if (minutes == 0 && seconds <= 10 && (seconds % 2) == 0 )
        {
            xrOrigin.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            xrOrigin.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        xrOrigin.GetComponentInChildren<TextMeshProUGUI>().text = timeText;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        chrono = 0;
    }
}
