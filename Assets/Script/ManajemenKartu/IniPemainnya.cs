using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IniPemainnya : MonoBehaviour
{
    [SerializeField] private PemainSO playerDatabase;  // Referensi ke ScriptableObject database player

    [SerializeField] private int playerIndex;  // Index pemain di dalam database (0 untuk player1, 1 untuk player2, dst.)

    public string Jurusan;
    //public string JenisKelamin;

    [SerializeField] private TMP_Text jurusanText;
    //[SerializeField] private TMP_Text jenisKelaminText;

    // Start is called before the first frame update
    void Start()
    {
        // Assign nilai berdasarkan playerIndex ke player
        if (playerIndex < playerDatabase.PlayerList.Count)
        {
            Jurusan = playerDatabase.PlayerList[playerIndex].Jurusan;
            //JenisKelamin = playerDatabase.PlayerList[playerIndex].JenisKelamin;

            // Menampilkan data di UI, jika diperlukan
            if (jurusanText != null)
                jurusanText.text = "Jurusan: " + Jurusan;
            //if (jenisKelaminText != null)
            //    jenisKelaminText.text = "Jenis Kelamin: " + JenisKelamin;
        }
        else
        {
            Debug.LogError("Player index out of range");
        }
    }
}
