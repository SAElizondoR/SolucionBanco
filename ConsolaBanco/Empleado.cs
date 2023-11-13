namespace ConsolaBanco;

public class Empleado : Usuario, IPersona
{
    public string Departamento { get; set; } = string.Empty;

    public Empleado() {}

    public Empleado(int Id, string Nombre, string CorreoE, decimal Saldo,
        string Departamento) : base(Id, Nombre, CorreoE)
    {
        this.Departamento = Departamento;
        Depositar(Saldo);
    }

    public override void Depositar(decimal monto)
    {
        base.Depositar(monto);

        if (!string.IsNullOrEmpty(Departamento))
        {
            if (Departamento.Equals("TI"))
            Saldo += monto * 0.05m;
        }
    }

    public override string MostrarDatos()
    {
        return base.MostrarDatos()
            + $", Departamento: {this.Departamento}";
    }

    public new string ObtenerNombre()
    {
        return Nombre + "!";
    }
}