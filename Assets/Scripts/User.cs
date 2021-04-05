using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int id { get;}
    public string mail { get; set; }
    public string password { get; set; }
    public int height { get; set; }
    public int weight { get; set; }
    public int maxFC { get; set; }
    public int maxW { get; set; }

    //Per guardar els workouts creats
    public List<Workout> workouts;

    //Historial de rutas
    public List<Session> historial;

    public User(int id, string mail, string password, int height, int weight, int maxFC, int maxW)
    {
        this.id = id;
        this.mail = mail;
        this.password = password;
        this.height = height;
        this.weight = weight;
        this.maxFC = maxFC;
        this.maxW = maxW;
        workouts = new List<Workout>();
        historial = new List<Session>();
    }
    
    public string toString() 
    {
        return string.Format("id: {0}, email: {1}, pass: {2}, height: {3}, weight: {4}, maxFC: {5}, maxW: {6} ", id, mail, password, height, weight, maxFC, maxW);
    }

    public void AfegirWorkout(Workout workout)
    {
        workouts.Add(workout);
    }

    public void EnsenyarWorkoutsLog()
    {
        foreach (var workout in workouts)
        {
            Debug.Log(workout.ToString());
        }
    }

    public string getMail() {
        return mail;
    }
}
