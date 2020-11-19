using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class BBDD : MonoBehaviour
{

    string cs = @"server=localhost;userid=root;password=;database=PracticaFinal";

    // Start is called before the first frame update
    void Start()
    {
        using var con = new MySqlConnection(cs);
        con.Open();

        Console.WriteLine($"MySQL version : {con.ServerVersion}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
