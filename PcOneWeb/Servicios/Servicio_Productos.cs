using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcOneWeb.Servicios
{
    public class Servicio_Productos
    {
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public List<Producto> ObtenerProductos(int categoria,string param)
        {
            /* using (PcOneEntities db= new PcOneEntities())
                 {*/
                PcOneEntities db = new PcOneEntities();
                    try
                    {
                        if(categoria == 0)
                        {
                        return (from data in db.Producto
                            where data.nombre.Contains(param)
                            select data).ToList();
                        }
                        return (from data in db.Producto
                                where data.id_categoria.Value.Equals(categoria) && data.nombre.Contains(param)
                                select data).ToList(); 
                    }
                    catch (Exception  e)
                    {
                        return null; //Si fallo recibira una lista vacia
                    }
                //}
        }
        public Task<List<Categoria>> ObtenerCategoriasAsync()
        {
            return Task.Run(() =>
            {
                PcOneEntities db = new PcOneEntities();
                try
                {
                    return (from data in db.Categoria
                            select data).ToList();
                }
                catch (Exception e)
                {
                    return new List<Categoria>();
                }
            });
        }
        public Producto ObtenerProducto(int id)
        {
            PcOneEntities db = new PcOneEntities();
                try
                {
                    Producto producto =  db.Producto.Find(id);
                    return producto;
                }
                catch(Exception e)
                {
                    return null;
                }
           
        }
  
       
    }//Clase
}//NameSpace
