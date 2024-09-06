using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kartu Database", menuName = "Database/Kartu")]
public class KartuSO : ScriptableObject
{
    public List<DBKartunya> KartuList = new List<DBKartunya>();
}
