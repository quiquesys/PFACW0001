using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PFACW0001
{
    public class ENUMS
    {
        public enum TFactura
        {
            Repuesto,
            Taller,
            VehiculoNuevo,
            VehiculoUsado,
            Oficina
        };
        public enum TDocumento
        {
            Factura,
            NotaCredito
        };
    }
}
