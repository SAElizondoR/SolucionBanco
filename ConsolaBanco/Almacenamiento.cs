using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsolaBanco;

public static class Almacenamiento
{
    static readonly string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory
        + "/usuarios.json";
    
    private static void Actualizar<T>(List<T> listaUsuarios)
    {
        JsonSerializerSettings ajustes
            = new() { Formatting = Formatting.Indented };

        string objeto = JsonConvert.SerializeObject(listaUsuarios, ajustes);

        File.WriteAllText(rutaArchivo, objeto);
    }
    
    public static void Agregar(Usuario usuario)
    {
        var listaUsuarios = new List<object>();

        try
        {
            listaUsuarios = ObtenerListaObjetos();
        }
        catch (JsonSerializationException){}

        listaUsuarios ??= new List<object>();
        
        listaUsuarios.Add(usuario);

        Actualizar(listaUsuarios);
    }

    public static void BorrarArchivo() {
        File.Delete(rutaArchivo);
    }

    private static List<Usuario> ObtenerUsuarios()
    {
        var listaUsuarios = new List<Usuario>();
        var listaObjetos = ObtenerListaObjetos();

        if (listaObjetos == null)
            return listaUsuarios;
        
        foreach (object objeto in listaObjetos)
        {
            Usuario? usuarioNuevo;
            JObject usuario = (JObject)objeto;

            if (usuario.ContainsKey("RegimenFiscal"))
                usuarioNuevo = usuario.ToObject<Cliente>();
            else
                usuarioNuevo = usuario.ToObject<Empleado>();
            
            if (usuarioNuevo != null)
                listaUsuarios.Add(usuarioNuevo);
        }

        return listaUsuarios;
    }

    public static List<Usuario> ObtenerUsuariosNuevos()
    {
        var listaUsuarios = ObtenerUsuarios();
        var listaUsuariosNuevos = listaUsuarios.Where(
            usuario => usuario.ObtenerFechaRegistro().Date
            .Equals(DateTime.Today)).ToList();

        return listaUsuariosNuevos;
    }

    public static string BorrarUsuario(int id)
    {
        var listaUsuarios = ObtenerUsuarios();
        var usuarioABorrar = listaUsuarios.Where(
            usuario=> usuario.ObtenerId() == id).Single();
        listaUsuarios.Remove(usuarioABorrar);
        Actualizar(listaUsuarios);
        return "Exito";
    }

    private static List<object>? ObtenerListaObjetos()
    {
        string usuariosEnArchivo = "";

        if (File.Exists(rutaArchivo))
            usuariosEnArchivo = File.ReadAllText(rutaArchivo);

        return JsonConvert.DeserializeObject<List<object>>(usuariosEnArchivo);
    }
}