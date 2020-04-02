using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CuentaCorriente : CuentaBancaria
    {
        public const double SOBREGIRO = -1000;
        public bool ConsignacionInicial = true;
        private const double MINIMOCONSIGNACION=100000;

        public override void Consignar(double valor)
        {
            if (this.ConsignacionInicial == true)
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
                    throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 50000");
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
            if (nuevoSaldo >= SOBREGIRO)
            {
                MovimientoFinanciero movimiento = new MovimientoFinanciero();
                movimiento.ValorRetiro = valor;
                movimiento.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(movimiento);
            }
            else
            {
                throw new CuentaCorrienteRetirarMaximoSobregiroException("No es posible realizar el Retiro, supera el valor de sobregiro permitido");
            }
        }
    }



    [Serializable]
    public class CuentaCorrienteRetirarMaximoSobregiroException : Exception
    {
        public CuentaCorrienteRetirarMaximoSobregiroException() { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message) : base(message) { }
        public CuentaCorrienteRetirarMaximoSobregiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaCorrienteRetirarMaximoSobregiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

   
}
