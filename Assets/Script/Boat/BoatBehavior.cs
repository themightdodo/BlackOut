using UnityEngine;
using System.Collections;
using PhysicsHelper;
using Unity.VisualScripting;


public class BoatBehavior : MonoBehaviour
{
    public Transform Motor;
    [Header("Direction power")]
    public float SteerPower = 500F;
    public float Power = 5f;
    public float MaxSpeed = 10f;
    public InputManager InputManager;

    protected Rigidbody rb;
    protected Quaternion StartRotation;
    public ParticleSystem ParticleSystem;
    protected Camera Camera;

    protected Vector3 CamVel;
    public float DeadZone = 0.8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
        Camera = Camera.main;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        var forceDirection = transform.forward;
        var steer = 0;

        //steer direction [-1,0,1]
        //LStick pour les directions;
        //LStick.X = Gauche où droite
        if (InputManager.Lstick.x <= -DeadZone)
        {
            steer = -1;
        }
        else if (InputManager.Lstick.x >= DeadZone)
        {
            steer = 1;
        }

      

        //Compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var targetVel = Vector3.zero;       
        
        //Rotational Force
        rb.AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);
        
        //Avancer/reculer
        if (InputManager.Lstick.z <= -DeadZone)
        {
            PhysicsHelper.PhysicsHelper.ApplyForceToReachVelocity(rb, forward * -MaxSpeed, Power);
        }
        if (InputManager.Lstick.z >= DeadZone)
        {
            PhysicsHelper.PhysicsHelper.ApplyForceToReachVelocity(rb, forward * MaxSpeed , Power);
        }

        // Particle System
        if (ParticleSystem != null)
        {
            if (InputManager.Lstick.z <= -DeadZone || InputManager.Lstick.z >= DeadZone)
            {
                ParticleSystem.Play();
            }
            else
            {
                ParticleSystem.Pause();
            }

        }
        else
        {
           
        }
    }
}
