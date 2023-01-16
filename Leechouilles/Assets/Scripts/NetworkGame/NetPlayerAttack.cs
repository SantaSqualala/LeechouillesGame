using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using Unity.VisualScripting;

public class NetPlayerAttack : NetworkBehaviour
{
    private NetInputHandler netInputHandler;
    private Camera cam;
    private bool canAttack = true;

    [SerializeField] private int damage = 1;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private LayerMask attackMask;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
        {
            Destroy(GetComponent<NetPlayerAttack>());
            return;
        }
    }

    private void Start()
    {
        netInputHandler = GetComponent<NetInputHandler>();
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!base.IsOwner)
        {
            return;
        }

        if(canAttack)
        {
            if(netInputHandler.inputActionL())
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if(Physics.SphereCast(cam.transform.position, 0.2f, cam.transform.forward, out RaycastHit hitInfo, 1f, attackMask))
        {
            ServerHitPlayer(hitInfo.transform.gameObject);
        }

        StartCoroutine(AttackDelay(attackDelay));
        print("Player attack");
    }

    [ServerRpc(RequireOwnership = false)]
    void ServerHitPlayer(GameObject player)
    {
        NetPlayerBehaviour.instance.DamagePlayer(player.GetInstanceID(), damage, gameObject.GetInstanceID());
    }

    IEnumerator AttackDelay(float timer)
    {
        canAttack = false;
        yield return new WaitForSeconds(timer);
        canAttack = true;
    }
}
