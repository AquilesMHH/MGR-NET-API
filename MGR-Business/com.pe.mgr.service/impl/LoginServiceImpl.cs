using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR_Entidades.Entidad;
using MGR_Persistence.com.pe.mgr.dao.impl;

namespace MGR_Business.com.pe.mgr.service.impl
{

    public class LoginServiceImpl : LoginService, IDisposable
    {
        private LoginDaoImpl _loginDao;
        public LoginServiceImpl()
        {
            _loginDao = new LoginDaoImpl();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
       
        public MgrLoginBean login(GRTA_USUARIO usuario, string ip, string token)
        {
           return  _loginDao.login(usuario, ip, token);
        }

        public FuncionalidadDto obtenerFuncionalidadDerecho(string usuario, string codigo_funcionalidad)
        {
            throw new NotImplementedException();
        }
    }
}
