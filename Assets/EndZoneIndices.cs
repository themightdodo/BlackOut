using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneIndices : MonoBehaviour
{
    public List<IndiceItem> IndicesToHave;
    public string NextCinematic;
    public string AltNextCinematic;
    public bool noPopup;
    int IndicesNumber;
    int IndicesHaven;

    Invest_Inventory inventory;

    private void Start()
    {
        IndicesNumber = IndicesToHave.Count;
        inventory = Invest_GameManager.GM_instance.playerManager.GetComponent<Invest_Inventory>();

    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in IndicesToHave)
        {
            if (inventory.CurrentIndices.Contains(item))
            {
                IndicesHaven++;
            }
        }
        if(IndicesHaven == IndicesNumber)
        {
            Invest_GameManager.GM_instance.NextSceneWin = NextCinematic;
            Invest_GameManager.GM_instance.InvestigationDone.Invoke(null, null, Invest_GameManager.GM_instance.NextSceneWin, noPopup);
        }
        else
        {
            Invest_GameManager.GM_instance.NextSceneWin = AltNextCinematic;
            Invest_GameManager.GM_instance.InvestigationDone.Invoke(null, null, Invest_GameManager.GM_instance.NextSceneWin, noPopup);
        }
    }
}
