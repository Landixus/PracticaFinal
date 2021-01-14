using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc 
{
    public int numBloc { get; }
    public int pot { get; set; }
    public int temps { get; set; }

    public Bloc(int numBloc)
    {
        this.numBloc = numBloc;
    }
}
