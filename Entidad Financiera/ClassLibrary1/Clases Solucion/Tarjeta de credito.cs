using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Back.Principal;

namespace Back
{
    public class Tarjeta_de_credito
    {
        public int id { get; set; }

        public int numerotarjeta { get; set; }

        public int limitecredito { get; set; }

        public int saldodisponible { get; set; }

        public int estado { get; set; }

        public Cliente cliente { get; set; }

        public EstadoTarjeta Estado { get; set; }

       

    }
}
