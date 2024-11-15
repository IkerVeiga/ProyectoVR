using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeInOtherDimension : MonoBehaviour
{
    [SerializeField] private float maxTime; //en segundos
    [SerializeField] private TextMeshProUGUI timeGUI;

    private float chrono = 0;
    private string timeText;
    private int minutes;
    private int seconds;
    // Start is called before the first frame update
    void Start()
    {
        
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
        string secondsString = seconds.ToString();
        if (secondsString.Length == 1)
        {
            secondsString = "0" + secondsString;
        }
        timeText = minutes + ":" + secondsString;
        timeGUI.text = timeText;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        chrono = 0;
    }
}
