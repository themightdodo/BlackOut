using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishing : Item
{
    public GameObject LeurreToInstance;
    GameObject Leurre;
    public Rope Linerenderer;
    public Vector3 Offset;
    CameraMove camera;
    public float MoveRod;
    public int Speed;
    Timer WaitTimer;
    Timer AttackTimer;
    public int RandomValue;
    public float LeurreSpeed;
    public float LeurreHealth;

    public float LigneHealth;
    float timeToWait;
    public float WaitValue;

    public float CatchRadius;

    public List<GameObject> itemToDrop;
    public GameObject itemOnLeurre;
    GameObject ItemChoosen;
    Timer TimeBtwThrow;
    bool gotphone;


    public enum FishingState
    {
        REST,
        THROW,
        WAIT,
        CATCH,
    }

    public enum LeurreState
    {
        Attack,
        Rest,
    }

    public LeurreState leurreState;

    public FishingState _State;

    Invest_PlayerManager pm;


    protected override void Start()
    {
        base.Start();
        Leurre = Instantiate(LeurreToInstance,transform.position+Offset,LeurreToInstance.transform.rotation);
        Linerenderer.endPoint = Leurre.transform;
        Leurre.GetComponent<CharacterJoint>().connectedBody = Linerenderer.GetComponent<Rigidbody>();
        pm =  Invest_GameManager.GM_instance.playerManager;
        camera = pm.Camera.GetComponent<CameraMove>();
        AttackTimer = new Timer(2f);
        TimeBtwThrow = new Timer(0.5f);
    }

    private void ResetLeurre()
    {
        Destroy(Leurre);
        Leurre = Instantiate(LeurreToInstance, transform.position + Offset, LeurreToInstance.transform.rotation);
        Linerenderer.endPoint = Leurre.transform;
        Leurre.GetComponent<CharacterJoint>().connectedBody = Linerenderer.GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        TimeBtwThrow.Refresh();
        if (ItemChoosen != null)
        {
            itemOnLeurre.transform.position = Leurre.transform.position;
        }
        Linerenderer.ropeLength = Vector3.Distance(transform.position, Leurre.transform.position);
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

    int randomNumber()
    {

        float random = Random.Range(0, 1);
        int value = 0;
        if(random <= 0.9f)
        {
            if (!gotphone)
            {
                value = 0;
                gotphone = true;
            }
            else
            {
                value = 1;
            }
           
            return value;
        }
        else
        {
            value = 1;
            return value;
        }
    }
    void Rest_transition()
    {
        ResetLeurre();
        if (ItemChoosen != null)
        {
            itemOnLeurre = Instantiate(itemToDrop[randomNumber()],Leurre.transform.position,Leurre.transform.rotation);
        }
        pm.FinMiniJeu.Invoke();
        _State = FishingState.REST;


    }
    void Throw_transition()
    {
        
        _State = FishingState.THROW;
        Destroy(Leurre.GetComponent<CharacterJoint>());
        Leurre.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, 2, 0))*250);
    }
    void Wait_transition()
    {
        _State = FishingState.WAIT;
        timeToWait = Random.Range(5, 10);
        WaitTimer = new Timer(timeToWait);
        Destroy(Leurre.GetComponent<Rigidbody>());

    }
    void Catch_transition()
    {
        
        LeurreSpeed = Random.Range(5, 10);
        LeurreHealth = Random.Range(20, 30);
        _State = FishingState.CATCH;

    }
    void Rest()
    {

    }
    void Throw()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Leurre.transform.position, 0.1f, Leurre.transform.forward,out hit, 2f, InteractLayer))
        {
            Wait_transition();
            pm.MiniJeu.Invoke();
        }
    }
    void Wait()
    {
        if (Invest_GameManager.GM_instance.GetComponent<InputManager>().Check.Pressed())
        {
            Rest_transition();
            TimeBtwThrow.Reset();
        } 
     
        WaitTimer.Refresh();
        WaitValue = WaitTimer.CurrentValue;
        camera.transform.LookAt(Leurre.transform.position);
        camera.JoyY = Mathf.Clamp(camera.JoyY, -80f, 80f);
        
      
        MoveRod = new Vector3(camera.JoyX, camera.JoyY).magnitude;
        if(MoveRod >= 50)
        {
            Leurre.transform.position = Vector3.MoveTowards(Leurre.transform.position, 
                new Vector3(transform.position.x, Leurre.transform.position.y, transform.position.z), Speed * Time.deltaTime);
        }

        if (WaitTimer.Done())
        {
            Catch_transition();
        }

    }
    void Catch()
    {
        if (Invest_GameManager.GM_instance.GetComponent<InputManager>().Check.Pressed())
        {
            Rest_transition();
            TimeBtwThrow.Reset();
        }
        camera.transform.LookAt(Leurre.transform.position);
        camera.JoyY = Mathf.Clamp(camera.JoyY, -80f, 80f);
        MoveRod = new Vector3(camera.JoyX, camera.JoyY).magnitude;

        AttackTimer.Refresh();
        if (AttackTimer.Done())
        {
            RandomValue = Random.Range(-1, 1);
            AttackTimer.Reset();
        }
        if(LeurreHealth > 0)
        {
            leurreState = LeurreState.Attack;
        }
        else
        {
            leurreState = LeurreState.Rest;
        }
        if(LigneHealth <= 0)
        {
            Rest_transition();
        }
       
        switch (leurreState)
        {
            case LeurreState.Attack:
                LeurreAttack();
                break;
            case LeurreState.Rest:
                LeurreRest();
                break;
        }


    }

    void LeurreRest()
    {
       
        if (MoveRod >= 60)
        {
            Linerenderer.stiffness = 100;
            Leurre.transform.position = Vector3.MoveTowards(Leurre.transform.position,
               new Vector3(transform.position.x, Leurre.transform.position.y, transform.position.z), Speed * 2 * Time.deltaTime);
        }
        else
        {
            Leurre.transform.position = Vector3.MoveTowards(Leurre.transform.position,
                new Vector3(transform.position.x, Leurre.transform.position.y, transform.position.z), Speed  * Time.deltaTime);
            Linerenderer.stiffness = 1;
        }

        if (Vector3.Distance(transform.position, Leurre.transform.position) <= CatchRadius)
        {
            Debug.Log("ON EST GOOD");
            ItemChoosen = itemToDrop[0];
            Rest_transition();

        }
    }

    void LeurreAttack()
    {
        if (MoveRod >= 60)
        {
            Linerenderer.stiffness = 1;
            LeurreHealth -= Time.deltaTime;
            if(LeurreHealth > 10)
            {
                if (RandomValue == 0)
                {
                    Leurre.transform.position += transform.right  * Time.deltaTime;
                }
                else
                {
                    Leurre.transform.position -= transform.right  * Time.deltaTime;
                }
            }
            else
            {
                Leurre.transform.position = Vector3.MoveTowards(Leurre.transform.position,
                new Vector3(transform.position.x, Leurre.transform.position.y, transform.position.z), Speed/2 * Time.deltaTime);
            }
            
            
        }
        else
        {
            Linerenderer.stiffness = 1000;
            LigneHealth -= Time.deltaTime;
            LeurreHealth -= Time.deltaTime;
            if (RandomValue == 0)
            {
                Leurre.transform.position += transform.right * LeurreSpeed * Time.deltaTime;
            }
            else
            {
                Leurre.transform.position -= transform.right * LeurreSpeed * Time.deltaTime;
            }

        }

    }
    protected override void Idle()
    {
        base.Idle();
        
    }
    
    protected override void Action()
    {
        base.Action();
        if(_State == FishingState.REST&& TimeBtwThrow.Done()&&itemOnLeurre==null)
        {
            Throw_transition();
        }
        else if (itemOnLeurre != null)
        {
            itemOnLeurre = null;
            ItemChoosen = null;
            TimeBtwThrow.Reset();
        }
            
        

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Offset, 0.2f);
    }
}
