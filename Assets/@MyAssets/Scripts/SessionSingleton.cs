using UnityEngine;

public class SessionSingleton : MonoBehaviour
{
    public static SessionSingleton instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
