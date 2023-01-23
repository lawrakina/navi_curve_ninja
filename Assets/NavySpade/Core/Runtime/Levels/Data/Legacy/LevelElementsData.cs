using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: чет с этим старым генератором раннеров сделать
[Serializable]
public class LevelElementsData 
{
    public bool randomOrder = true;
    public GameElement startElement = default;
    public GameElement finishElement = default;
    public List<GameElement> gameElements = new List<GameElement>();
}
