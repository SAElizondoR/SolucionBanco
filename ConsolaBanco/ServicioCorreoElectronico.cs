using MailKit.Net.Smtp;
using MimeKit;

namespace ConsolaBanco;


public static class ServicioCorreoElectronico
{
    public static void EnviarCorreo()
    {
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("Checo",
            "saelizondor@gmail.com"));
        mensaje.To.Add(new MailboxAddress("Admin",
            "sergio.elizondord@uanl.edu.mx"));
        mensaje.Subject = "Consola bancaria: Usuarios nuevos";

        mensaje.Body = new TextPart("plain") {
            Text = ObtenerTextoCorreo()
        };

        using var cliente = new SmtpClient();
        cliente.Connect("smtp.gmail.com", 587, false);
        cliente.Authenticate("saelizondor@gmail.com", "");
        cliente.Send(mensaje);
        cliente.Disconnect(true);
    }

    private static string ObtenerTextoCorreo()
    {
        List<Usuario> usuariosNuevos = Almacenamiento.ObtenerUsuariosNuevos();

        if (usuariosNuevos.Count == 0)
            return "No hay usuarios nuevos.";
        
        string textoCorreo = "Usuarios agregados hoy:\n";

        foreach (Usuario usuario in usuariosNuevos)
            textoCorreo += "\t+ " + usuario.MostrarDatos() + "\n";
        
        return textoCorreo;
    }
}
