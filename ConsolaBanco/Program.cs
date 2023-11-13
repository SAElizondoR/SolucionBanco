using System.Text.RegularExpressions;
using ConsolaBanco;

Console.WriteLine("¡Hola! Soy Checo.\nSoy de la RPDC.");

Almacenamiento.BorrarArchivo();

// Crear usuario 1
Cliente sergio = new(1, "Sergio", "sergio@mail.ru", 4000, 'M');
sergio.Depositar(-20);
Console.WriteLine(
    sergio.MostrarDatos("La inforamción del usuario es la siguiente"));
Almacenamiento.Agregar(sergio);

// Crear usuario 2
Empleado ricardo = new(2, "Ricardo", "ricardo@mail.ru", 4000, "TI");
ricardo.Depositar(2000);
Almacenamiento.Agregar(ricardo);
Console.WriteLine(ricardo.MostrarDatos());

// Crear usuario 3
Cliente jona = new(3, "Jona", "jona@mail.ru", 1500, 'M');
Almacenamiento.Agregar(jona);

if (args.Length == 0) {
  Console.WriteLine("Enviar correo.");
  ServicioCorreoElectronico.EnviarCorreo();
} else {
  Console.WriteLine($"Primer argumento: {args[0]}\n"
                    // + $"Tercer argumento: {args[2]}\n"
                    + "Mostrar menú.");
  MostrarMenu();
}

void MostrarMenu() {
  Console.Clear();
  Console.WriteLine("Selecciona una opción:\n1 - Crear un usuario nuevo.\n" +
                    "2 - Eliminar un usuario existente.\n3 - Salir.");

  byte opcion = 0;
  do {
    string? entrada = Console.ReadLine();

    if (!byte.TryParse(entrada, out opcion))
      Console.WriteLine("Debes ingresar un número (1, 2 ó 3).");
    else if (opcion < 1 || opcion > 3)
      Console.WriteLine("Debes ingresar un número válido (1, 2 ó 3).");
  } while (opcion < 1 || opcion > 3);

  switch (opcion) {
  case 1:
    CrearUsuario();
    break;
  case 2:
    BorrarUsuario();
    break;
  case 3:
    Environment.Exit(0);
    break;
  }

  Thread.Sleep(2000);

  MostrarMenu();
}

void CrearUsuario() {
  Console.Clear();

  int id = LeerId("Id", true);
  string? nombre = Leer("Nombre");
  string? correoElectronico = LeerCorreoElectronico();
  decimal saldo = LeerSaldo();
  char tipoUsuario = LeerTipoUsuario();

  Usuario usuarioNuevo;

  if (tipoUsuario.Equals('c')) {
    char regimenFiscal = LeerCaracter("Regimen fiscal");
    usuarioNuevo =
        new Cliente(id, nombre!, correoElectronico!, saldo, regimenFiscal);
  } else {
    string? departamento = Leer("Departamento");
    usuarioNuevo =
        new Empleado(id, nombre!, correoElectronico!, saldo, departamento!);
  }

  Almacenamiento.Agregar(usuarioNuevo);
}

void BorrarUsuario() {
  Console.Clear();

  int id = LeerId("Ingresa el id del usuario a eliminar", false);

  string resultado = Almacenamiento.BorrarUsuario(id);

  if (resultado.Equals("Exito")) {
    Console.Write("Usuario eliminado.");
  }
}


string Leer(string mensaje)
{
  Console.Write($"{mensaje}: ");
  return Console.ReadLine()!;
}

int LeerId(string mensaje, bool agregar)
{
  int id;
  do
  {
    if (!int.TryParse(Leer(mensaje), out id))
    {
      Console.WriteLine("Entrada inválida.");
      continue;
    }
    if (!EsPositivo(id))
    {
      Console.WriteLine("El id debe ser positivo.");
      continue;
    }
    // Si estamos agregando y existe el registro
    if (agregar && Almacenamiento.Existe(id)) 
    {
      Console.WriteLine("Ya existe un registro con el id ingresado.");
      continue;
    }
    // Si estamos borrando y no existe el registro
    if (!agregar && !Almacenamiento.Existe(id))
    {
      Console.WriteLine("No existe el registro con el id ingresado.");
      continue;
    }
    break;
  } while (true);
  return id;
}

string LeerCorreoElectronico()
{
  string correoElectronico;
  string patronCorreo = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
  do
  {
    correoElectronico = Leer("Correo electrónico");
    if (!Regex.IsMatch(correoElectronico, patronCorreo))
    {
      Console.WriteLine("Correo electrónico inválido.");
      continue;
    }
    break;
  } while (true);

  return correoElectronico;
}

decimal LeerSaldo()
{
  decimal saldo;
  do
  {
    if (!decimal.TryParse(Leer("Saldo"), out saldo))
    {
      Console.WriteLine("Saldo inválido.");
      continue;
    }
    if (!EsPositivo(saldo))
    {
      Console.WriteLine("Debe ser positivo.");
      continue;
    }
    break;
  } while (true);

  return saldo;
}

char LeerCaracter(string mensaje) {
  if (!char.TryParse(Leer(mensaje), out char valor))
    Console.WriteLine("Entrada inválida.");

  return valor;
}

char LeerTipoUsuario()
{
  char tipoUsuario;
  List<char> opcionesValidas = new() { 'c', 'e'};

  do
  {

    if (!char.TryParse(
      Leer("Escribe 'c' si el usuario es cliente y 'e' si es empleado"),
      out tipoUsuario))
    {
      Console.WriteLine("Entrada inválida.");
      continue;
    }
    if (!opcionesValidas.Contains(tipoUsuario))
    {
      Console.WriteLine("Valor inválido.");
      continue;
    }
    break;
  } while(true);

  return tipoUsuario;
}

bool EsPositivo(decimal valor)
{
    return valor > 0;
}
