using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ProyectoException : Exception
{
    //public EmployeeListNotFoundException()
    //{
    //}

    public ProyectoException(string message)
        : base(message)
    {
    }

    //public EmployeeListNotFoundException(string message, Exception inner)
    //    : base(message, inner)
    //{
    //}
}


