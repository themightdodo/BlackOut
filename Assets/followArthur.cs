using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followArthur : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject Player;
    public Animator animator;
    Beuverie_PlayerManager pm;
    public Vector3 offset;
    public float BuffDistance;
    PostProcessManager processManager;
    float BaseGreen;
    Color color;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pm = Beuverie_GameManager.GM_instance.playerManager;
        Player = Beuverie_GameManager.GM_instance.playerManager.gameObject;
        processManager = Beuverie_GameManager.GM_instance.GetComponent<PostProcessManager>();
        BaseGreen = processManager.posterisationColor.g;
    }

    private void Update()
    {
        agent.SetDestination(Player.transform.position + offset);
        if (Vector3.Distance(transform.position, Player.transform.position) < BuffDistance)
        {
            animator.Play("Idle");
            if(BaseGreen < 0.3f)
            {
                BaseGreen += 0.001f;
                color =   new Color(0.09019608f, BaseGreen, 0.2901961f);
                BuffArthur();
            }          
            processManager.FPR.passMaterial.SetColor("_Color", color);
            
        }
        else 
        {
            animator.Play("Walk");
            if (BaseGreen > 0.1372549f)
            {
                BaseGreen -= 0.001f;
                color = new Color(0.09019608f, BaseGreen, 0.2901961f);
                DebuffArthur();
            }
            processManager.FPR.passMaterial.SetColor("_Color", color);
            
        }
    }

    void BuffArthur()
    {
        pm.GetComponent<Addiction>().Addiction_timer.CurrentValue += 0.022f;
    }
    void DebuffArthur()
    {
        pm.GetComponent<Addiction>().Addiction_timer.CurrentValue -= 0.012f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, BuffDistance);
    }
}
