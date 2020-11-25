using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private int id { get;}
    private string mail { get; set; }
    private string password { get; set; }
    private int height { get; set; }
    private int weight { get; set; }
    private int maxFC { get; set; }
    private int maxW { get; set; }
    //private ArrayList<Workout>;

    public User(int id, string mail, string password, int height, int weight, int maxFC, int maxW)
    {
        this.id = id;
        this.mail = mail;
        this.password = password;
        this.height = height;
        this.weight = weight;
        this.maxFC = maxFC;
        this.maxW = maxW;
    }
    
    public string toString() 
    {
        return string.Format("id: {0}, email: {1}, pass: {2}, height: {3}, weight: {4}, maxFC: {5}, maxW: {6} ", id, mail, password, height, weight, maxFC, maxW);
    }
}
