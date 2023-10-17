using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back
{
    public class Cuenta_bancaria
    {
        public int id { get; set; }

        public int numeroCuenta { get; set; }

        public int saldo { get; set; }

        public string tipo { get; set; }
        
        public Cliente cliente { get; set;}

        public int cuentaorigen { get; set; }

        public int cuentadestino { get; set; }
    }
}
