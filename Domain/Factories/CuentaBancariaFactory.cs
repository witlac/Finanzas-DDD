using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Factories
{
    public class CuentaBancariaFactory
    {
        public CuentaBancaria CrearCuentaBancaria(string tipoCuenta)
        {
            if (tipoCuenta.Equals("corriente"))
            {
                CuentaBancaria cuenta = new CuentaCorriente();
                return cuenta;

            }
            else
            { 
                CuentaBancaria cuenta = new CuentaAhorro();
                return cuenta;
            }
        }
    }
}
