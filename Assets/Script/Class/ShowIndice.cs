using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ShowIndice : Node
{
   
    public Indice indice;
    [Output(dynamicPortList = true)]
    public List<Choix> choices;
}
