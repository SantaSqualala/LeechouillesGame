using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private List<AlienMovementBehaviour> aliens = new List<AlienMovementBehaviour>();
    [SerializeField] private bool hunterWin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aliens.Count <= 0)
        {
            hunterWin = true;
        }
    }
}
