using Application;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using static Application.CrearDepositoATerminoService;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>()
             .UseInMemoryDatabase("Banco")
             .Options;

            BancoContext context = new BancoContext(optionsInMemory);
            
            CrearCuentaBancaria(context);
            CrearDepositoATermino(context);
           

        }

  
      

        private static void CrearCuentaBancaria(BancoContext context)
        {
            #region  Crear

            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(context));
            var requestCrer = new CrearCuentaBancariaRequest() { Numero = "524255", Nombre = "Boris Arturo" };

            CrearCuentaBancariaResponse responseCrear = _service.Ejecutar(requestCrer);

            System.Console.WriteLine(responseCrear.Mensaje);
            #endregion
        }

     

        private static void CrearDepositoATermino(BancoContext context)
        {
            CrearDepositoATerminoService _service = new CrearDepositoATerminoService(new UnitOfWork(context));
            var requestCrer = new CrearDepositoATerminoRequest() { Numero = "12345", Nombre = "Cristian Mejia",FechaDeInicio=DateTime.Now,FechaDeTermino = new DateTime(2020,3,16) ,TasaInteres=0.02};
            CrearDepositoATerminoResponse responseCrear = _service.Ejecutar(requestCrer);
            System.Console.WriteLine(responseCrear.Mensaje);
            System.Console.ReadKey();
        }
    }
}
