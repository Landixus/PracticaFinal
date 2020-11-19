
using UnityEngine;
using System;
using MySql.Data.MySqlClient;

public class BBDD : MonoBehaviour
{
    

    // Start is called before the first frame update
    public void test()
    {
        string connStr = "server=localhost;user=root;database=practicafinaltest;port=3306;password=root";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM users";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int r = Convert.ToInt32(result);
                Console.WriteLine("Number of countries in the world database is: " + r);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();
        Debug.Log("Done.");
    }

// Update is called once per frame
void Update()
    {
        
    }
}
