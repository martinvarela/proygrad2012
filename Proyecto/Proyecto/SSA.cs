using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SSA
{

    public static int largo = 10;
    public static int cantMuestras = 40;
    public int[] muestras = new int[cantMuestras];
    public int[,] resultado = new int[largo, largo];
    public int[] aux_muestras = new int[cantMuestras];
    public int[,] aux_resultado = new int[largo, largo];

    public List<int> muestreados;
    public List<int> todos;
    

    public List<PuntoZonificacion> SimulatedAnnealing2(List<PuntoZonificacion> puntos)
    {
        List<PuntoZonificacion> resultado = new List<PuntoZonificacion>();
        
        //esto reduce el conjunto de puntos a 400 para luego hacer SSA con esos puntos
        //cuando este lo de promediar zonas no es necesario, ya que eso va a reducir la cant de puntos
        int indAct = puntos.Count / 400;
        while (indAct < puntos.Count)
        {
            PuntoZonificacion puntoRes = new PuntoZonificacion();
            Coordenada coordenada = new Coordenada();
            coordenada.X = puntos[indAct].Coordenada.X;
            coordenada.Y = puntos[indAct].Coordenada.Y;
            puntoRes.Coordenada = coordenada;
            puntoRes.Variabilidad = puntos[indAct].Variabilidad;
            indAct = indAct + (puntos.Count / 400);
            resultado.Add(puntoRes);
        }

        //a partir de los puntos, selecciona als muestras iniciales
        //List<PuntoZonificacion> zonif =  Inicializar2(resultado);
        //List<PuntoZonificacion> zonif =  ClonarZonif(resultado);
        Inicializar3(resultado);
        //Imprimir2(todos);
        //Imprimir2(muestreados);
        //List<PuntoZonificacion> zonifAux;
        List<int> auxMuestreados;
        List<int> auxTodos;
        double auxFitness;
        double fitness;
        
        //Random rnd = new Random();
        int iteration = -1;
        ////the probability
        double proba;
        double alpha = 0.999;
        double temperature = 400.0;
        double epsilon = 0.0000001;
        double delta;
        fitness = CalcularFitness2(resultado, muestreados);

        System.Diagnostics.Debug.WriteLine(" fitness: " + fitness);
        while (iteration < 10000/*temperature > epsilon*/ )
        {
            iteration++;

            //zonifAux = ClonarZonif(zonif);
            auxMuestreados = ClonarLista(muestreados);
            auxTodos = ClonarLista(todos);
            MoverMuestra2(auxMuestreados, auxTodos);
            auxFitness = CalcularFitness2(resultado, auxMuestreados);
            System.Diagnostics.Debug.WriteLine(" auxFitnens: " + auxFitness);
            delta = fitness - auxFitness;
            //if the new distance is better accept it and assign it
            if (delta < 0)
            {
                muestreados = auxMuestreados;
                todos = auxTodos;
                fitness = auxFitness;
            }
            /*else
            {
                proba = rnd.NextDouble();
                //if the new distance is worse accept 
                //it but with a probability level
                //if the probability is less than 
                //E to the power -delta/temperature.
                //otherwise the old value is kept
                if (proba < Math.Exp(-delta / temperature))
                {
                    Console.WriteLine("acepto una peor");
                    muestras = aux_muestras;
                    resultado = aux_resultado;
                    fitnnes = aux_fitnnes;
                }
            }*/
            //cooling process on every iteration
            temperature *= alpha;
            //print every 400 iterations
            if (iteration % 400 == 0)
            {
                System.Diagnostics.Debug.WriteLine(fitness);
                Imprimir2(todos);
                Imprimir2(muestreados);
                System.Diagnostics.Debug.WriteLine("temp: " + temperature + " delta: " + delta + " fitnnes: " + fitness + " aux_fitnnes: " + auxFitness);
                System.Diagnostics.Debug.WriteLine("iter: " + iteration);
            }
        }
        
        
        return resultado;


    }

    //public int[,] SimulatedAnnealing(int[,] variabilidad)
    //{
    //    for (int i = 0; i < largo; i++)
    //    {
    //        for (int j = 0; j < largo; j++)
    //        {
    //            resultado[i, j] = 0;
    //        }
    //    }
    //    //Imprimir(resultado);
    //    Inicializar(resultado);
    //    //Imprimir(resultado);
    //    //ImprimirMuestras(resultado);
    //    Random rnd = new Random();
    //    int iteration = -1;
    //    //the probability
    //    double proba;
    //    double alpha = 0.999;
    //    double temperature = 400.0;
    //    double epsilon = 0.0000001;
    //    double delta;
    //    double fitnnes = CalcularFitness(muestras, variabilidad);
    //    Console.WriteLine(" fitnens: " + fitnnes);
    //    while (true /*temperature > epsilon*/ )
    //    {
    //        iteration++;

    //        aux_muestras = (int[])muestras.Clone();
    //        aux_resultado = (int[,])resultado.Clone();
    //        MoverMuestra(aux_resultado);
    //        double aux_fitnnes = CalcularFitness(aux_muestras, variabilidad);
    //        //Console.WriteLine(" aux_fitnens: " + aux_fitnnes);
    //        delta = fitnnes - aux_fitnnes;
    //        //if the new distance is better accept it and assign it
    //        if (delta < 0)
    //        {
    //            muestras = aux_muestras;
    //            resultado = aux_resultado;
    //            fitnnes = aux_fitnnes;
    //        }
    //        /*else
    //        {
    //            proba = rnd.NextDouble();
    //            //if the new distance is worse accept 
    //            //it but with a probability level
    //            //if the probability is less than 
    //            //E to the power -delta/temperature.
    //            //otherwise the old value is kept
    //            if (proba < Math.Exp(-delta / temperature))
    //            {
    //                Console.WriteLine("acepto una peor");
    //                muestras = aux_muestras;
    //                resultado = aux_resultado;
    //                fitnnes = aux_fitnnes;
    //            }
    //        }*/
    //        //cooling process on every iteration
    //        temperature *= alpha;
    //        //print every 400 iterations
    //        if (iteration % 400 == 0)
    //        {
    //            Console.WriteLine(fitnnes);
    //            Imprimir2(todos);
    //            Imprimir2(muestreados);
    //            Console.WriteLine("temp: " + temperature + " delta: " + delta + " fitnnes: " + fitnnes + " aux_fitnnes: " + aux_fitnnes);
    //            Console.WriteLine("iter: " + iteration);
    //        }
    //    }
    //    return resultado;
    //}

    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    private double CalcularFitness(int[] m, int[,] variabilidad)
    {
        int valor = 0;
        for (int i = 0; i < cantMuestras; i++)
        {
            int posi = m[i] / 10;
            int posj = m[i] % 10;
            //Console.WriteLine("m[i]: " + m[i] + " posi: "+ posi+ " posj: " + posj);
            valor += variabilidad[posi, posj];
        }
        return valor;
    }

    //el fitness es el error cuadratico medio entre los valores en los puntos reales 
    //y los valores en los puntos interpolados con las muestras
    public double CalcularFitness2(List<PuntoZonificacion> zonif, List<int> muestras)
    {   
        double valor = 0;
        int pos;
        for (int i = 0; i < muestras.Count; i++)
        {
            pos = muestras[i];
            valor += zonif[pos].Variabilidad;
        }
        return valor;
    }

    //aca voy a tener una lista con los indices de los puntos de muestreo y voy a modificar uno al azar
    private void MoverMuestra(int[,] aux_resultado)
    {
        Random rnd = new Random();
        int mover = rnd.Next(cantMuestras); // creates a number between 0 and cantMuestras
        //obtengo el x e y del punto que voy a quitar
        int xMover = aux_muestras[mover] / 10;
        int yMover = aux_muestras[mover] % 10;
        //encontrar un punto aleatorio que no este marcado para muestrear
        bool encontre = false;
        while (!encontre)
        {
            int posi = rnd.Next(largo);
            int posj = rnd.Next(largo);
            //el punto no estaba muestreado
            if (aux_resultado[posi, posj] == 0)
            {
                aux_resultado[posi, posj] = 1;
                aux_resultado[xMover, yMover] = 0;
                aux_muestras[mover] = posi * largo + posj;
                encontre = true;

            }
        }
    }

    //aca voy a tener una lista con los indices de los puntos de muestreo y voy a modificar uno al azar
    public void MoverMuestra2(List<int> auxMuestreados, List<int> auxTodos)
    {
        Random rnd = new Random();
        int mover = rnd.Next(auxMuestreados.Count); // creates a number between 0 and auxMuestreados.Count
        int posMover = auxMuestreados[mover]; //pos en la lista de auxTodos a mover
        //encontrar un punto aleatorio que no este marcado para muestrear
        bool encontre = false;
        while (!encontre)
        {
            int pos = rnd.Next(auxTodos.Count);
            //el punto no estaba muestreado
            if (auxTodos[pos] == 0)
            {
                auxTodos[pos] = 1;
                auxTodos[posMover] = 0;
                auxMuestreados[mover] = pos;
                encontre = true;
            }
        }
    }

    //funcion auxiliar para imprimir las muestra seleccionadas en determinado momento
    //es para debuggear, hay que adaptarla a la nueva forma de los puntos
    public void Imprimir(int[,] resultado)
    {
        for (int i = 0; i < largo; i++)
        {
            for (int j = 0; j < largo; j++)
            {
                Console.Write(resultado[i, j]);
            } Console.WriteLine();
        } Console.WriteLine();
    }

    public void Imprimir2(List<int> aux_todos)
    {
        foreach ( int aux in aux_todos ) 
        {
                System.Diagnostics.Debug.WriteLine(aux + ' ');

        } System.Diagnostics.Debug.WriteLine("");
    }

    
    private void ImprimirMuestras(int[,] resultado)
    {
        for (int i = 0; i < muestras.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine(muestras[i] + " ");

        } System.Diagnostics.Debug.WriteLine("");
    }

    //funcion para inicializar los puntos a muestrear, esto depende de la cantidad de muestras a seleccionar
    //armar un estilo de grilla con puntos equiespaciados ó seleccionar los puntos al azar
    private void Inicializar(int[,] resultado)
    {
        //int cantPuntos = largo * largo;
        //int cantMuestras = cantPuntos / 10;
        int muestra = 0;
        for (int i = 0; i < largo; i++)
        {
            for (int j = 0; j < largo; j++)
            {
                if (j % 2 == 1)
                    if (i % 2 == 1)
                    {
                        resultado[i, j] = 1;
                        this.muestras[muestra] = i * largo + j;
                        muestra++;
                    }
            }
        }

    }

    //funcion para inicializar los puntos a muestrear, esto depende de la cantidad de muestras a seleccionar
    //armar un estilo de grilla con puntos equiespaciados ó seleccionar los puntos al azar
    private List<PuntoZonificacion> Inicializar2(List<PuntoZonificacion> listaZonificacion)
    {
        List<PuntoZonificacion> res = new List<PuntoZonificacion>(); 
        PuntoZonificacion aux;
        Coordenada coordAux;
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind=0 ; ind < listaZonificacion.Count; ind++){
            if (((ind + 1) % (listaZonificacion.Count /cantMuestras)) == 0)
            {
                this.todos.Add(1);
                this.muestreados.Add(ind);
                aux = new PuntoZonificacion();
                coordAux = new Coordenada();
                coordAux.X = listaZonificacion[ind].Coordenada.X;
                coordAux.Y = listaZonificacion[ind].Coordenada.Y;
                aux.Coordenada = coordAux;
                aux.Variabilidad = listaZonificacion[ind].Variabilidad;
                res.Add(aux);
            }
            else
            {
                this.todos.Add(0);
            }
        }
        return res;
    }

    //funcion para inicializar los puntos a muestrear, esto depende de la cantidad de muestras a seleccionar
    //armar un estilo de grilla con puntos equiespaciados ó seleccionar los puntos al azar
    private void Inicializar3(List<PuntoZonificacion> listaZonificacion)
    {
        this.todos = new List<int>();
        this.muestreados = new List<int>();
        for (int ind = 0; ind < listaZonificacion.Count; ind++)
        {
            if (((ind + 1) % (listaZonificacion.Count / cantMuestras)) == 0)
            {
                this.todos.Add(1);
                this.muestreados.Add(ind);
            }
            else
            {
                this.todos.Add(0);
            }
        }
    }

    public List<PuntoZonificacion> ClonarZonif(List<PuntoZonificacion> zonif) {
        List<PuntoZonificacion> resultado = new List<PuntoZonificacion>();
        for (int i = 0; i < zonif.Count; i++)
        {
            PuntoZonificacion puntoRes = new PuntoZonificacion();
            Coordenada coordenada = new Coordenada();
            coordenada.X = zonif[i].Coordenada.X;
            coordenada.Y = zonif[i].Coordenada.Y;
            puntoRes.Coordenada = coordenada;
            puntoRes.Variabilidad = zonif[i].Variabilidad;
            resultado.Add(puntoRes);
        }
        return resultado;
    }

    public List<int> ClonarLista( List<int> lista ){
        List<int> res = new List<int>();
        for (int i=0; i < lista.Count; i++)
        {
            res.Add(lista[i]);
        }
        return res;
        
    }

    //calculo el Root Mean Square Error para los puntos estimados a partir de las muestras
    //esta va a ser la funcion de fitness, cuando menor sea el error, mejor es la solucion
    public double RMSE(List<PuntoZonificacion> reales, List<PuntoZonificacion> estimados) {
        double error= 0;
        for (int i = 0; i < reales.Count; i++)
        {
            error += Math.Pow(reales[i].Variabilidad - estimados[i].Variabilidad, 2);
        }
        error = Math.Sqrt( error / reales.Count);
        return error;
    
    }

    


}
