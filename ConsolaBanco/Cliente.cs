namespace ConsolaBanco;

public class Cliente : Usuario, IPersona
{
    public char RegimenFiscal { get; set; }

    public Cliente() {}

    public Cliente(int Id, string Nombre, string CorreoE, decimal Saldo,
        char RegimenFiscal) : base(Id, Nombre, CorreoE)
    {
        this.RegimenFiscal = RegimenFiscal;
        this.Depositar(Saldo);
    }

    public override void Depositar(decimal monto)
    {
        base.Depositar(monto);

        if (RegimenFiscal.Equals('M'))
            Saldo += Saldo * 0.02m;
    }

    public override string MostrarDatos()
    {
        return base.MostrarDatos()
            + $", Régimen Fiscal: {this.RegimenFiscal}";
    }
}