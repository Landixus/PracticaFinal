using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc 
{
    private int numBloc { get; }
    private int pot { get; set; }
    private float temps { get; set; }

    public Bloc(int numBloc, int pot, float temps)
    {
        this.numBloc = numBloc;
        this.pot = pot;
        this.temps = temps;
    }
}
