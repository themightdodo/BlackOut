using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishing : Item
{
    public GameObject LeurreToInstance;
    GameObject Leurre;
    public Rope LineRenderer;
    public Vector3 Offset;
    GameObject camera;

    public enum FishingState
    {
        REST,
        THROW,
        WAIT,
        CATCH,
    }

    public FishingState _State;

    Invest_PlayerManager pm;

    protected override void Start()
    {
        base.Start();
        Leurre = Instantiate(LeurreToInstance,transform.position+Offset,LeurreToInstance.transform.rotation);
        LineRenderer.endPoint = Leurre.transform;
        Leurre.GetComponent<CharacterJoint>().connectedBody = LineRenderer.GetComponent<Rigidbody>();
        pm =  Invest_GameManager.GM_instance.playerManager;
        camera = pm.Camera;
    }

    protected override void Update()
    {
        base.Update();
        switch (_State)
        {
            case (FishingState.REST):
                Rest();
                break;
            case (FishingState.THROW):
                Throw();
                break;
            case (FishingState.WAIT):
                Wait();
                break;
            case (FishingState.CATCH):
                Catch();
                break;
        }
    }
    void Rest_transition()
    {
        _State = FishingState.REST;
    }
    void Throw_transition()
    {
        _State = FishingState.THROW;
        Destroy(Leurre.GetComponent<CharacterJoint>());
        Leurre.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, 2, 0))*10);
    }
    void Wait_transition()
    {
        _State = FishingState.WAIT;
        Destroy(Leurre.GetComponent<Rigidbody>());
    }
    void Catch_transition()
    {
        _State = FishingState.CATCH;
    }
    void Rest()
    {

    }
    void Throw()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Leurre.transform.position, 0.5f, Leurre.transform.forward,out hit, 2f, InteractLayer))
        {
            Debug.Log("DE LO");
            Wait_transition();
            pm.MiniJeu.Invoke();
        }
    }
    void Wait()
    {
        camera.transform.LookAt(Leurre.transform.position);
    }
    void Catch()
    {

    }

    protected override void Idle()
    {
        base.Idle();
        
    }

    protected override void Action()
    {
        base.Action();
       
        Throw_transition();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Offset, 0.2f);
    }
}
