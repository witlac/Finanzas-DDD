using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base;

namespace Domain.Entities
{
    public class CertificadoDeDepositoATermino : Entity<int>
    {

        public DateTime FechaDeTermino { get; set; }

        public DateTime FechaDeInicio { get; set; } 

        public bool ConsignacionInicial = true;

        public const double MINIMOCONSIGNACION = 1000000;

        public double TasaInteres { get; set; }

        public double Saldo { get; protected set; }
        public List<MovimientoFinanciero> Movimientos { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }

        public CertificadoDeDepositoATermino()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }


        public void Consignar(double valor)
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
                    this.FechaDeInicio = consignacion.FechaMovimiento;
                    
                }
                else
                {
                    throw new Exception("No es posible realizar la consignacion, la primera consignacion debe ser mayor a 1 millon");
                }
            }
            else
            {
                throw new Exception("Solo se puede consignar una vez");

            }

        }

        public void Retirar(double valor)
        {     
            if (FechaDeTermino < DateTime.Now)
            {
                SaldoConIntereses();
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.ValorRetiro = valor;
                retiro.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(retiro);
            }
            else
            {
                throw new Exception("No es posible realizar el Retiro, porque no se ha cumplido la fecha a termino");
            }
        }

        public double  SaldoConIntereses()
        {
            TimeSpan meses = (FechaDeTermino - FechaDeInicio);
            int dias = (meses.Days / 30);
            double saldoConInteres = Saldo * (1 + TasaInteres * dias); //tasa de interes mensual simple
            Saldo = saldoConInteres;
            return saldoConInteres;
        }

    }
}
