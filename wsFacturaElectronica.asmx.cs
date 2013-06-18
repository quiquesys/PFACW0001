using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Linq.SqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using PGENL0001;

namespace PFACW0001
{
    /// <summary>
    /// Descripción breve de wsFacturaElectronica
    /// </summary>
    [WebService(Namespace = "http://cdinet/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsFacturaElectronica : System.Web.Services.WebService
    {
        
        [WebMethod]
        public RespuestaGeneral PFACO0001(string fechaInicial, string fechaFinal, string tipoDocumento)
        {
            RespuestaGeneral respuesta = new RespuestaGeneral();
            String loteFacturas = string.Empty;
            DateTime fechaInicioLote = new DateTime();
            DateTime fechaFinLote = new DateTime();
            try
            {
                if (String.IsNullOrEmpty(fechaInicial) || String.IsNullOrEmpty(fechaFinal)) //valida que las fechas no vengan vacias
                {
                    respuesta.Mensaje = (String.IsNullOrEmpty(fechaInicial)) ? "Debe ingresar la fecha inicial" : "Debe ingresar la fecha final";
                }
                else if (!String.IsNullOrEmpty(fechaInicial) && !String.IsNullOrEmpty(fechaFinal) && String.IsNullOrEmpty(tipoDocumento))
                {
                    respuesta.Mensaje = "Debe ingresar el tipo de documento [1=Factura][2=Nota de Crédito]";
                }
                else if (!String.IsNullOrEmpty(fechaInicial) && !String.IsNullOrEmpty(fechaFinal) && !String.IsNullOrEmpty(tipoDocumento))
                {
                    if (tipoDocumento.Equals("1") || tipoDocumento.Equals("2")) //verifica que solo se ingrese tipo de documento factura o nota de credito respectivamente
                    {
                        fechaInicioLote = DateTime.Parse(fechaInicial);
                        fechaFinLote = DateTime.Parse(fechaFinal);
                        if (SqlMethods.DateDiffDay(fechaInicioLote, fechaFinLote) < 0) //verifica que la fecha inicial no sea mayor a la fecha final
                        {
                            respuesta.Mensaje = "La fecha final no debe de ser mayor a la fecha de inicio";
                        }
                        else
                        {
                            //se procede a procesar la petición

                        }
                    }
                    else
                    {
                        respuesta.Mensaje = "El dato ingresado como tipo de documento no se reconoce como un documento valido.";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = String.Format("Detalle: {0}", ex.Message);
            }
            return respuesta;
        }           
        [WebMethod]//Anteriormente RegistraFacturaRepuestosXML
        public RespuestaGeneral RegistraDocumento(String pCompania,String pNoDocumento, String pEstadoDocumento, String pInventario, String pTipoDocumento, String pUsuario, String pFechaDocumento)
        {
            RespuestaGeneral respuesta = new RespuestaGeneral();
            DateTime inicioOperacion = DateTime.Now;
            DateTime finOperacion;
            String tDoc = "";
            try
            {
                if (pTipoDocumento.Equals("FIN") || pTipoDocumento.Equals("FOF") || pTipoDocumento.Equals("FVH") || pTipoDocumento.Equals("FVU") || pTipoDocumento.Equals("SER"))//Todas las Facturas
                {
                    tDoc = "1";
                }
                else if (pTipoDocumento.Equals("NIN") || pTipoDocumento.Equals("NOF") || pTipoDocumento.Equals("NVH") || pTipoDocumento.Equals("NVU") || pTipoDocumento.Equals("NSE"))//Todas las Notas de Credito
                {
                    tDoc = "2";
                }
                //AccesoDatos acd = new AccesoDatos();
                //string qry = String.Format(Query.EstadoDocumento, tDoc,pCompania,pNoDocumento);
                //DataTable estadoDoc = acd.RealizaConsulta(qry);
                //if (estadoDoc.Rows.Count > 0)
                //{
                //    pEstadoDocumento = estadoDoc.Rows[0]["estado"].ToString();
                //}
                string xmlDoc = "";
                inicioOperacion = DateTime.Now;
                if (pTipoDocumento.Equals("FIN"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Repuesto, "");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("NIN"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Repuesto, "");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("SER"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Taller, "");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("NSE"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Taller, "");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("FVH"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.VehiculoNuevo,"",pFechaDocumento);
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("FVU"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.VehiculoUsado, "",pFechaDocumento);
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("NVH") || pTipoDocumento.Equals("NVU"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Oficina, "NCR");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("FOF"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Oficina, "FAC");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (pTipoDocumento.Equals("NOF"))
                {
                    GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, pCompania, pEstadoDocumento, pInventario, pNoDocumento, PFACW0001.ENUMS.TFactura.Oficina, "NCR");
                    xmlDoc = xml.getXmlDocumentoElectronico();
                }
                if (String.IsNullOrEmpty(xmlDoc))
                {
                    respuesta.Resultado = false;
                    respuesta.Respuesta = "S/D";
                    respuesta.Mensaje = "Error al Armar XML del Documento";
                }
                else
                {
                    RespuestaGeneral temp = new RespuestaGeneral();
                    temp = CargaDocumento_IFACERE_LOCAL(xmlDoc, pTipoDocumento);
                    respuesta.Resultado = temp.Resultado;
                    respuesta.Respuesta = temp.Respuesta;
                    respuesta.Mensaje = temp.Mensaje;
                    if (respuesta.Resultado)
                    {
                        string hora = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                        AlmacenaEnvioDocumentos(false, pCompania, pTipoDocumento, "", "", hora, pUsuario, "0", pNoDocumento);
                    }
                    finOperacion = DateTime.Now;
                    respuesta.Mensaje = respuesta.Mensaje + Environment.NewLine + String.Format("Duración: {0}", (finOperacion - inicioOperacion).ToString().Substring(0,8));
                }
            }
            catch (Exception err)
            {
                finOperacion = DateTime.Now;
                respuesta.Resultado = false;
                respuesta.Respuesta = null;
                respuesta.Mensaje = err.Message + Environment.NewLine + String.Format("Duración: {0}", (finOperacion - inicioOperacion).ToString().Substring(0, 8));
            }
            return respuesta;
        }        
        private string ArmaLoteDocumentos(String tipoDocumento, String Compania, String tipoInventario, String fecha_inicio, String fecha_fin, String usuario, String NumLote)
        {
            string resultado = "";
            string pedido = NumLote;
            string tDoc = "";
            int pilaDocumento = 0;
            DataTable ListadoDocumentos = new DataTable();
            if (tipoDocumento.Equals("FIN") || tipoDocumento.Equals("FOF") || tipoDocumento.Equals("FVH") || tipoDocumento.Equals("FVU") || tipoDocumento.Equals("SER"))//Todas las Facturas
            {
                tDoc = "1";
            }
            else if (tipoDocumento.Equals("NIN") || tipoDocumento.Equals("NOF") || tipoDocumento.Equals("NVH") || tipoDocumento.Equals("NVU") || tipoDocumento.Equals("NSE"))//Todas las Notas de Credito
            {
                tDoc = "2";
            }
            try
            {
                AccesoDatos acd = new AccesoDatos();
                string Lote = "";
                if (tipoDocumento.Equals("FIN") || tipoDocumento.Equals("NIN") )
                {
                    Lote = String.Format(Query.LoteDocumentos, tDoc, Compania, tipoInventario,"<>", desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("SER") || tipoDocumento.Equals("NSE"))
                {
                    Lote = String.Format(Query.LoteDocumentos, tDoc, Compania, tipoInventario, "=", desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("FVH")) 
                {
                    Lote = String.Format(Query.LoteDocumentosFVH,Compania, desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("NVH"))
                {
                    Lote = String.Format(Query.LoteDocumentosNVH, Compania, desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("NVU"))
                {
                    Lote = String.Format(Query.LoteDocumentosNVU, Compania, desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("FVU"))
                {
                    string vsste="";
                    string serie = "";
                    if (Compania.Equals("CS"))
                    {
                        vsste = String.Format("'{0}',''", Compania);
                        serie = "U";
                    }
                    else if (Compania.Equals("RE"))
                    {
                        vsste = String.Format("'{0}'",Compania);
                        serie = "C";
                    }

                    Lote = String.Format(Query.LoteDocumentosFVU, desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"),vsste,serie);
                }
                if (tipoDocumento.Equals("FOF"))
                {
                    Lote = String.Format(Query.LoteDocumentosOficina,Compania,"FAC", desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                if (tipoDocumento.Equals("NOF"))
                {
                    Lote = String.Format(Query.LoteDocumentosOficina,Compania,"NCR", desglosaFecha(fecha_inicio, "D"), desglosaFecha(fecha_fin, "D"), desglosaFecha(fecha_inicio, "M"), desglosaFecha(fecha_fin, "M"), desglosaFecha(fecha_inicio, "A"), desglosaFecha(fecha_fin, "A"));
                }
                DataTable Todas = acd.RealizaConsulta(Lote);
                
                if (Todas.Rows.Count > 0)
                {
                    DataTable NoProcesadas = new DataTable();
                    DataRow[] rrr = Todas.Select("procesada <> ''");
                    if (rrr.Length > 0)
                        NoProcesadas = rrr.CopyToDataTable();
                    DataTable Procesadas = new DataTable();
                    DataRow[] ppp = Todas.Select("procesada = ''");
                    if (ppp.Length > 0)
                        Procesadas = ppp.CopyToDataTable();
                    //Aca va la Logica para guardar las NoProcesadas
                    if (NoProcesadas.Rows.Count > 0)
                    {
                        foreach (DataRow r in NoProcesadas.Rows)
                        {
                            AlmacenaFacturas_NoAceptadas(Compania, tipoDocumento, r["NO_DOC"].ToString().Trim(), "Factura aun no Procesada - Sin Detalle", "N");
                        }
                    }
                    //Fin de Logica
                    if (Procesadas.Rows.Count > 0)
                    {
                        ListadoDocumentos = Procesadas.Copy();
                    }
                }
                int CantidadDocumentos;
                if (ListadoDocumentos.Rows.Count > 0)
                {
                    CantidadDocumentos = ListadoDocumentos.Rows.Count;
                }
                else
                {
                    CantidadDocumentos = 0;
                }
                if (CantidadDocumentos > 0)
                {
                    String InitTag = @"<?xml version='1.0' encoding='ISO-8859-1' ?>"+Environment.NewLine+
                        "<LOTE>"+Environment.NewLine+"<PEDIDO>" + pedido + "</PEDIDO>" +Environment.NewLine+
                        "<TOTALDOCUMENTOS>" + CantidadDocumentos + "</TOTALDOCUMENTOS>"+Environment.NewLine+"<DOCUMENTOS>"+Environment.NewLine;
                    String EndTag = Environment.NewLine+"</DOCUMENTOS>"+Environment.NewLine+"</LOTE>";
                    String Cuerpo = "";
                    String DocumentoLote = "";
                    for (int i = 0; i < ListadoDocumentos.Rows.Count; i++)
                    {
                        pilaDocumento = i;
                        if (tipoDocumento.Equals("FIN"))//Facturas de Inventario
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Repuesto, "");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("NIN"))//NotaCredito Inventario
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Repuesto, "");
                            string NotaCreditoTemp = xml.getXmlDocumentoElectronico();
                            NotaCreditoTemp = NotaCreditoTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + NotaCreditoTemp;
                        }
                        if (tipoDocumento.Equals("SER"))
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Taller, "");
                            string facturaTempServ = xml.getXmlDocumentoElectronico();
                            facturaTempServ = facturaTempServ.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTempServ;
                        }
                        if (tipoDocumento.Equals("NSE")) //Notas de credito de servicios
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Taller, "");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("FVH")) //Facturas vehiculos nuevos
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.VehiculoNuevo, "", ListadoDocumentos.Rows[i]["FECHA"].ToString());
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("NVH")) //Notas de credito vehiculos nuevos
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Oficina, "NCR");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("FVU")) //Notas de credito vehiculos usados
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.VehiculoUsado, "", ListadoDocumentos.Rows[i]["FECHA"].ToString());
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("NVU")) //Notas de credito vehiculos usados
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Oficina, "NCR");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("FOF")) //Facturas de Oficina
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.Factura, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Oficina, "FAC");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("NOF")) //Notas de credito de Oficina
                        {
                            GeneraXML xml = new GeneraXML(PFACW0001.ENUMS.TDocumento.NotaCredito, Compania, ListadoDocumentos.Rows[i]["ESTADO"].ToString().Trim(), tipoInventario, ListadoDocumentos.Rows[i]["NO_DOC"].ToString().Trim(), PFACW0001.ENUMS.TFactura.Oficina, "NCR");
                            string facturaTemp = xml.getXmlDocumentoElectronico();
                            facturaTemp = facturaTemp.Remove(0, 39);
                            Cuerpo = Cuerpo + facturaTemp;
                        }
                        if (tipoDocumento.Equals("3"))//NotaDebito
                        {
                        }
                    }
                    DocumentoLote = InitTag + Cuerpo + EndTag;
                    resultado = DocumentoLote;
                }
                else
                {
                    throw new Exception("No exiten documentos en la fecha indicada.");
                }
            }
            catch (Exception err)
            {
                if (ListadoDocumentos.Rows.Count>0)
                throw new Exception("PFACLOTEx - Doc. No: " + pilaDocumento.ToString() + "  Documento: " + ListadoDocumentos.Rows[pilaDocumento]["NO_DOC"].ToString() +".- "+ err.Message);
                else
                    throw new Exception("PFACLOTEx: " + err.Message);
            }
            return resultado;
        }

        private string ObtieneUltimoLote(String Cia, String TipoDocumento)
        {
            try
            {
                AccesoDatos cpm = new AccesoDatos();
                DataTable NumLote = cpm.RealizaConsulta(String.Format(Query.NumeroLote, Cia, TipoDocumento));
                Decimal lote = Convert.ToDecimal(NumLote.Rows[0]["NUMLOT"].ToString());
                string loteActual = Convert.ToString(lote + 1);
                string dml = String.Format(Query.IncrementaLote, loteActual, Cia, TipoDocumento);
                int valorUpdate = cpm.RealizaDml(dml);
                if (valorUpdate == 1)
                {
                    return lote.ToString();
                }
                else
                {
                    throw new Exception("Al Actualizar Numero Lote xTFAC045");
                }
            }
            catch (Exception err)
            {
                
                throw new Exception("Error al Obtener Numero de Pedido Lote - "+err.Message);
            }
        }

        private string desglosaFecha(string fecha, string tipo)
        {
            string respuesta = "";
            try
            {
                switch (tipo)
                {
                    case "A": respuesta = fecha.Substring(6, 2);
                        break;
                    case "M": respuesta = fecha.Substring(2, 2);
                        break;
                    case "D": respuesta = fecha.Substring(0, 2);
                        break;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Error en el formato de fecha " + err.Message);
            }
            return respuesta;
        }        
        public RespuestaGeneral CargaDocumentoLote_IFACERE_LOCAL(String pDatos)
        {
            RespuestaGeneral respuesta = new RespuestaGeneral();
            try
            {
                iflocal.SSO_wsEFactura ifacere = new iflocal.SSO_wsEFactura();
                ifacere.Timeout = 42300000;
                iflocal.clsResponseLote retorno = ifacere.RegistraLoteDocumentosXML(pDatos);
                respuesta.Resultado = retorno.pResultado;
                respuesta.Mensaje = retorno.pDescripcion;
                respuesta.Respuesta = null;//retorno.pResultadoDocumentos;
            }
            catch (Exception err)
            {
                respuesta.Resultado = false;
                respuesta.Mensaje = "Error en la Carga del Lote - IFACERE Local x001 "+ err.Message;
                respuesta.Respuesta = null;
            }
            return respuesta;
        }

        
        private RespuestaGeneral CargaDocumento_IFACERE_LOCAL(String pDatos, String pTipoDoc)
        {
            RespuestaGeneral respuesta = new RespuestaGeneral();
            try
            {
                iflocal.SSO_wsEFactura ifacere = new iflocal.SSO_wsEFactura();
                if (pTipoDoc.Equals("FIN") || pTipoDoc.Equals("SER") || pTipoDoc.Equals("FOF")||pTipoDoc.Equals("FVH")||pTipoDoc.Equals("FVU"))
                {
                    iflocal.clsResponseGeneral retorno = ifacere.RegistraFacturaXML(pDatos);
                    respuesta.Resultado = retorno.pResultado;
                    respuesta.Mensaje = retorno.pDescripcion;
                    respuesta.Respuesta = retorno.pRespuesta;
                }
                if (pTipoDoc.Equals("NIN") || pTipoDoc.Equals("NSE") || pTipoDoc.Equals("NVH") || pTipoDoc.Equals("NVU") || pTipoDoc.Equals("NOF"))
                {
                    iflocal.clsResponseGeneral retorno = ifacere.RegistraNotaCreditoXML(pDatos);
                    respuesta.Resultado = retorno.pResultado;
                    respuesta.Mensaje = retorno.pDescripcion;
                    respuesta.Respuesta = retorno.pRespuesta;
                }
            }
            catch (Exception err)
            {
                respuesta.Resultado = false;
                respuesta.Mensaje = "Error en la Carga del Documento - IFACERE Local x002 " + err.Message;
                respuesta.Respuesta = null;
            }
            return respuesta;
        }

        [WebMethod]
        public RespuestaGeneral RegistraLoteDocumentos(String tipoDocumento, String Compania, String tipoInventario, String fecha_inicio, String fecha_fin, String usuario)
        {
            RespuestaGeneral respuesta = new RespuestaGeneral();
            DateTime inicioOperacion = DateTime.Now;
            DateTime finOperacion;
            string xmlLote = "";
            try
            {
                
                inicioOperacion = DateTime.Now;
                string LoteNumero = ObtieneUltimoLote(Compania, tipoDocumento);
                xmlLote = ArmaLoteDocumentos(tipoDocumento, Compania, tipoInventario, fecha_inicio, fecha_fin, usuario, LoteNumero);
                if (String.IsNullOrEmpty(xmlLote))
                {
                    respuesta.Resultado = false;
                    respuesta.Respuesta = "N/D";
                    respuesta.Mensaje = "Error al Armar XML del Lote";
                }
                else
                {
                    RespuestaGeneral temp = new RespuestaGeneral();
                    temp = CargaDocumentoLote_IFACERE_LOCAL(xmlLote);
                    respuesta = temp;
                    if (respuesta.Resultado)
                    {
                        string hora = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
                        AlmacenaEnvioDocumentos(true, Compania, tipoDocumento, fecha_inicio, fecha_fin, hora, usuario, LoteNumero,"");
                    }
                    finOperacion = DateTime.Now;                    
                    respuesta.Mensaje = respuesta.Mensaje + Environment.NewLine + String.Format("Duración: {0}", (finOperacion - inicioOperacion).ToString().Substring(0,8));
                }                
            }
            catch(Exception err)
            {
                finOperacion = DateTime.Now;
                respuesta.Resultado = false;
                respuesta.Respuesta = xmlLote;
                respuesta.Mensaje = err.Message + Environment.NewLine + String.Format("Duración: {0}", (finOperacion - inicioOperacion).ToString().Substring(0, 8));
            }
            return respuesta;
        }
    

        private void AlmacenaEnvioDocumentos(bool Lote, String Cia, String TipoFactura, String FechaLoteI, String FechaLoteF, String horaEnvio, String UsuarioOperacion, String NumLote, String Factura)
        {
            try
            {
                AccesoDatos CapaMedia = new AccesoDatos();
                if (Lote)
                {
                    string AnioInicio= desglosaFecha(FechaLoteI,"A");
                    string MesInicio = desglosaFecha(FechaLoteI,"M");
                    string DiaInicio = desglosaFecha(FechaLoteI,"D");
                    string AnioFin = desglosaFecha(FechaLoteF,"A");
                    string MesFin = desglosaFecha(FechaLoteF,"M");
                    string DiaFin = desglosaFecha(FechaLoteF,"D");
                    string SigloInicio = "1";
                    string SigloFin = "1";
                    string SigloOper = "1";
                    string AnioOper = DateTime.Now.Year.ToString().Substring(2,2);
                    AnioOper = AnioOper.PadLeft(2, '0');
                    string MesOper = DateTime.Now.Month.ToString();
                    MesOper = MesOper.PadLeft(2, '0');
                    string DiaOper = DateTime.Now.Day.ToString();
                    DiaOper = DiaOper.PadLeft(2, '0');
                    string HoraOper = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
                    if (CapaMedia.RealizaDml(String.Format(Query.AlmacenaDocumentoEnvio, Cia, TipoFactura, SigloInicio, AnioInicio, MesInicio, DiaInicio, SigloFin, AnioFin, MesFin, DiaFin, HoraOper, SigloOper, AnioOper, MesOper, DiaOper, UsuarioOperacion, NumLote, Factura)) == 1)
                    {
                        ;
                    }
                    else
                    {
                        throw new Exception("Error al Insertar Datos TFAC042");
                    }
                }
                else //si es Documento Individual
                {
                    string AnioInicio = "";
                    string MesInicio = "";
                    string DiaInicio = "";
                    string AnioFin = "";
                    string MesFin = "";
                    string DiaFin = "";
                    string SigloInicio = "";
                    string SigloFin = "";
                    string SigloOper = "1";
                    string AnioOper = DateTime.Now.Year.ToString().Substring(2, 2);
                    AnioOper = AnioOper.PadLeft(2, '0');
                    string MesOper = DateTime.Now.Month.ToString();
                    MesOper = MesOper.PadLeft(2, '0');
                    string DiaOper = DateTime.Now.Day.ToString();
                    DiaOper = DiaOper.PadLeft(2, '0');
                    string HoraOper = Convert.ToString(DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0'));
                    string dml = String.Format(Query.AlmacenaDocumentoEnvio, Cia, TipoFactura, SigloInicio, AnioInicio, MesInicio, DiaInicio, SigloFin, AnioFin, MesFin, DiaFin, HoraOper, SigloOper, AnioOper, MesOper, DiaOper, UsuarioOperacion, NumLote, Factura);
                    if (CapaMedia.RealizaDml(dml) == 1)
                    {
                        ;
                    }
                    else
                    {
                        throw new Exception("Error al Insertar Datos xTFAC042");
                    }
                }

            }
            catch (Exception err)
            {
                throw new Exception("Error al Almacenar el Envio del Lote - "+err.Message);
            }
        }
        private void AlmacenaFacturas_NoAceptadas(String Compania, String tipoFactura, String NumFactura, String Motivo, String Estado)
        {
            try
            {
                AccesoDatos ad = new AccesoDatos();
                string SigloOper = "1";
                string AnioOper = DateTime.Now.Year.ToString().Substring(2, 2).PadLeft(2,'0');
                string MesOper = DateTime.Now.Month.ToString().PadLeft(2,'0');
                string DiaOper = DateTime.Now.Day.ToString().PadLeft(2,'0');
                string HoraOper = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');                
                string dml = String.Format(Query.AlmacenaNoAceptadas,Compania,tipoFactura,NumFactura,SigloOper,AnioOper,MesOper,DiaOper,HoraOper,(Motivo.Length>50)?Motivo.Substring(0,50):Motivo,Estado);
                int almacena = ad.RealizaDml(dml);
                if (almacena == 1)
                {
                    ;
                }
                else
                {
                    throw new Exception("Al Insertar el Registro xTFAC043");
                }
            }
            catch (Exception err)
            {
                throw new Exception("Error al Almacenar Facturas No Aceptadas - "+err.Message);
            }
        }
        [WebMethod]
        public bool Test(string serie)
        {
            GeneraXML x = new GeneraXML();
            return x.CDI_F_VerificaExistenciaSerie(serie);
        }
        
    }

}
