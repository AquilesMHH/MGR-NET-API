using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;
using MGR_Common.com.pe.mgr.common.constants;
using System.Reflection;
using MGR_Persistence.com.pe.mgr.dao.SqlString;
using System.Configuration;

namespace MGR_Persistence.com.pe.mgr.dao.impl
{

    public class LoginDaoImpl : LoginDao, IDisposable 
    {
        private ModelMGRContext context;
        SqlDataTableDaoImpl sqlData;
        OracleConect oracleConex;
        public string conn =  ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public LoginDaoImpl()
        {
            context = new ModelMGRContext();
            sqlData = new SqlDataTableDaoImpl();
            oracleConex = new OracleConect();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
       
        public MgrLoginBean login(GRTA_USUARIO obj, string ip, string token)
        {
            MgrLoginBean mgrLoginBean = new MgrLoginBean();
            int pIdUsuario = 0;
            string sqlQuery = "";
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {

                try
                {
                   // var vGRTA_USUARIO = context.Database.SqlQuery<GRTA_USUARIO>(MgrEnumConsultaGeneral.MgrVerificaUsuario(obj),pIdUsuario).FirstOrDefault();
                    DataSet dataSet = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrVerificaUsuario(obj), System.Data.CommandType.Text,  null);
                    if (dataSet != null)
                    {
                        DataTable objData  = dataSet.Tables["ds"];
                        pIdUsuario =(int) objData.Rows[0][0];
                    }
                    GRTA_USUARIO varUsuario = (from t in context.GRTA_USUARIO where t.ID_USUARIO == pIdUsuario orderby t.ID_USUARIO descending select t).SingleOrDefault();

                    UsuarioDto usuarioDto = new UsuarioDto();
                    if (varUsuario != null) {
                        usuarioDto.primer_nombre = varUsuario.PRIMER_NOMBRE;
                        usuarioDto.segundo_nombre = varUsuario.SEGUNDO_NOMBRE;
                        usuarioDto.primer_apellido = varUsuario.PRIMER_APELLIDO;
                        usuarioDto.segundo_apellido = varUsuario.SEGUNDO_APELLIDO;
                        usuarioDto.email = varUsuario.EMAIL;
                        usuarioDto.foto = varUsuario.FOTO;
                        usuarioDto.usuario_administrador = varUsuario.USUARIO_ADMINISTRADOR > 0 ? true : false;
                    }
                    var varRol = context.Database.SqlQuery<String>(MgrEnumConsultaGeneral.MgrUsuarioRol,
                                          new OracleParameter("@pIdUsuario", pIdUsuario)).ToList();
                    OracleParameter[] parameters = new OracleParameter[] {
                                 new OracleParameter("@pIdUsuario", pIdUsuario) };
                    DataSet dsFuncionalidad = MGR_Common.OracleHelper.Query(conn, MgrEnumConsultaGeneral.MgrUsuarioFuncionalidad, System.Data.CommandType.Text, parameters);
                    List<GrtaFuncionalidad> objListar = new List<GrtaFuncionalidad>();
                    if (dsFuncionalidad != null) {
                        objListar = dsFuncionalidad.Tables[0].DataTableToList<GrtaFuncionalidad>();
                    }
                    Dictionary<String, List<GrtaFuncionalidad>> mapPermiso = new Dictionary<String, List<GrtaFuncionalidad>>();
                    List<GrtaFuncionalidad> lstFuncionalidadMenu = new List<GrtaFuncionalidad>();
                    List<GrtaFuncionalidad> lstFuncionalidadFormulario = new List<GrtaFuncionalidad>();
                    List<GrtaFuncionalidad> lstFuncionalidadDialogo = new List<GrtaFuncionalidad>();
                    List<GrtaFuncionalidad> lstFuncionalidadProcesoInterno = new List<GrtaFuncionalidad>();
                    foreach (GrtaFuncionalidad vFnldad in objListar)
                    {
                        if ((vFnldad.ID_FUNCIONALIDAD_PADRE == null) && (vFnldad.TIPO.Equals(TipoFuncionalidad.M))) {
                            List<GrtaFuncionalidad> lstFuncionalidadSubMenu = new List<GrtaFuncionalidad>();
                            foreach (var vFnldadHijo in objListar)
                            {
                                if ((vFnldad.ID_FUNCIONALIDAD_PADRE == null) 
                                    && (vFnldad.ID_FUNCIONALIDAD_PADRE == vFnldad.ID_FUNCIONALIDAD) 
                                    && (vFnldad.TIPO.Equals(TipoFuncionalidad.SM)))
                                {
                                    lstFuncionalidadSubMenu.Add(vFnldadHijo);

                                }
                            }
                            vFnldad.Lst_funcionalidad_hijas.AddRange(lstFuncionalidadSubMenu);
                            lstFuncionalidadMenu.Add(vFnldad);
                            //lstFuncionalidadSubMenu.add
                        }
                    }


                    mgrLoginBean.StrIdUsuario = varUsuario.ID_USUARIO;
                    mgrLoginBean.StrUsuario = varUsuario.USUARIO;
                    mgrLoginBean.usuario = varUsuario;
                    mgrLoginBean.strIdSession= 1;
                    mgrLoginBean.intValido = 1;
                    mgrLoginBean.lstRoles = varRol;
                    mgrLoginBean.Permiso = mapPermiso;
                    mgrLoginBean.token= token;
                    return mgrLoginBean;
                  
                }
                catch (Exception ext)
                {
                    string valor = ext.ToString();
                    dbContextTransaction.Rollback();
                }
            }
            return null;
        }

        public DataTable GetDataTable(string sqlQuery)
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(context.Database.Connection);

                using (var cmd = factory.CreateCommand())
                {
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = context.Database.Connection;
                    using (var adapter = factory.CreateDataAdapter())
                    {
                        adapter.SelectCommand = cmd;

                        var tb = new DataTable();
                        adapter.Fill(tb);
                        return tb;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public FuncionalidadDto obtenerFuncionalidadDerecho(string usuario, string codigo_funcionalidad)
        {
            return null;
        }

           
    }

    
}
/*
 * 
 * 
                    var varUsuario = context.GRTA_USUARIO.SqlQuery(MgrEnumConsultaGeneral.MgrUsuarioConsulta,
                        new OracleParameter("@pIdUsuario", pIdUsuario)).FirstOrDefault();

 *  var usuarioObj = new OracleParameter("@pUsuario", obj.USUARIO);
                    var claveObj = new OracleParameter("@pClave", obj.CLAVE);
                    OracleParameter[] parametersS = new OracleParameter[] {
                                 new OracleParameter("@pUsuario", obj.USUARIO),
                                 new OracleParameter("@pClave", obj.CLAVE)    };

                    String sql = MgrEnumConsultaGeneral.MgrVerificaUsuario;
                    DataTable retVal = new DataTable();

                    Oracle.ManagedDataAccess.Client.OracleParameter[] parmss = 
                        new Oracle.ManagedDataAccess.Client.OracleParameter[]{
                            new Oracle.ManagedDataAccess.Client.OracleParameter("@pUsuario",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10),
                            new Oracle.ManagedDataAccess.Client.OracleParameter("@pClave",Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2,10)
                            };
                    parmss[0].Value = obj.USUARIO;
                    parmss[1].Value = obj.CLAVE;
 *    DbProviderFactory factory = DbProviderFactories.GetFactory(context.Database.Connection);
                    using (var cmd = factory.CreateCommand())
                    {
                        cmd.CommandText = SqlStr;
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = context.Database.Connection;
                        using (var adapter = factory.CreateDataAdapter())
                        {
                            adapter.SelectCommand = cmd;

                            var tb = new DataTable();
                            adapter.Fill(tb);
                           
                        }
                    }
                     var varFunciones = sqlData.GetDataTable(MgrEnumConsultaGeneral.MgrUsuarioFuncionalidad, parameters);
  


                    //var vGRTA_USUARIO = context.Database.SqlQuery<DataTable>(sqlQuery, pIdUsuario).ToList();
                    //var query = context.GRTA_USUARIO.Where(p => p.ID_USUARIO== pIdUsuario);
                    GRTA_USUARIO objTable = (from t in context.GRTA_USUARIO
                                   where t.ID_USUARIO == pIdUsuario
                                       orderby t.ID_USUARIO descending
                                   select t).SingleOrDefault();

 * */
