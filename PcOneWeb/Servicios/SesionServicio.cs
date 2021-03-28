using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PcOneWeb.Servicios
{
    public class SesionServicio
    {

        public bool CheckEmail(string email)
        {
            using (PcOneEntities db = new PcOneEntities())
            {
                try
                {
                    var result = (from data in db.Cliente
                                  where data.email.Equals(email)
                                  select data).ToList();
                    if (result.Count() == 0) { return true; }
                    else { return false; }
                }
                catch
                {
                    return false;
                }

            }
        }//
        public bool RegistarUsuario(Cliente c)
        {
            using (PcOneEntities db = new PcOneEntities())
            {
                try
                {
                    db.Cliente.Add(c);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
       public string ComprobarLogin(string email,string password)
        {
            using (PcOneEntities db = new PcOneEntities())
            {
                LoginData loginData = new LoginData();
                try
                {
                    var result = (from data in db.Cliente
                                 where data.email.Equals(email) && data.contrasena.Equals(password)
                                 select data).ToList();
                    

                    if(result.Count() == 0) {
                        loginData.ValidData = false;
                        return JsonConvert.SerializeObject(loginData);
                    } else {
                        loginData.ValidData = true;
                        loginData.Nombre = result[0].nombre;
                        loginData.Email = result[0].email;
                        return JsonConvert.SerializeObject(loginData);
                    }
                }
                catch
                {
                    loginData.ValidData = false;
                    return JsonConvert.SerializeObject(loginData);
                }
                
            }
        }
     }//Clase
}//NameSpace
public class LoginData
{
    public bool ValidData { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
}