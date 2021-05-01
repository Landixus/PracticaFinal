using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Create table bloc (
	numBloc SERIAL,
	id_workout Int NOT NULL,
	pot Int NOT NULL,
	temps Int NOT NULL,
	PRIMARY KEY(numBloc, id_workout),
	FOREIGN KEY(id_workout) REFERENCES public.workout(id_workout)
)

 */

[Serializable()]
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
