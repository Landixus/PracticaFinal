
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
 

public class BBDD : MonoBehaviour
{
    //La connexio pot ser statica ja que sempre sera la mateixa per totes les connexions
    public string conn = "URI=file:" + Application.dataPath + "/baseDadesSQLitle.db"; //Path to database.
    public void SelectTest() 
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT ID_User,mail, password, height, weight, maxFC, maxW  " + "FROM user";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string mail = reader.GetString(1);
            string password = reader.GetString(2);
            int height = reader.GetInt32(3);
            int weight = reader.GetInt32(4);
            int maxFC = reader.GetInt32(5);
            int maxW = reader.GetInt32(6);

            Debug.Log("id= " + id + "  email=" + mail + "  password=" + password + "  height=" + height + "  weight=" + weight + "  maxFC=" + maxFC + "  maxW=" + maxW);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }


    public void addUser() {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO cars(ID_User,mail, password, height, weight, maxFC, maxW) VALUES(, @price)";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        cmd.CommandText = "INSERT INTO cars(name, price) VALUES(@name, @price)";

        cmd.Parameters.AddWithValue("@name", "BMW");
        cmd.Parameters.AddWithValue("@price", 36600);
        cmd.Prepare();

        cmd.ExecuteNonQuery();

        Console.WriteLine("row inserted");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
