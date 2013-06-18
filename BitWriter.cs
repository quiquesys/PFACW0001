using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;

namespace PFACW0001
{
    /// <summary>
    /// Summary description for BitWriter
    /// </summary>
    public class BitWriter
    {
        protected StreamWriter logStream;

        /// <summary>
        /// Inicializa el archivo tambien para su escritura
        /// </summary>
        public BitWriter(string workdir)
        {
            string logFileName;

            logFileName = String.Concat(workdir,
                ConfigurationManager.AppSettings["LogFileNameFormat"]);

            FileInfo archivo = new FileInfo(logFileName);
            DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0, 0));

            if (archivo.Exists)
            {
                if (String.Compare(archivo.LastWriteTime.ToString("yyyyMMdd"), yesterday.ToString("yyyyMMdd")) < 0)
                {
                    archivo.Delete();
                    logStream = archivo.CreateText();
                }
                else
                {
                    logStream = archivo.AppendText();
                }
            }
            else
                logStream = archivo.CreateText();

            logStream.WriteLine(String.Format("--- Inicio de control {0} ---",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")));
        }

        /// <summary>
        /// Ingresar un texto para indicar un evento. En el archivo especificado en web.config quedara el texto junto a la fecha y hor a del evento.
        /// </summary>
        /// <param name="LogIndicationText">Texto a indicar fecha hora</param>
        public void LogEventTime(string LogIndicationText)
        {
            logStream.WriteLine(String.Format("{0} - {1}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), LogIndicationText));
        }

        /// <summary>
        /// Necesario para cerrar el archivo
        /// </summary>
        public void CloseFile()
        {
            logStream.WriteLine(String.Format("--- Finalizacion de control {0} ---",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")));
            logStream.Close();
        }


        public static void LogSingleEvent(string logIndicationText)
        {
            StreamWriter strm;
            string logFileName;
            string workdir = ConfigurationManager.AppSettings["DirectorioTrabajo"];

            logFileName = String.Concat(workdir,
                ConfigurationManager.AppSettings["LogFileNameFormat"]);

            FileInfo archivo = new FileInfo(logFileName);
            DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0, 0));

            if (archivo.Exists)
            {
                if (String.Compare(archivo.LastWriteTime.ToString("yyyyMMdd"), yesterday.ToString("yyyyMMdd")) < 0)
                {
                    archivo.Delete();
                    strm = archivo.CreateText();
                }
                else
                {
                    strm = archivo.AppendText();
                }
            }
            else
                strm = archivo.CreateText();

            strm.WriteLine(String.Format("{0} - {1}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), logIndicationText));

            strm.Close();
        }

    }
}