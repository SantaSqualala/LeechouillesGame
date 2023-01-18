using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVictoryManager : MonoBehaviour
{
    [SerializeField] private Image hunterWinImg;
    [SerializeField] private Image alienWinImg;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float timeLimit = 500f;

    private List<AlienMovementBehaviour> aliens = new List<AlienMovementBehaviour>();
    private bool hunterWin = false;


    private void Start()
    {
        StartCoroutine(GameDuration());
    }

    // Update is called once per frame
    void Update()
    {
        if(hunterWin)
        {
            HunterWin();
        }

        timeLimit -= Time.deltaTime;
        timeText.text = "Time remaining : " + timeLimit;
    }

    public void AlienDead(GameObject alien)
    {
        Debug.Log("Alien " + alien + " is dead");

        aliens.Remove(alien.GetComponent<AlienMovementBehaviour>());
        if (aliens.Count <= 0)
        {
            hunterWin = true;
        }
    }

    private IEnumerator GameDuration()
    {
        yield return new WaitForSeconds(timeLimit);
        if(!hunterWin)
        {
            AliensWin();
        }
    }

    private void AliensWin()
    {
        alienWinImg.gameObject.SetActive(true);
    }

    private void HunterWin()
    {
        hunterWinImg.gameObject.SetActive(true);
    }
}
