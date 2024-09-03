using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartuManager : MonoBehaviour
{
    [SerializeField]
    private KartuSO kartuDatabase;

    private List<DBKartu> kartu = new List<DBKartu> ();

    private void Start()
    {
        for (int i = 0; i < kartuDatabase.kartu.Count; i++)
        {
            kartu.Add(kartuDatabase.kartu[i]);
        }
    }
}
