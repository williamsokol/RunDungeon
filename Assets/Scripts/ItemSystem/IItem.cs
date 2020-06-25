using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
    void UseItem(Invertory inventory);
    void HideItem(Invertory inventory);
}
