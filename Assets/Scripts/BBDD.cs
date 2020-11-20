
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


    public void insertUser(String mail, String password, int height, int weight) {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();
        string insertUserQuery = "INSERT INTO User(ID_User,mail, password, height, weight, maxFC, maxW) VALUES(null, '"+ mail +"','"+ password+"'," + height+","+weight+","+100+","+250+");";

        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = insertUserQuery;

        try
        {
            IDataReader reader = dbcmd.ExecuteReader();
            Console.WriteLine("row inserted");
            reader.Close();
            reader = null;
        }
        catch (Exception ex) {
            if (ex.Message.Contains("UniqueConstraint"))
            {
                Debug.Log("Email repetit " + ex.Message);
            }
            else {
                Debug.Log("ERROR desconegut " + ex.Message);
            }
        }
        
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
