using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workout 
{
    private int id { get; }
    private float tempsTotal { get; set; }

    private List<Bloc> blocs { get; set; }
  
    private string description { get; set; }

    public Workout(int id)
    {
        this.id = id;
    }
}


