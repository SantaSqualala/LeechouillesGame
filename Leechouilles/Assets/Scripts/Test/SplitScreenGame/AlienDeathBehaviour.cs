using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDeathBehaviour : MonoBehaviour
{
    [SerializeField] private AlienMovementBehaviour move;
    [SerializeField] private AlienLifeBehaviour life;

    public void Death()
    {
        move.Death();
        life.Death();
    }
}
