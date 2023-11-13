namespace ConsolaBanco;

public abstract class Persona
{
    public abstract string ObtenerNombre();

    public static string ObtenerPais()
    {
        return "México";
    }
}

public interface IPersona
{
    string ObtenerNombre();
    string ObtenerPais();
}