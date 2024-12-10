using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class TimeInOtherDimension : MonoBehaviour
{
    [SerializeField] private float maxTime; //en segundos
    [SerializeField] private TextMeshProUGUI tmp;

    private float chrono = 0;
    public string timeText { get; set; }
    private int minutes;
    private int seconds;

    private AudioSource watchBeep;
    // Start is called before the first frame update
    void Start()
    {
        //watchBeep = xrOrigin.GetComponent<AudioSource>();
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

        if (minutes == 0 && seconds == 11) //Esto igual pasar al reloj
        {
            Debug.Log(watchBeep);
            watchBeep.Play();
        }

        if (minutes == 0 && seconds <= 10 && (seconds % 2) == 0 )
        {
            tmp.color = Color.red;
        }
        else
        {
            tmp.color = Color.white;
        }
        tmp.text = timeText;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        chrono = 0;
    }
}
