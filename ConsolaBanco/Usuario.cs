using Newtonsoft.Json;

namespace ConsolaBanco;

public class Usuario : IPersona
{
    [JsonProperty]
    protected int Id { get; set; }
    [JsonProperty]
    protected string Nombre { get; set; } = string.Empty;
    [JsonProperty]
    protected string CorreoE { get; set; } = string.Empty;
    [JsonProperty]
    protected decimal Saldo { get; set; }
    [JsonProperty]
    protected DateTime FechaRegistro { get; set; }

    public Usuario()
    {
        this.Saldo = 10;
    }

    public Usuario(int Id, string Nombre, string CorreoE)
    {
        this.Id = Id;
        this.Nombre = Nombre;
        this.CorreoE = CorreoE;
        this.FechaRegistro = DateTime.Now;
    }

    public int ObtenerId()
    {
        return Id;
    }

    public DateTime ObtenerFechaRegistro()
    {
        return FechaRegistro;
    }

    public virtual void Depositar(decimal monto)
    {
        decimal cantidad = 0;

        if (monto < 0)
            cantidad = 0;
        else
            cantidad = monto;
        
        this.Saldo += cantidad;
    }

    public virtual string MostrarDatos()
    {
        return $"Id: {this.Id}, Nombre: {this.Nombre}, " +
        $"Correo: {this.CorreoE}, Saldo: {this.Saldo}, " +
        $"Fecha de registro: {this.FechaRegistro.ToShortDateString()}";
    }

    public string MostrarDatos(string mensajeInicial)
    {
        return $"{mensajeInicial} -> {MostrarDatos()}";
    }

    public string ObtenerNombre()
    {
        return Nombre;
    }

    public string ObtenerPais()
    {
        return "Méixco";
    }
}
