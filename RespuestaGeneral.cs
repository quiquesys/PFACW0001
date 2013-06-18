using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PFACW0001
{
    public class RespuestaGeneral
    {
        Object _respuesta;
        /// <summary>
        /// Almacena o devuelve la respuesta obtenida de la operación realizada
        /// </summary>
        public Object Respuesta
        {
            get { return _respuesta; }
            set { _respuesta = value; }
        }
        Boolean _resultado;
        /// <summary>
        /// Almacena o devuelve el resutlado obtenido de la operación realizada.
        /// Los resultados pueden ser [true]=exito o [false]=fallo
        /// </summary>
        public Boolean Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }

        String _mensaje;
        /// <summary>
        /// Almacena o devuelve el mensaje correspondiente a la operación.
        /// En caso de fallo devuelve la causa de fallo, en caso contrario el mensaje es "OK";
        /// </summary>
        public String Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        /// <summary>
        /// Constructor de clase Respuesta General, inicializa las variables de clase.
        /// </summary>
        public RespuestaGeneral()
        {
            Resultado = false;
            Mensaje = String.Empty;
        }
    }
}
