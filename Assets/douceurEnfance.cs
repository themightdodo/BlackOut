using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class douceurEnfance : Item
{

    Invest_GameManager gm;
    InvestEyes eyes;

    protected override void Start()
    {
        base.Start();
        gm = Invest_GameManager.GM_instance;
        eyes = gm.GetComponent<InvestEyes>();
    }



    protected override void Action()
    {
        base.Action();
        eyes.InstanciateEyes();
        Destroy(gameObject);
    }
}
