using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workout 
{
    public int id { get; set; }
    public float tempsTotal { get; set; }

    public List<Bloc> blocs { get; set; }
  
    public string description { get; set; }

    public Workout(int id)
    {
        this.id = id;
        blocs = new List<Bloc>();
    }

    public void AddBloc(Bloc bloc)
    {
        blocs.Add(bloc);
    }

    public void RemoveLastBloc()
    {
        blocs.RemoveAt(blocs.Count - 1);
    }
}


