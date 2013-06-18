using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PFACW0001.Estructura
{
    public class Factura
    {
        #region variables y encapsulamiento
        #region FACTURA
        private DataSet _dsFactura;

        public DataSet DsFactura
        {
            get { return _dsFactura; }
            set { _dsFactura = value; }
        }
        private DataTable _dtEncabezado;

        public DataTable DtEncabezado
        {
            get { return _dtEncabezado; }
            set { _dtEncabezado = value; }
        }
        private DataTable _dtOpcinoal;

        public DataTable DtOpcinoal
        {
            get { return _dtOpcinoal; }
            set { _dtOpcinoal = value; }
        }
        private DataTable _dtDetalle;

        public DataTable DtDetalle
        {
            get { return _dtDetalle; }
            set { _dtDetalle = value; }
        }
        private String _noFactura;

        public String NoFactura
        {
            get { return _noFactura; }
            set { _noFactura = value; }
        }
        private String _resolucion;

        public String Resolucion
        {
            get { return _resolucion; }
            set { _resolucion = value; }
        }
        private String _idSerie;

        public String IdSerie
        {
            get { return _idSerie; }
            set { _idSerie = value; }
        }
        private String _empresa;

        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        private String _sucursal;

        public String Sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }
        private String _caja;

        public String Caja
        {
            get { return _caja; }
            set { _caja = value; }
        }
        private String _usuario;

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private String _fechaEmision;

        public String FechaEmision
        {
            get { return _fechaEmision; }
            set { _fechaEmision = value; }
        }
        private String _generacion;

        public String Generacion
        {
            get { return _generacion; }
            set { _generacion = value; }
        }
        private String _moneda;

        public String Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }
        private String _tasaCambio;

        public String TasaCambio
        {
            get { return _tasaCambio; }
            set { _tasaCambio = value; }
        }
        private String _nombreContribuyente;

        public String NombreContribuyente
        {
            get { return _nombreContribuyente; }
            set { _nombreContribuyente = value; }
        }
        private String _direccionContribuyente;

        public String DireccionContribuyente
        {
            get { return _direccionContribuyente; }
            set { _direccionContribuyente = value; }
        }
        private String _nitContribuyente;

        public String NitContribuyente
        {
            get { return _nitContribuyente; }
            set { _nitContribuyente = value; }
        }
        private String _valorNeto;

        public String ValorNeto
        {
            get { return _valorNeto; }
            set { _valorNeto = value; }
        }
        private String _iva;

        public String IVA
        {
            get { return _iva; }
            set { _iva = value; }
        }
        private String _total;

        public String Total
        {
            get { return _total; }
            set { _total = value; }
        }
        private String _descuento;

        public String Descuento
        {
            get { return _descuento; }
            set { _descuento = value; }
        }
        private String _exento;

        public String Exento
        {
            get { return _exento; }
            set { _exento = value; }
        }
        private String[] _opcional;

        public String[] Opcional
        {
          get { return _opcional; }
          set { _opcional = value; }
        }
        private String _telefono;

        public String Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        private String _totalLetras;

        public String TotalLetras
        {
          get { return _totalLetras; }
          set { _totalLetras = value; }
        }
        #endregion
        #region NOTA DE CREDITO
        private string _noDocumento;

        public string NODOCUMENTO
        {
            get { return _noDocumento; }
            set { _noDocumento = value; }
        }
        private string _serieFactura;

        public string SERIEFACTURA
        {
            get { return _serieFactura; }
            set { _serieFactura = value; }
        }
        private string _concepto;

        public string CONCEPTO
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        private string _fechaFactura;

        public string FECHAFACTURA
        {
            get { return _fechaFactura; }
            set { _fechaFactura = value; }
        }
        private string _cargar;

        public string CARGAR
        {
            get { return _cargar; }
            set { _cargar = value; }
        }
        private string _abonar;

        public string ABONAR
        {
            get { return _abonar; }
            set { _abonar = value; }
        }

        #endregion
        #region FACTURA ANULADA
        private string _razonanulacion;

        public string Razonanulacion
        {
            get { return _razonanulacion; }
            set { _razonanulacion = value; }
        }

        #endregion
        #endregion

        public Factura()
        {
            DsFactura = new DataSet("FACTURA");
            DtEncabezado = new DataTable();
            DtEncabezado.TableName = "ENCABEZADO";
            DtOpcinoal = new DataTable();
            DtOpcinoal.TableName = "OPCIONAL";
            DtDetalle = new DataTable();
            DtDetalle.TableName = "DETALLE";
            NoFactura = String.Empty;
            Resolucion = String.Empty;
            IdSerie = String.Empty;
            Empresa = String.Empty;
            Sucursal = String.Empty;
            Caja = String.Empty;
            Usuario = String.Empty;
            FechaEmision = String.Empty;
            Generacion = String.Empty;
            Moneda = String.Empty;
            TasaCambio = String.Empty;
            NombreContribuyente = String.Empty;
            DireccionContribuyente = String.Empty;
            NitContribuyente = String.Empty;
            ValorNeto = String.Empty;
            IVA = String.Empty;
            Total = String.Empty;
            Descuento = String.Empty;
            Exento = String.Empty;
            Telefono=String.Empty;
            TotalLetras=String.Empty;
            Opcional = new String[29];
            for(int k=0;k<29;k++)
                Opcional[k]=String.Empty;
        }
        protected void generaEncabezado()
        {
            DtEncabezado.Columns.Add("NOFACTURA");
            DtEncabezado.Columns.Add("RESOLUCION");
            DtEncabezado.Columns.Add("IDSERIE");
            DtEncabezado.Columns.Add("EMPRESA");
            DtEncabezado.Columns.Add("SUCURSAL");
            DtEncabezado.Columns.Add("CAJA");
            DtEncabezado.Columns.Add("USUARIO");
            DtEncabezado.Columns.Add("FECHAEMISION");
            DtEncabezado.Columns.Add("GENERACION");
            DtEncabezado.Columns.Add("MONEDA");
            DtEncabezado.Columns.Add("TASACAMBIO");
            DtEncabezado.Columns.Add("NOMBRECONTRIBUYENTE");
            DtEncabezado.Columns.Add("DIRECCIONCONTRIBUYENTE");
            DtEncabezado.Columns.Add("NITCONTRIBUYENTE");
            DtEncabezado.Columns.Add("VALORNETO");
            DtEncabezado.Columns.Add("IVA");
            DtEncabezado.Columns.Add("TOTAL");
            DtEncabezado.Columns.Add("DESCUENTO");
            DtEncabezado.Columns.Add("EXENTO");
        }
        protected void LlenaEncabezado()
        {
            generaEncabezado();
            DataRow dr = DtEncabezado.NewRow();
            dr["NOFACTURA"] = NoFactura;
            dr["RESOLUCION"] = Resolucion;
            dr["IDSERIE"] = IdSerie;
            dr["EMPRESA"] = Empresa;
            dr["SUCURSAL"] = Sucursal;
            dr["CAJA"] = Caja;
            dr["USUARIO"] = Usuario;
            dr["FECHAEMISION"] = FechaEmision;
            dr["GENERACION"] = Generacion;
            dr["MONEDA"] = Moneda;
            dr["TASACAMBIO"] = TasaCambio;
            dr["NOMBRECONTRIBUYENTE"] = NombreContribuyente;
            dr["DIRECCIONCONTRIBUYENTE"] = DireccionContribuyente;
            dr["NITCONTRIBUYENTE"] = NitContribuyente;
            dr["VALORNETO"] = ValorNeto;
            dr["IVA"] = IVA;
            dr["TOTAL"] = Total;
            dr["DESCUENTO"] = Descuento;
            dr["EXENTO"] = Exento;
            DtEncabezado.Rows.Add(dr);
        }
        protected void generaOpcional()
        {
            DtOpcinoal.Columns.Add("OPCIONAL1");
            DtOpcinoal.Columns.Add("OPCIONAL2");
            DtOpcinoal.Columns.Add("OPCIONAL3");
            DtOpcinoal.Columns.Add("OPCIONAL4");
            DtOpcinoal.Columns.Add("OPCIONAL5");
            DtOpcinoal.Columns.Add("OPCIONAL6");
            DtOpcinoal.Columns.Add("OPCIONAL7");
            DtOpcinoal.Columns.Add("OPCIONAL8");
            DtOpcinoal.Columns.Add("OPCIONAL9");
            DtOpcinoal.Columns.Add("TELEFONO");
            DtOpcinoal.Columns.Add("TOTAL_LETRAS");
        }
        protected void LlenaOpcional()
        {
            NumeroALetras numLetras = new NumeroALetras();
            generaOpcional();
            DataRow dr = DtOpcinoal.NewRow();                        
            for (int k=0;k<9;k++)
                dr[k] = Opcional[k];
            dr["TELEFONO"] = Telefono;
            TotalLetras = numLetras.ConvertirNumeroALetras(Total);
            dr["TOTAL_LETRAS"] = TotalLetras;
            DtOpcinoal.Rows.Add(dr);
        }
        protected void generaDetalle()
        {
            DtDetalle.Columns.Add("LINEA");
            DtDetalle.Columns[0].Caption = "LINEA";            
            DtDetalle.Columns.Add("LINEA");                

        }
        protected void LlenaDetalle()
        {
            generaDetalle();
            DataRow dr = DtDetalle.NewRow();
            dr[0] = "1";                        
            DtDetalle.Rows.Add(dr);           
        }
        public DataSet ArmaFactura()
        {
            LlenaEncabezado();
            DsFactura.Tables.Add(DtEncabezado);
            LlenaOpcional();
            DsFactura.Tables.Add(DtOpcinoal);
            LlenaDetalle();
            DsFactura.Tables.Add(DtDetalle);
            return _dsFactura;
        }
    }
}
