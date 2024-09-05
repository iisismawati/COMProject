using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "DatabaseKartu", menuName = "Database/Kartu")]
public class KartuSO : ScriptableObject
{
    public List<DBKartu> kartu = new List<DBKartu> ();
}
