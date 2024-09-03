using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListKartu : MonoBehaviour
{
    [SerializeField]
    private List<DBKartu> kartu = new List<DBKartu> ();

    public int indexKartu;

    private void Start()
    {
        indexKartu = 0;
        Debug.Log(kartu.Count);
    }
}
