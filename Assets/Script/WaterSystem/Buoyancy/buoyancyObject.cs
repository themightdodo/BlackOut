using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class buoyancyObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underWaterDrag = 3f;

    public float underWaterAngularDrag = 1f;

    public float airDrag = 0f;

    public float airAngularDrag = 0f;

    public float floatingPower = 15f;

    public float waterHeight = 0f;

    Rigidbody m_rigidbody;

    int floatersUnderWater;

    bool underwater;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < floaters.Length; i++)
        {
            floatersUnderWater = 0;
            float difference = floaters[i].position.y - OceanManager.Instance.WaterHeightAtPosition(floaters[i].position);

            if(difference < 0)
            {
                m_rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderWater += 1;
                if(!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }

            }
           
            if(underwater && floatersUnderWater == 0)
            {
                underwater = false;
                SwitchState(false);
            }
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (underwater)
        {
        m_rigidbody.drag = underWaterDrag;
        m_rigidbody.angularDrag = underWaterAngularDrag;

        }
        else
        {
            m_rigidbody.drag = airDrag;
            m_rigidbody.angularDrag= airAngularDrag;
        }
    }
}
