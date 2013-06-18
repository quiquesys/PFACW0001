using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using PGENL0001;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace PFACW0001
{
    public class GeneraXML
    {
        #region variables de clase
        AccesoDatos ad = new AccesoDatos();
        DataTable series;
        string _nombreArchivo;
        /// <summary>
        /// Aca se crean las constantes para los tipos
        /// de documentos asociados en la Serie y Resolucion
        /// en la BD De EFACE
        /// Factura = 4
        /// Nota Credito = 5
        /// </summary>
        private const string Eface_Fac = "4";
        private const string Eface_Ncr = "5";

        private string TipoDocIFACERE;

        private string _descripcionError;
        public string DESCRIPCION_ERROR
        {
            get { return _descripcionError; }
            set { _descripcionError = value; }
        }

        public string NombreArchivo
        {
          get { return _nombreArchivo; }
          set { _nombreArchivo = value; }
        }

        XmlTextWriter _xmlDoc;

        public XmlTextWriter XmlDoc
        {
            get { return _xmlDoc; }
            set { _xmlDoc = value; }
        }
        private String _cia;

        public String Cia
        {
            get { return _cia; }
            set { _cia = value; }
        }
        private String _fechaDocumento;

        public String FechaDocumento
        {
            get { return _fechaDocumento; }
            set { _fechaDocumento = value; }
        }

        private String _fechaInicial;

        public String FechaInicial
        {
            get { return _fechaInicial; }
            set { _fechaInicial = value; }
        }
        private String _fechaFinal;

        public String FechaFinal
        {
            get { return _fechaFinal; }
            set { _fechaFinal = value; }
        }
        private PFACW0001.ENUMS.TDocumento _tipoDocumento;

        public PFACW0001.ENUMS.TDocumento TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        private String _estadoDocumento;

        public String EstadoDocumento
        {
            get { return _estadoDocumento; }
            set { _estadoDocumento = value; }
        }
        private String _inventario;

        public String Inventario
        {
            get { return _inventario; }
            set { _inventario = value; }
        }
        private String _numeroFactura;

        public String NumeroFactura
        {
            get { return _numeroFactura; }
            set { _numeroFactura = value; }
        }
        private String _TipoDocumentoOficina;

        public String TipoDocumentoOficina
        {
            get { return _TipoDocumentoOficina; }
            set { _TipoDocumentoOficina = value; }
        }
        private PFACW0001.ENUMS.TFactura _tipoFactura;

        public PFACW0001.ENUMS.TFactura TipoFactura
        {
            get { return _tipoFactura; }
            set { _tipoFactura = value; }
        }
        StringWriter _xmlStr;

        public StringWriter XmlStr
        {
            get { return _xmlStr; }
            set { _xmlStr = value; }
        }
#endregion

        public GeneraXML() {
            CDI_P_CargarSeries();
        }
        public GeneraXML(PFACW0001.ENUMS.TDocumento tipoDocumento,String cia, String estadoDocumento,String inventario, String numeroFactura,PFACW0001.ENUMS.TFactura tipoFactura, String tipDocOfi)
        {
            Cia = cia;
            TipoDocumento = tipoDocumento;
            EstadoDocumento = estadoDocumento;
            Inventario = inventario;            
            //FechaInicial = fechaInicial;
            //FechaFinal = fechaFinal;
            NumeroFactura = numeroFactura;
            TipoFactura = tipoFactura;
            TipoDocumentoOficina = tipDocOfi;
            NombreArchivo = AppDomain.CurrentDomain.BaseDirectory + "tmp\\lote"+System.DateTime.Now.ToShortDateString().Replace('/','-').Trim()+"_"+ System.DateTime.Now.ToLongTimeString().Replace("a.m.","").Replace("p.m.","").Replace(":","").Trim()+".xml";
            XmlStr = new StringWriter();
            XmlDoc = new XmlTextWriter(XmlStr);//(new MemoryStream(), Encoding.UTF8);//new XmlTextWriter(NombreArchivo, null);
        }
        public GeneraXML(PFACW0001.ENUMS.TDocumento tipoDocumento, String cia, String estadoDocumento, String inventario, String numeroFactura, PFACW0001.ENUMS.TFactura tipoFactura, String tipDocOfi, String fDoc)
        {
            Cia = cia;
            TipoDocumento = tipoDocumento;
            EstadoDocumento = estadoDocumento;
            Inventario = inventario;
            FechaDocumento = fDoc;            
            NumeroFactura = numeroFactura.Trim();
            TipoFactura = tipoFactura;
            TipoDocumentoOficina = tipDocOfi;
            NombreArchivo = AppDomain.CurrentDomain.BaseDirectory + "tmp\\lote" + System.DateTime.Now.ToShortDateString().Replace('/', '-').Trim() + "_" + System.DateTime.Now.ToLongTimeString().Replace("a.m.", "").Replace("p.m.", "").Replace(":", "").Trim() + ".xml";
            XmlStr = new StringWriter();
            XmlDoc = new XmlTextWriter(XmlStr);//(new MemoryStream(), Encoding.UTF8);//new XmlTextWriter(NombreArchivo, null);
        }
        private void CDI_P_CargarSeries()
        {
            this.series = ad.RealizaConsulta(Query.SeriesFgen038);
            DataRow s1 = this.series.NewRow();
            s1[0] = "41A";
            DataRow s2 = this.series.NewRow();
            s2[0] = "13T";
            DataRow s3 = this.series.NewRow();
            s3[0] = "13N";
            DataRow s4 = this.series.NewRow();
            s4[0] = "13V";
            DataRow s5 = this.series.NewRow();
            s5[0] = "OI";
            DataRow s6 = this.series.NewRow();
            s6[0] = "I";
            DataRow s7 = this.series.NewRow();
            s7[0] = "O";
            DataRow s8 = this.series.NewRow();
            s8[0] = "O1S";
            DataRow s9 = this.series.NewRow();
            s9[0] = "006";
            DataRow s10 = this.series.NewRow();
            s10[0] = "A";
            DataRow s11 = this.series.NewRow();
            s11[0] = "B";
            DataRow s12 = this.series.NewRow();
            s12[0] = "13C";
            DataRow s13 = this.series.NewRow();
            s13[0] = "CAC";
            this.series.Rows.Add(s1);
            this.series.Rows.Add(s2);
            this.series.Rows.Add(s3);
            this.series.Rows.Add(s4);
            this.series.Rows.Add(s5);
            this.series.Rows.Add(s6);
            this.series.Rows.Add(s7);
            this.series.Rows.Add(s8);
            this.series.Rows.Add(s9);
            this.series.Rows.Add(s10);
            this.series.Rows.Add(s11);
            this.series.Rows.Add(s12);
            this.series.Rows.Add(s13);
        }
        public string getXmlDocumentoElectronico()
        {            
            PFACW0001.Estructura.Factura ef = new PFACW0001.Estructura.Factura();            
            try
            {
                string campoNOFACTURA = "";
                string campoSERIE = "";                
                CDI_P_CargarSeries();
                XmlDoc.Formatting = Formatting.Indented;                                
                XmlDoc.Namespaces = false;
                XmlDoc.WriteStartDocument();                
                XmlDoc.WriteStartElement((TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))?"FACTURA":"NOTACREDITO");
                XmlDoc.WriteStartElement("ENCABEZADO");
                #region obtiene encabezado
                string gnufac = string.Empty;
                DataTable encabezado = new DataTable();
                #region OBTIENE ENCABEZADO
                if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo))
                {
                    TipoDocIFACERE = Eface_Fac;
                    encabezado = ad.RealizaConsulta(String.Format(Query.EncabezadoAutosNuevos, NumeroFactura, EstadoDocumento,Cia)).Copy();
                    ef.NODOCUMENTO = encabezado.Rows[0]["NOFACTURA"].ToString().Trim();
                    encabezado.Columns["FECHAEMISION"].MaxLength = FechaDocumento.ToString().Length;
                    encabezado.Rows[0]["FECHAEMISION"] = FechaDocumento;
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                {
                    TipoDocIFACERE = Eface_Fac;
                    encabezado = ad.RealizaConsulta(String.Format(Query.EncabezadoAutosUsados, NumeroFactura, EstadoDocumento)).Copy();
                    ef.NODOCUMENTO = encabezado.Rows[0]["NOFACTURA"].ToString().Trim();
                    encabezado.Columns["FECHAEMISION"].MaxLength=FechaDocumento.ToString().Length;
                    encabezado.Rows[0]["FECHAEMISION"] = FechaDocumento;
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                {
                    TipoDocIFACERE = Eface_Fac;
                    encabezado = ad.RealizaConsulta(String.Format(Query.EncabezadoOficina, NumeroFactura, EstadoDocumento,Cia)).Copy();
                    
                    if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE3"].ToString()))
                    {
                        campoNOFACTURA = "NOFACTURA3";
                        campoSERIE = "SERIE3";
                    }
                    else if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE2"].ToString()))
                    {
                        campoNOFACTURA = "NOFACTURA2";
                        campoSERIE = "SERIE2";
                    }
                    else if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE"].ToString()))
                    {
                        campoNOFACTURA = "NOFACTURA";
                        campoSERIE = "SERIE";
                    }
                    ef.NODOCUMENTO = encabezado.Rows[0][campoNOFACTURA].ToString().Trim();
                }                
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Repuesto) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))
                {
                    TipoDocIFACERE = Eface_Fac;
                    string consulta = String.Format(Query.ENCABEZADO_TFAC051, ((int)TipoDocumento) + 1, Cia, EstadoDocumento, NumeroFactura.Trim());
                    encabezado = ad.RealizaConsulta(consulta).Copy();
                    gnufac = encabezado.Rows[0]["GNUFAC"].ToString().Trim();
                    ef.NODOCUMENTO = obtieneNumFact(gnufac, "N");
                    EstadoDocumento = encabezado.Rows[0]["GFLANU"].ToString().Trim();
                }
                #endregion
                #region OBTIENE DATOS DE SAT
                if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                {
                    TipoDocIFACERE = Eface_Ncr;
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Repuesto) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))
                    {
                        string gnuref = encabezado.Rows[0]["NOFACTURA"].ToString().Trim();
                        ef.NoFactura = obtieneNumFact(gnuref, "N");
                        ef.SERIEFACTURA = obtieneNumFact(gnuref, "S");
                        ef.FECHAFACTURA = encabezado.Rows[0]["FECHAFACTURA"].ToString().Trim();
                        ef.IdSerie = obtieneNumFact(encabezado.Rows[0]["GNUFAC"].ToString().Trim(), "S");
                        ef.Empresa = obtieneDatosSAT(Cia, "", "", "EMP", TipoDocIFACERE, "");
                        ef.Sucursal = obtieneDatosSAT(ef.Empresa, "", "", "SUC", TipoDocIFACERE, ef.IdSerie);
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa, ef.Sucursal, obtieneNumFact(encabezado.Rows[0]["GNUFAC"].ToString().Trim(), "N"), "RES", TipoDocIFACERE, ef.IdSerie);//"2011-5-188-2646";  //FACTURA 2012-5-19953-43 NOTA CREDITO 2012-5-19953-44
                        ef.Caja = obtieneCaja(encabezado.Rows[0]["GCICOD"].ToString(), encabezado.Rows[0]["GFRIMP"].ToString());
                    }
                    else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                    {             
                        //FACTURA ASOCIADA
                        string pCia = String.Format("'{0}'",Cia);
                        if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                        {
                            pCia = (Cia.Equals("CS")) ? String.Format("'{0}',''", Cia) : String.Format("'{0}'", Cia);
                        }
                        DataTable facturaAsociada = new DataTable();                       
                        facturaAsociada = ad.RealizaConsulta(String.Format(Query.NumeroFacturaSerie,encabezado.Rows[0]["VDOREF"].ToString().Trim(),pCia));
                        ef.NoFactura = (String.IsNullOrEmpty(facturaAsociada.Rows[0]["IDSERIE"].ToString()))?obtieneNumFact(facturaAsociada.Rows[0]["NOFACTURA"].ToString(),"N"):facturaAsociada.Rows[0]["NOFACTURA"].ToString();
                        ef.SERIEFACTURA = (String.IsNullOrEmpty(facturaAsociada.Rows[0]["IDSERIE"].ToString())) ? obtieneNumFact(facturaAsociada.Rows[0]["NOFACTURA"].ToString(), "S") : facturaAsociada.Rows[0]["IDSERIE"].ToString();
                        ef.FECHAFACTURA = facturaAsociada.Rows[0]["FECHAFACTURA"].ToString();
                        //DATOS SAT
                        DataTable datosSAT = new DataTable();
                        datosSAT = ad.RealizaConsulta(String.Format(Query.DatosSATVOFN,encabezado.Rows[0]["VCOEIM"].ToString(),encabezado.Rows[0]["VCOMAR"].ToString(), (Cia.Equals("AC"))?"":encabezado.Rows[0][campoSERIE].ToString()));
                        ef.IdSerie = encabezado.Rows[0][campoSERIE].ToString();
                        ef.Empresa = datosSAT.Rows[0]["EMPRESA"].ToString();
                        ef.Sucursal = datosSAT.Rows[0]["SUCURSAL"].ToString();
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa, ef.Sucursal, ef.NODOCUMENTO, "RES", TipoDocIFACERE, ef.IdSerie);
                        ef.Caja = datosSAT.Rows[0]["CAJA"].ToString();
                        //MANEJA CUENTAS                        
                        DataTable cargarCuenta = new DataTable();
                        cargarCuenta = ad.RealizaConsulta(String.Format(Query.CuentasCargoAbono,NumeroFactura,"1"));
                        ef.CARGAR = construirCuenta(cargarCuenta);
                        if (!String.IsNullOrEmpty(ef.CARGAR))
                        {
                            String[] a = ef.CARGAR.Split('|');
                            for (int k = 9; k < a.Length + 9; k++)
                                ef.Opcional[k] = a[k-9];
                        }
                        DataTable abonarCuenta = new DataTable();
                        abonarCuenta = ad.RealizaConsulta(String.Format(Query.CuentasCargoAbono,NumeroFactura,"2"));
                        ef.ABONAR = construirCuenta(abonarCuenta);
                        if (!String.IsNullOrEmpty(ef.ABONAR))
                        {
                            String[] a = ef.ABONAR.Split('|');
                            for (int k = 14; k < a.Length + 14; k++)
                                ef.Opcional[k] = a[k-14];
                        }
                        
                    }                    
                }
                if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                {
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) )
                    {
                        ef.IdSerie = encabezado.Rows[0]["SERIE"].ToString();
                        ef.Empresa = encabezado.Rows[0]["EMPRESA"].ToString();
                        ef.Sucursal = encabezado.Rows[0]["GSUCOD"].ToString();
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa, ef.Sucursal, ef.NODOCUMENTO, "RES", TipoDocIFACERE, ef.IdSerie);                                               
                        ef.Caja = encabezado.Rows[0]["CAJA"].ToString();
                    }
                    else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                    {
                        DataTable datosSAT = new DataTable();
                        if (Cia.Equals("CS"))
                        {
                            datosSAT = ad.RealizaConsulta(String.Format(Query.DatosSATVOFN, Cia, encabezado.Rows[0]["VCOMAR"].ToString(), encabezado.Rows[0]["SERIE"].ToString()));
                        }
                        else
                        {
                            datosSAT = ad.RealizaConsulta(String.Format(Query.DatosSATVOFN, Cia, "", ""));
                        }
                        ef.IdSerie = encabezado.Rows[0]["SERIE"].ToString();
                        ef.Empresa = datosSAT.Rows[0]["EMPRESA"].ToString();
                        ef.Sucursal = datosSAT.Rows[0]["SUCURSAL"].ToString();
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa,ef.Sucursal,ef.NODOCUMENTO,"RES", TipoDocIFACERE,ef.IdSerie);
                        ef.Caja = datosSAT.Rows[0]["CAJA"].ToString();
                    }
                    else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                    {
                        DataTable datosSAT = new DataTable();
                        datosSAT = ad.RealizaConsulta(String.Format(Query.DatosSATVOFN, Cia, "",""));
                        string campoSerie="";
                        if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE3"].ToString()))
                            campoSerie = "SERIE3";
                        else if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE2"].ToString()))
                            campoSerie = "SERIE2";
                        else if (CDI_F_VerificaExistenciaSerie(encabezado.Rows[0]["SERIE"].ToString()))
                            campoSerie = "SERIE";                        
                        ef.IdSerie = encabezado.Rows[0][campoSerie].ToString();//serie2
                        ef.Empresa = datosSAT.Rows[0]["EMPRESA"].ToString();
                        ef.Sucursal = datosSAT.Rows[0]["SUCURSAL"].ToString();
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa, ef.Sucursal, ef.NODOCUMENTO, "RES", TipoDocIFACERE, ef.IdSerie);
                        ef.Caja = datosSAT.Rows[0]["CAJA"].ToString();
                    }
                    else
                    {
                        ef.IdSerie = obtieneNumFact(encabezado.Rows[0]["GNUFAC"].ToString().Trim(), "S");
                        ef.Empresa = obtieneDatosSAT(Cia, "", "", "EMP", TipoDocIFACERE, "");
                        ef.Sucursal = obtieneDatosSAT(ef.Empresa, "", "", "SUC", TipoDocIFACERE, ef.IdSerie);
                        ef.Resolucion = obtieneDatosSAT(ef.Empresa, ef.Sucursal, obtieneNumFact(encabezado.Rows[0]["GNUFAC"].ToString().Trim(), "N"), "RES", TipoDocIFACERE, ef.IdSerie);
                        ef.Caja = obtieneCaja(encabezado.Rows[0]["GCICOD"].ToString(), encabezado.Rows[0]["GFRIMP"].ToString());                                                                                                       
                    }
                }
                #endregion
                if (string.IsNullOrEmpty(ef.Caja)) 
                {
                    DESCRIPCION_ERROR = String.Format(" [Error no existe caja asociada al documento {0}]", NumeroFactura);
                    throw new Exception(DESCRIPCION_ERROR);
                }else if (ef.Caja.Equals("S/N"))
                {
                    if (String.IsNullOrEmpty(encabezado.Rows[0]["gfrimp"].ToString().Trim()))
                        DESCRIPCION_ERROR = String.Format(" [Error, no existe correlativo de caja]");
                    throw new Exception(DESCRIPCION_ERROR);
                }
                #region MANEJO DE USUARIO
                if (String.IsNullOrEmpty(encabezado.Rows[0]["USUARIO"].ToString()))
                {
                    ef.Usuario = "";
                }
                else if (encabezado.Rows[0]["USUARIO"].ToString().Length >= 25)
                {
                    ef.Usuario = encabezado.Rows[0]["USUARIO"].ToString().Substring(0, 24);
                }
                else
                {
                    ef.Usuario = encabezado.Rows[0]["USUARIO"].ToString();
                }
                #endregion
                #region OBTIENE DETALLE
                DataTable detalle = new DataTable();
                DataTable Descuento_Subtotal = new DataTable();
                if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo))
                {
                    detalle = ad.RealizaConsulta(String.Format(Query.DetalleAutosNuevos,NumeroFactura,Cia)).Copy();
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                {
                    detalle = ad.RealizaConsulta(String.Format(Query.DetalleAutosUsados, NumeroFactura)).Copy();
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                {
                    detalle = ad.RealizaConsulta(String.Format(Query.DetalleOficina, NumeroFactura,Cia)).Copy();
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))//DE SERVICIO
                {
                    String criterioSalida = String.Empty;
                    if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                    {
                        criterioSalida = (EstadoDocumento.Equals("A")) ? "AF" : "SF";
                    }
                    else
                    {
                        criterioSalida = "AP";
                    }
                    if (encabezado.Rows[0]["GTIFAC"].ToString().Trim().Equals("TS") && TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))//CUANDO ES TIPO ESPECIAL DE SERVICIOS
                    {
                        string qry = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                        detalle = ad.RealizaConsulta(qry).Copy();                        
                    }
                    else
                    {
                        if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                        {
                            string qry_detalle = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                            detalle = ad.RealizaConsulta(qry_detalle).Copy();                           
                        }
                        else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                        {
                            if (encabezado.Rows[0]["GTIFAC"].ToString().Trim().Equals("TS") && TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))//CUANDO ES TIPO ESPECIAL DE SERVICIO Y NOTA DE CREDITO
                            {
                                string qry_det = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                                detalle = ad.RealizaConsulta(qry_det).Copy();                                
                                string gnuref = encabezado.Rows[0]["NOFACTURA"].ToString();//.Substring(3);
                                ef.NoFactura = obtieneNumFact(gnuref, "N");// gnuref.Substring(gnufac.Length - 6, 6);
                                ef.SERIEFACTURA = obtieneNumFact(gnuref, "S");//gnuref.Substring(0, gnufac.Length - 6);
                            }
                        }
                    }
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Repuesto))//DE INVENTARIO
                {
                    String criterioSalida = String.Empty;
                    if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                    {
                        criterioSalida = (EstadoDocumento.Equals("A")) ? "AF" : "SF";
                    }
                    else
                    {
                        criterioSalida = "AP";
                    }
                    if (encabezado.Rows[0]["GTIFAC"].ToString().Trim().Equals("TR") && TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))//CUANDO ES TIPO ESPECIAL DE INVENTARIO UNA SOLO LINEA DE DETALLE
                    {
                        string qry_detalle = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                        detalle = ad.RealizaConsulta(qry_detalle).Copy();                        
                    }
                    else if (!encabezado.Rows[0]["GTIFAC"].ToString().Trim().Equals("TS"))
                    {
                        if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                        {
                            string qry_det = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                            detalle = ad.RealizaConsulta(qry_det).Copy();                            
                        }
                        else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                        {
                            string qry_det = String.Format(Query.DETALLE_TFAC052, Cia, NumeroFactura);
                            detalle = ad.RealizaConsulta(qry_det).Copy();                           
                        }
                    }
                }

                #endregion
                ef.FechaEmision = encabezado.Rows[0]["FECHAEMISION"].ToString();
                ef.Generacion = "C";

                ef.Moneda = (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo)) ? encabezado.Rows[0]["MONEDA"].ToString() : "GTQ";
                ef.TasaCambio = "1";
                ef.NombreContribuyente = encabezado.Rows[0]["NOMBRECONTRIBUYENTE"].ToString();
                ef.DireccionContribuyente = encabezado.Rows[0]["DIRECCIONCONTRIBUYENTE"].ToString();
                ef.NitContribuyente = encabezado.Rows[0]["NITCONTRIBUYENTE"].ToString();
                ef.ValorNeto = encabezado.Rows[0]["VALORNETO"].ToString();
                ef.IVA = encabezado.Rows[0]["IVA"].ToString();
                ef.Total = encabezado.Rows[0]["TOTAL"].ToString();
                ef.Descuento = encabezado.Rows[0]["DESCUENTO"].ToString();
                string exento = encabezado.Rows[0]["EXENTO"].ToString();
                ef.Exento = (String.IsNullOrEmpty(exento) || Double.Parse(exento)<=0) ? "0.00" : encabezado.Rows[0]["EXENTO"].ToString();
                
                #endregion
                
                #region ingresa datos a encabezado
                if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                {   
                    InsertaEtiqueta("NOFACTURA", ef.NODOCUMENTO,false);
                }
                else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                {
                    InsertaEtiqueta("CONCEPTO", "ANULACION",true);
                    InsertaEtiqueta("SERIEFACTURA", ef.SERIEFACTURA,true);
                    InsertaEtiqueta("FECHAFACTURA", ef.FECHAFACTURA,false);
                    InsertaEtiqueta("NOFACTURA", ef.NoFactura,false);
                    InsertaEtiqueta("NODOCUMENTO", ef.NODOCUMENTO,false);

                }
                InsertaEtiqueta("RESOLUCION", ef.Resolucion,false);
                InsertaEtiqueta("IDSERIE", ef.IdSerie,true); //ef.IdSerie  //CFSNC nota credito  CFS factura
                InsertaEtiqueta("EMPRESA", ef.Empresa,false);
                InsertaEtiqueta("SUCURSAL", ef.Sucursal,false);
                InsertaEtiqueta("CAJA", ef.Caja, false); 
                InsertaEtiqueta("USUARIO", ef.Usuario,(String.IsNullOrEmpty(ef.Usuario))?false:true);
                InsertaEtiqueta("FECHAEMISION", ef.FechaEmision,false);
                InsertaEtiqueta("GENERACION", ef.Generacion,false);
                InsertaEtiqueta("MONEDA", ef.Moneda,false);
                InsertaEtiqueta("TASACAMBIO", ef.TasaCambio,false);
                InsertaEtiqueta("NOMBRECONTRIBUYENTE", ef.NombreContribuyente,true);
                InsertaEtiqueta("DIRECCIONCONTRIBUYENTE", ef.DireccionContribuyente,true);
                InsertaEtiqueta("NITCONTRIBUYENTE", ef.NitContribuyente,true);
                InsertaEtiqueta("VALORNETO", formatoMoneda( Math.Abs(Double.Parse(ef.ValorNeto)).ToString() ), false);
                InsertaEtiqueta("IVA", formatoMoneda( Math.Abs(Double.Parse(ef.IVA)).ToString() ), false);
                InsertaEtiqueta("TOTAL", formatoMoneda( Math.Abs(Double.Parse(ef.Total)).ToString() ), false);
                InsertaEtiqueta("DESCUENTO", formatoMoneda( Math.Abs(Double.Parse(ef.Descuento)).ToString() ), false);
                InsertaEtiqueta("EXENTO", formatoMoneda( ef.Exento ),false);
                if (EstadoDocumento.Equals("A"))
                {
                    InsertaEtiqueta("RAZONANULACION","ANULACION",true);  
                }
                #endregion
                XmlDoc.WriteEndElement();//fin encabezado
                
                XmlDoc.WriteStartElement("OPCIONAL");
                #region OBTIENE OPCIONAL
                DataTable opcional = new DataTable();
                //string a = String.Format(Query.OpcionalRepuestos, TipoDocumento, Cia, EstadoDocumento, Inventario, NumeroFactura);
                if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo))
                {
                    opcional = ad.RealizaConsulta(String.Format(Query.OpcionalAutosNuevos,NumeroFactura,EstadoDocumento,Cia)).Copy();
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado))
                {
                    opcional = ad.RealizaConsulta(String.Format(Query.OpcionalAutosUsados, NumeroFactura, EstadoDocumento)).Copy();
                }
                else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                {
                    opcional = ad.RealizaConsulta(String.Format(Query.OpcionalOficina, NumeroFactura, TipoDocumentoOficina, EstadoDocumento)).Copy();                                        
                }
                else
                {
                    string qry_opc = String.Format(Query.OPCIONALES_TFAC051, ((int)TipoDocumento) + 1, Cia, EstadoDocumento, NumeroFactura);
                    opcional = ad.RealizaConsulta(qry_opc).Copy();
                    
                    if (opcional.Rows[0]["OPCIONAL5"].ToString().Length > 25)
                        opcional.Rows[0]["OPCIONAL5"] = opcional.Rows[0]["OPCIONAL5"].ToString().Substring(0, 25);
                    if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                    {
                        if (encabezado.Rows[0]["VALORNETO"].ToString().Trim().Equals("0.00") && encabezado.Rows[0]["DESCUENTO"].ToString().Trim().Equals("0.00"))
                        {
                            //opcional.Columns["OPCIONAL9"].MaxLength = 100;
                            opcional.Rows[0]["OPCIONAL9"] = "";
                        }
                        else
                        {
                            //opcional.Columns["OPCIONAL9"].MaxLength = 100;
                            opcional.Rows[0]["OPCIONAL9"] = String.Format("{0:0.00}", Double.Parse(encabezado.Rows[0]["VALORNETO"].ToString())); //Se Obtiene el Subtotal del Detalle 
                        }
                    }
                    opcional.Columns["OPCIONAL7"].MaxLength = 40;
                    opcional.Rows[0]["OPCIONAL7"] = ef.IdSerie + ef.NODOCUMENTO;
                                        
                }
                if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                {
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                    {
                        //MANEJA CUENTAS                        
                        DataTable cargarCuenta = new DataTable();
                        cargarCuenta = ad.RealizaConsulta(String.Format(Query.CuentasCargoAbono, NumeroFactura, "1"));
                        ef.CARGAR = construirCuenta(cargarCuenta);
                        DataTable abonarCuenta = new DataTable();
                        abonarCuenta = ad.RealizaConsulta(String.Format(Query.CuentasCargoAbono, NumeroFactura, "2"));
                        ef.ABONAR = construirCuenta(abonarCuenta);                       
                        if (!String.IsNullOrEmpty(ef.CARGAR))
                        {
                            String[] a = ef.CARGAR.Split('|');
                            for (int k = 9; k < a.Length + 9; k++)
                                ef.Opcional[k] = a[k-9];
                        }
                        if (!String.IsNullOrEmpty(ef.ABONAR))
                        {
                            String[] a = ef.ABONAR.Split('|');
                            for (int k = 14; k < a.Length + 14; k++)
                                ef.Opcional[k] = a[k-14];
                        }


                    }
                }
                else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                {
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                    {
                        opcional.Columns["OPCIONAL5"].MaxLength = (opcional.Columns["OPCIONAL3"].ToString().Length > 0) ? opcional.Columns["OPCIONAL3"].ToString().Length : (opcional.Columns["OPCIONAL5"].MaxLength + 1);      
                        opcional.Rows[0]["OPCIONAL5"] = opcional.Rows[0]["OPCIONAL3"].ToString();                                               
                        opcional.Columns["OPCIONAL3"].MaxLength = opcional.Columns["OPCIONAL3"].MaxLength+1;
                        opcional.Rows[0]["OPCIONAL3"] = "";
                    }
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))
                    {
                        //opcional.Columns["OPCIONAL9"].MaxLength = 100;
                        opcional.Rows[0]["OPCIONAL9"] = String.Format("{0:0.00}", Double.Parse(encabezado.Rows[0]["VALORNETO"].ToString())); //Se Obtiene el Subtotal del Detalle 
                        opcional.Columns["OPCIONAL7"].MaxLength = 40;// (ef.IdSerie.Length + ef.NODOCUMENTO.Length);
                        opcional.Rows[0]["OPCIONAL7"] = ef.IdSerie + ef.NODOCUMENTO;
                    }
                    if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Repuesto) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))
                    {
                        opcional.Rows[0]["OPCIONAL4"] = "";
                        opcional.Columns["OPCIONAL7"].MaxLength = 40;// (ef.IdSerie.Length + ef.NODOCUMENTO.Length);
                        opcional.Rows[0]["OPCIONAL7"] = ef.IdSerie + ef.NODOCUMENTO;
                    }
                }
                
                ef.Opcional[0] = !opcional.Columns.Contains("OPCIONAL1") ? "" : opcional.Rows[0]["OPCIONAL1"].ToString();
                ef.Opcional[1] = !opcional.Columns.Contains("OPCIONAL2") ? "" : opcional.Rows[0]["OPCIONAL2"].ToString();
                ef.Opcional[2] = !opcional.Columns.Contains("OPCIONAL3") ? "" : opcional.Rows[0]["OPCIONAL3"].ToString();
                ef.Opcional[3] = !opcional.Columns.Contains("OPCIONAL4") ? "" : opcional.Rows[0]["OPCIONAL4"].ToString(); //Se cambio porque son los mismos valores que el Usuario
                ef.Opcional[4] = !opcional.Columns.Contains("OPCIONAL5") ? "" : opcional.Rows[0]["OPCIONAL5"].ToString();
                ef.Opcional[5] = !opcional.Columns.Contains("OPCIONAL6") ? "" : opcional.Rows[0]["OPCIONAL6"].ToString();
                ef.Opcional[6] = !opcional.Columns.Contains("OPCIONAL7") ? "" : opcional.Rows[0]["OPCIONAL7"].ToString();
                ef.Opcional[7] = !opcional.Columns.Contains("OPCIONAL8") ? "" : opcional.Rows[0]["OPCIONAL8"].ToString();
                ef.Opcional[8] = !opcional.Columns.Contains("OPCIONAL9") ? ef.ValorNeto : opcional.Rows[0]["OPCIONAL9"].ToString();
                if (ef.Descuento.Equals("0.00") || ef.Descuento.Equals("0"))
                    ef.Opcional[8] = ef.Total;                
                NumeroALetras numLetras = new NumeroALetras();
                ef.TotalLetras = numLetras.ConvertirNumeroALetras(ef.Total);
                #endregion
                #region INGRESA DATOS A OPCONAL
                InsertaEtiqueta("OPCIONAL1", ef.Opcional[0],false);
                InsertaEtiqueta("OPCIONAL2", ef.Opcional[1],true);
                InsertaEtiqueta("OPCIONAL3", ef.Opcional[2],true);
                InsertaEtiqueta("OPCIONAL4", ef.Opcional[3],false);
                InsertaEtiqueta("OPCIONAL5", ef.Opcional[4],true);
                InsertaEtiqueta("OPCIONAL6", ef.Opcional[5],false);
                InsertaEtiqueta("OPCIONAL7", ef.Opcional[6],false);
                InsertaEtiqueta("OPCIONAL8", ef.Opcional[7],false);   
                InsertaEtiqueta("OPCIONAL9", (!String.IsNullOrEmpty(ef.Opcional[8]))? formatoMoneda( Math.Abs(Double.Parse(ef.Opcional[8])).ToString() ): formatoMoneda( ef.Opcional[8] ),false);
                if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                {
                    for (int k=9;k<19;k++)
                    InsertaEtiqueta(String.Format("OPCIONAL{0}",(k+1).ToString()),ef.Opcional[k], false);   
                }
                InsertaEtiqueta("TELEFONO", ef.Telefono,false);
                InsertaEtiqueta("TOTAL_LETRAS", ef.TotalLetras,true);
                #endregion
                XmlDoc.WriteEndElement();//fin opcional
                XmlDoc.WriteStartElement("DETALLE");                
                #region INSERTA DETALLE
                int totalLineasDetalle = detalle.Rows.Count;
                int contador=0;
                foreach (DataRow dr in detalle.Rows)
                {
                    contador++;
                    XmlDoc.WriteStartElement("LINEA");
                    if (contador == totalLineasDetalle && (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina)))
                    {
                        InsertaEtiqueta("CANTIDAD", "1", false);
                        InsertaEtiqueta("DESCRIPCION", dr["DESCRIPCION"].ToString(), true);
                        InsertaEtiqueta("METRICA", (TipoDocumento.Equals("TS")) ? "" : dr["METRICA"].ToString(), true);
                        InsertaEtiqueta("PRECIOUNITARIO", ef.Total, false);
                        InsertaEtiqueta("VALOR", formatoMoneda( ef.Total ), false);
                        InsertaEtiqueta("DETALLE1", dr["DETALLE1"].ToString(), false);
                    }
                    else if (contador < totalLineasDetalle && (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado)) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                    {
                        InsertaEtiqueta("CANTIDAD", (TipoDocumento.Equals("TS")) ? "0" : dr["CANTIDAD"].ToString(), false);
                        InsertaEtiqueta("DESCRIPCION", dr["DESCRIPCION"].ToString(), true);
                        InsertaEtiqueta("METRICA", (TipoDocumento.Equals("TS")) ? "" : dr["METRICA"].ToString(), true);                        
                        InsertaEtiqueta("PRECIOUNITARIO", "0.00", false);
                        InsertaEtiqueta("VALOR", "0.00", false);
                        InsertaEtiqueta("DETALLE1", dr["DETALLE1"].ToString(), false);
                    }
                    else if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Repuesto) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Taller))
                    {
                        InsertaEtiqueta("CANTIDAD", (TipoDocumento.Equals("TS")) ? "0" : dr["CANTIDAD"].ToString(), false);
                        InsertaEtiqueta("DESCRIPCION", dr["DESCRIPCION"].ToString(), true);
                        InsertaEtiqueta("METRICA", (TipoDocumento.Equals("TS")) ? "" : dr["METRICA"].ToString(), true);                                    
                        InsertaEtiqueta("PRECIOUNITARIO", String.Format("{0:0.00}", Double.Parse(dr["PRECIOUNITARIO"].ToString())), false);
                        InsertaEtiqueta("VALOR", formatoMoneda( dr["VALOR"].ToString() ), false);
                        InsertaEtiqueta("DETALLE1", dr["DETALLE1"].ToString(), false);
                    }
                    XmlDoc.WriteEndElement();//fin linea
                }

                #endregion                
                XmlDoc.WriteEndElement(); //fin detalle                
                XmlDoc.WriteEndElement(); //fin factura 
            XmlDoc.WriteEndDocument();
            XmlDoc.Flush();
                       
            }
            catch (Exception e)
            {
                throw new Exception("Error en getXML x058 - " + String.Format(DESCRIPCION_ERROR,NumeroFactura));
            }
            finally
            {
                if (XmlDoc != null)
                {
                    XmlDoc.Close();
                    XmlStr.Close();
                }
            }
            string texto = "";
            texto = XmlStr.ToString();
            //System.IO.StreamReader sr = new System.IO.StreamReader(NombreArchivo);
            //texto = sr.ReadToEnd();
            //sr.Close();
            
            return texto;
            
        }      
        protected string construirCuenta(DataTable cuenta)
        {
            String strRetorno = string.Empty;
            if (cuenta.Rows.Count > 0)
            {
                foreach (DataRow dr in cuenta.Rows)
                {
                    strRetorno += dr["GCOCLA"].ToString();
                    strRetorno += ".";
                    int tam = dr["GSUCOD"].ToString().Trim().Length;
                    string aux = "";
                    if (tam < 3)
                    {
                        for (int c1 = 0; c1 < (3 - tam); c1++)
                            aux += "0";
                    }
                    aux += dr["GSUCOD"].ToString();
                    aux += ".";
                    strRetorno += aux;

                    tam = dr["GMACOD"].ToString().Trim().Length;
                    aux = "";
                    if (tam < 3)
                    {
                        for (int c1 = 0; c1 < (3 - tam); c1++)
                            aux += "0";
                    }
                    aux += dr["GMACOD"].ToString();
                    aux += ".";
                    strRetorno += aux;

                    tam = dr["GIGCOD"].ToString().Trim().Length;
                    aux = "";
                    if (tam < 4)
                    {
                        for (int c1 = 0; c1 < (4 - tam); c1++)
                            aux += "0";
                    }
                    aux += dr["GIGCOD"].ToString();
                    aux += "     ";
                    aux += dr["AVLTOT"].ToString();
                    aux += (cuenta.Rows.Count==1)?"":"|";
                    strRetorno += aux;
                }
            }
            return (cuenta.Rows.Count==5)?strRetorno.Substring(0,strRetorno.Length-1):strRetorno;
        }
        protected void InsertaEtiqueta(String etiqueta, String contenido,bool cdata)
        {   
            XmlDoc.WriteStartElement(etiqueta);
            if (cdata)
            {
                XmlDoc.WriteCData(contenido);  
            }
            else
            {
                XmlDoc.WriteString(contenido);  
            }
            
            XmlDoc.WriteEndElement();
        }
        private string obtieneCaja(String Cia, String Correlativo)
        {
            string caja = "";
            AccesoDatos cmp = new AccesoDatos();
            string qry = String.Format(Query.CajaFactura, Cia, Correlativo);
            DataTable Caja = cmp.RealizaConsulta(qry);
            if (Caja.Rows.Count > 0)
            {
                caja = Caja.Rows[0]["CAJA"].ToString();
            }
            else
            {
                caja = "S/N";
            }
            return caja;
        }
        private string obtieneDatosSAT(String Cia, String CentroCosto, String NumFac, String Dato, String tDocIfacere, String pSerie)
        {
            string resultado = "";
            string qry ="";
            AccesoDatos add = new AccesoDatos();
            if (TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoNuevo) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.VehiculoUsado) || TipoFactura.Equals(PFACW0001.ENUMS.TFactura.Oficina))
                qry = String.Format(Query.DatosSATVOFN, Cia, CentroCosto,pSerie);
            else
                qry = String.Format(Query.DatosSAT, Cia);
            DataTable Sat = add.RealizaConsulta(qry);
                if (Dato.Equals("RES"))//Resolucion
                {
                    resultado = getDataSQLSvr("RES", Cia, CentroCosto, NumFac, tDocIfacere, pSerie);
                }
                if (Dato.Equals("SER"))//Serie
                {
                    resultado = getDataSQLSvr("SER", Cia, CentroCosto, NumFac, tDocIfacere, pSerie);
                }
                if (Dato.Equals("EMP"))//Empresa
                {
                    if (Sat.Rows.Count > 0)
                    {
                        resultado = Sat.Rows[0]["EMPRESA"].ToString();
                    }
                    else
                    {
                        resultado = "S/D";
                    }
                }
                if (Dato.Equals("SUC"))//Sucursal
                {
                    resultado = getDataSQLSvr("SUC", Cia, "", "", tDocIfacere, pSerie);
                }
            
            return resultado;
        }
        private string getDataSQLSvr(String pDato, String emp, String suc, String numfac, String tDocIfacere,String serie)
        {
            string retorno="";
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLEXPRESS"].ConnectionString);
            try
            {
                
                myConnection.Open();
                SqlCommand commando = myConnection.CreateCommand();
                string qry = "";
                if (pDato.Equals("SER"))
                {
                    if(tDocIfacere.Equals("4"))
                    {
                        qry = String.Format("Select IdSerie from SSO_GFC_FAC_SERIE where IdEmpresa = '{0}' and IdSucursal = '{1}' and ({2} >= DocumentoInicio) and ({2} <= DocumentoFin) AND IdSerie='{3}'", emp, suc, numfac, serie);
                    }
                    if (tDocIfacere.Equals("5"))
                    {
                        qry = String.Format("Select IdSerie from SSO_GFC_NC_SERIE where IdEmpresa = '{0}' and IdSucursal = '{1}' and ({2} >= DocumentoInicio) and ({2} <= DocumentoFin) AND IdSerie='{3}'", emp, suc, numfac, serie);
                    }
                }
                if (pDato.Equals("RES"))
                {
                    if (tDocIfacere.Equals("4"))
                    {
                        qry = String.Format("Select Resolucion from SSO_GFC_FAC_SERIE where IdEmpresa = '{0}' and IdSucursal = '{1}' and ({2} >= DocumentoInicio) and ({2} <= DocumentoFin) AND IdSerie='{3}'", emp, suc, numfac, serie);
                    }
                    if (tDocIfacere.Equals("5"))
                    {
                        qry = String.Format("Select Resolucion from SSO_GFC_NC_SERIE where IdEmpresa = '{0}' and IdSucursal = '{1}' and ({2} >= DocumentoInicio) and ({2} <= DocumentoFin) AND IdSerie='{3}'", emp, suc, numfac, serie);
                    }
                }
                if (pDato.Equals("SUC"))
                {
                    if (tDocIfacere.Equals("4"))
                    {
                        qry = String.Format("select distinct(idsucursal) as sucursal from sso_gfc_fac_serie where idserie = '{0}' and idEmpresa={1}", serie, emp);
                    }
                    if (tDocIfacere.Equals("5"))
                    {
                        qry = String.Format("select distinct(idsucursal) as sucursal from sso_gfc_nc_serie where idserie = '{0}' and idEmpresa={1}", serie, emp);
                    }
                }
                commando.CommandText = qry;
                retorno = commando.ExecuteScalar().ToString();               
            }            
            catch (Exception err)
            {
                myConnection.Close();
                if (string.IsNullOrEmpty(retorno))
                {
                    if (pDato.Equals("RES"))
                    {
                        DESCRIPCION_ERROR = " [Error xDataSQLSvr al obtener la resolución del documento {0}]";
                        throw new Exception(DESCRIPCION_ERROR);
                    }
                    else if (pDato.Equals("SUC"))
                    {
                        DESCRIPCION_ERROR = " [Error xDataSQLSvr al obtener la sucursal del documento {0}]";
                        throw new Exception(DESCRIPCION_ERROR);
                    }
                }
                //throw new Exception(err.Message); ;
            }
            finally
            {
                myConnection.Close();
            }
            return retorno;
        }
        /// <summary>
        /// Obtiene las iniciales de la compania a partir de su codigo
        /// </summary>
        /// <param name="codCompania">codigo de la compania (ej. 45,66,130)</param>
        /// <returns>retorna las iniciales de la compania (ej. cs,ac,rc)</returns>
        private string obtenerCompania(string codCompania)
        {
            string compania = "";
            switch (codCompania)
            {
                case "45": compania = "CS";
                    break;
                case "66": compania = "AC";
                    break;
                case "130": compania = "RC";
                    break;
                case "192": compania = "RE";
                    break;
                case "199": compania = "AA";
                    break;
                case "280": compania = "CA";
                    break;
                case "281": compania = "SF";
                    break;
                case "282": compania = "NA";
                    break;
                case "283": compania = "VL";
                    break;
                case "284": compania = "SA";
                    break;
                case "CS": compania = "45";
                    break;
                case "AC": compania = "66";
                    break;
                case "RC": compania = "130";
                    break;
                case "RE": compania = "192";
                    break;
                case "AA": compania = "199";
                    break;
                case "CA": compania = "280";
                    break;
                case "SF": compania = "281";
                    break;
                case "NA": compania = "282";
                    break;
                case "VL": compania = "283";
                    break;
                case "SA": compania = "284";
                    break;
            }
            return compania;
        }
        /// <summary>
        /// Verifica si la serie es de Notas o Facturas
        /// </summary>
        /// <param name="pSerie">Serie a verificar</param>
        /// <returns>true si es parte de notas o facturas</returns>
        private bool mVerificaTipoSerie(string pSerie)
        {
            bool bResultado = false;                        
            DataTable dtRespuesta = new DataTable();
            if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                dtRespuesta = (DataTable)(getDataSQLSvr(String.Format(Query.SerieNCR,obtenerCompania(Cia),pSerie)));
            else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                dtRespuesta = (DataTable)(getDataSQLSvr(String.Format(Query.SerieFAC, obtenerCompania(Cia), pSerie)));
            bResultado = (dtRespuesta.Rows.Count>0)?true:false;
            return bResultado;
        }
        public string obtieneNumFact(String gnufac, string Numero_Serie)
        {
            try
            {
                string nuevoValor = "";
                //DataTable series = ad.RealizaConsulta(Query.SeriesFgen038);
                if (series.Rows.Count > 0)
                {
                    for (int i = 0; i < series.Rows.Count; i++)
                    {
                        if (gnufac.Trim().StartsWith(series.Rows[i]["SERIE"].ToString().Trim()) && !String.IsNullOrEmpty(series.Rows[i]["SERIE"].ToString()))
                        {
                            if (Numero_Serie.Equals("S"))
                            {
                                nuevoValor = series.Rows[i]["SERIE"].ToString();
                                break;
                            }
                            if (Numero_Serie.Equals("N"))
                            {
                                nuevoValor = gnufac.Substring(series.Rows[i]["SERIE"].ToString().Length);
                                break;
                            }
                        }
                    }
                }
                if (String.IsNullOrEmpty(nuevoValor))
                {
                    String desc = (Numero_Serie.Equals("N")) ? "el numero" : "la serie";
                    DESCRIPCION_ERROR = String.Format(" [Error al obtener {0} de documento xNumFact {1}]", desc, gnufac);
                    throw new Exception(DESCRIPCION_ERROR);                
                }
                else
                {
                    //if (Numero_Serie=="S")
                    //    if (!mVerificaTipoSerie(nuevoValor))
                    //    {
                    //        if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.NotaCredito))
                    //        {
                    //            DESCRIPCION_ERROR = String.Format(" [Error xDataSQLSvr] La serie {0} no es una serie de Notas de crédito de {1}",nuevoValor,Cia);
                    //            throw new Exception(DESCRIPCION_ERROR);
                    //        }
                    //        else if (TipoDocumento.Equals(PFACW0001.ENUMS.TDocumento.Factura))
                    //        {
                    //            DESCRIPCION_ERROR = String.Format(" [Error xDataSQLSvr] La serie {0} no es una serie de Facturas de {1}", nuevoValor, Cia);
                    //            throw new Exception(DESCRIPCION_ERROR);
                    //        }
                    //    }
                    return nuevoValor;
                }                 
            }
            catch (Exception err)
            {
                throw new Exception();
            }
        }
        private string formatoMoneda(String pValor)
        {
            return String.Format("{0:#,##0.00;(#,##0.00);0.00}", (string.IsNullOrEmpty(pValor)) ? "0" : pValor);
        }
        private object getDataSQLSvr(string Query)
        {
            Object oRetorno = "";
            DataTable _tabla;
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLEXPRESS"].ConnectionString);
            try
            {
                myConnection.Open();
                SqlCommand commando = myConnection.CreateCommand();                
                commando.CommandText = Query;
                SqlDataReader _reader = commando.ExecuteReader();
                _tabla = new DataTable();
                _tabla.Load(_reader);
                oRetorno = _tabla;
            }
            catch (Exception err)
            {
                myConnection.Close();
                DESCRIPCION_ERROR = " [Error xDataSQLSvr] "+err.ToString();
                throw new Exception(DESCRIPCION_ERROR);
            }
            finally
            {
                myConnection.Close();                
            }
            return oRetorno;
        }
        public bool CDI_F_VerificaExistenciaSerie(string pSerie)
        {            
            bool bResultado = false;
            DataRow[] dr = this.series.Select(String.Format("SERIE='{0}'", pSerie));
            bResultado = (dr.Length > 0) ? true : false;            
            return bResultado;
        }
    }
}
