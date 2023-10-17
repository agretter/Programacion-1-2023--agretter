using Back.CoNexionDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Back.Principal;

namespace Back
{
    public class Principal
    {
        // Método para dar de alta un cliente.
        AplicacionDBcontext dbContext = new AplicacionDBcontext();
        public void AltaCliente(Cliente cliente)
        {
            dbContext.Clientes.Add(cliente);
            dbContext.SaveChanges();
        }
        //se usa el parametro de tipo string pero se puede usar la otra linea tambien.
        public void Altacuenta(Cuenta_bancaria cbparamentro, Cliente cliente, string tipo)
        {
            Cuenta_bancaria cuenta_bancaria = new Cuenta_bancaria();
            cuenta_bancaria.cliente = cliente;
            //cuenta_bancaria.tipo = cbparamentro.tipo;
            cuenta_bancaria.tipo = tipo;
            cuenta_bancaria.saldo = 0;
            cuenta_bancaria.numeroCuenta = Generarnrocuenta(tipo, cliente.DNI);
            dbContext.Cuenta_Bancarias.Add(cuenta_bancaria);
            dbContext.SaveChanges();
        }

        // Método para generar un numero de cuenta.
        public int Generarnrocuenta(string tipo, int dni)
        {
            var nrodecuenta = 0;
            if (tipo == "Caja de ahorro")
            {
                nrodecuenta = int.Parse("0000" + dni.ToString());
            }
            else
            {
                nrodecuenta = int.Parse("1111" + dni.ToString());
            }

            return nrodecuenta;
        }
        
        // Método para dar de alta la tarjeta.
        public void Altatajeta(Tarjeta_de_credito datostarjeta, Cliente cliente)
        {
            Tarjeta_de_credito tarjeta_credito = new Tarjeta_de_credito();
            tarjeta_credito.cliente = cliente;
            tarjeta_credito.saldodisponible = 0;
            tarjeta_credito.limitecredito = 0;
            tarjeta_credito.numerotarjeta = Generarnrotarjeta(cliente.DNI);
            dbContext.Tarjeta_De_Creditos.Add(tarjeta_credito);
            dbContext.SaveChanges();
        }

        // Método para generar un numero de tarjeta.
        // ver alta cliente si esta bien implentado.
        public int Generarnrotarjeta(int dni)
        {
            var nrodetarjeta = 0;
            if (AltaCliente != null)

            {
                nrodetarjeta = int.Parse("2222" + dni.ToString());

            }
            else
            {
                Console.WriteLine("No se puede generar el número de tarjeta porque no hay un cliente dado de alta.");
            }
            return nrodetarjeta;

        }
        //Estado pausado para la tajeta (baja logica).
        public enum EstadoTarjeta
        {
            Activa,
            Pausada,
            Bloqueada,
            Cancelada,
        }

        // Método para pausar la tarjeta.
        public void PausarTarjeta(Tarjeta_de_credito parametroestado)
        {
            parametroestado.Estado = EstadoTarjeta.Pausada;
        }

        // Método para bloquear la tarjeta.
        public void BloquearTarjeta(Tarjeta_de_credito parametroestado)
        {
            parametroestado.Estado = EstadoTarjeta.Bloqueada;
        }

        // Método para cancelar la tarjeta.
        public void CancelarTarjeta(Tarjeta_de_credito parametroestado)
        {
            parametroestado.Estado = EstadoTarjeta.Cancelada;
        }

        // Método para realizar un deposito a una cuenta.
        public void Realizardeposito(int NumeroCuenta, int monto)
        {
            Cuenta_bancaria cuentaencontrada = dbContext.Cuenta_Bancarias.Find(NumeroCuenta);

            if (cuentaencontrada != null)
            {
                cuentaencontrada.saldo += monto;
                dbContext.SaveChanges();
            }

        }
        // Método para realizar una extraccion de una cuenta.
        // ver el string y los return.
        public string Realizarextraccion(int NumeroCuenta, int monto)
        {
            Cuenta_bancaria cuentaencontrada = dbContext.Cuenta_Bancarias.Find(NumeroCuenta);

            if (cuentaencontrada != null)
            {
                if (cuentaencontrada.saldo > monto)
                {
                    cuentaencontrada.saldo -= monto;
                    dbContext.SaveChanges();
                    return "Extraccion realizada con exito";
                }
                else return "Su saldo no es suficiente.";
            }
            else return "Cuenta no encontrada";
        }

        // Método para realizar un deposito a una cuenta.
        // ver el tipo void con el return.
        // nose si cuenta destino y origen me toma el numro de cuenta que esta en cuenta_bancaria.

        public void Realizartransferencia(Cuenta_bancaria cuentaorigen, Cuenta_bancaria cuentadestino, int monto) 
        {
            if (cuentaorigen != null && cuentadestino != null)
            {
                if (cuentaorigen.saldo >= monto)
                {
                    cuentaorigen.saldo -= monto;
                    cuentadestino.saldo += monto;
                    dbContext.SaveChanges();
                }
                else return "Su saldo no es suficiente.";
            }
            else return "Una o dos de la cuentas no existe";
        }

        // ver el tipo void con el return.
        public void Pagocontarjeta(Tarjeta_de_credito tarjeta, int monto, int saldo)
        {
            if (tarjeta != null)
            {
                if (tarjeta.Estado == EstadoTarjeta.Activa)
                {
                    if (tarjeta.saldodisponible >= monto)
                    {
                        tarjeta.saldodisponible -= monto;
                        dbContext.SaveChanges();
                        return "Pago realizado con éxito.";
                    }
                    else return "Saldo insuficiente en la tarjeta de crédito.";
                }
                else
                {
                    return "El estado de su tarjeta no permite realizar pagos";
                }
                
            }
        }

    }

}

