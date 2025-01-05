using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private List<string> credits = new List<string>();

    [SerializeField] private float timeBetweenCredits = 2f;
    [SerializeField] private float timeBetweenLetters = 0.05f;
    [SerializeField] private float timeShowingText = 0.05f;
    [SerializeField] private float timeBetweenLettersDelete = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreditsCoroutine());
    }

    private IEnumerator CreditsCoroutine()
    {
        for (int j = 0; j < credits.Count - 1; j++)
        {
            string credit = credits[j];
            for (int i = 0; i < credit.Length; i++)
            {
                tmp.text += credit[i];
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            yield return new WaitForSeconds(timeShowingText);
            for (int i = 0; i <= credit.Length; i++)
            {
                tmp.text = credit.Substring(0, credit.Length - i);
                yield return new WaitForSeconds(timeBetweenLettersDelete);
            }
            yield return new WaitForSeconds(timeBetweenCredits);
        }
        string credit2 = credits[credits.Count - 1];
        for (int i = 0; i < credit2.Length; i++)
        {
            tmp.text += credit2[i];
            yield return new WaitForSeconds(timeBetweenLetters);
        }

    }
}
