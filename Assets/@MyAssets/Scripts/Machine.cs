using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeSceneCoroutine());
        }
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Credits");
    }
}
