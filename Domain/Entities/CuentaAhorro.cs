using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const double TOPERETIRO = 1000;
        public bool ConsignacionInicial = true;
        private const double MINIMOCONSIGNACION = 50000;

        public override void Consignar(double valor)
        {
            if(this.ConsignacionInicial == true)
            {
                if (valor >= MINIMOCONSIGNACION)
                {
                    MovimientoFinanciero consignacion = new MovimientoFinanciero();
                    consignacion.ValorConsignacion = valor;
                    consignacion.FechaMovimiento = DateTime.Now;
                    Saldo += valor;
                    this.Movimientos.Add(consignacion);
                    this.ConsignacionInicial = false;
                }
                else
                {
                    throw new CuentaCorrienteConsignarException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
                }
            }
            else
            {
                MovimientoFinanciero consignacion = new MovimientoFinanciero();
                consignacion.ValorConsignacion = valor;
                consignacion.FechaMovimiento = DateTime.Now;
                Saldo += valor;
                this.Movimientos.Add(consignacion);
            }
            

        }

        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            if (nuevoSaldo > TOPERETIRO)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.ValorRetiro = valor;
                retiro.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(retiro);
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
        }
    }

    public class CuentaCorrienteConsignarException : Exception
    {
        public CuentaCorrienteConsignarException() { }
        public CuentaCorrienteConsignarException(string message) : base(message) { }
        public CuentaCorrienteConsignarException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteConsignarException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    [Serializable]
    public class CuentaAhorroTopeDeRetiroException : Exception
    {
        public CuentaAhorroTopeDeRetiroException() { }
        public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
        public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaAhorroTopeDeRetiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
