using ConsolaBanco;

Console.WriteLine("¡Hola! Soy Checo.\nSoy de la RPDC.");

Almacenamiento.BorrarArchivo();

Cliente sergio = new(1, "Sergio", "sergio@mail.ru", 4000, 'M');
sergio.Depositar(-20);
Console.WriteLine(
    sergio.MostrarDatos("La inforamción del usuario es la siguiente"));
Almacenamiento.Agregar(sergio);

Empleado ricardo = new(2, "Ricardo", "ricardo@mail.ru", 4000, "TI");
ricardo.Depositar(2000);
Almacenamiento.Agregar(ricardo);
Console.WriteLine(ricardo.MostrarDatos());

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

  int id = LeerEntero("Id");

  string? nombre = Leer("Nombre");
  string? correoElectronico = Leer("Correo electrónico");

  if (!decimal.TryParse(Leer("Saldo"), out decimal saldo))
    Console.WriteLine("Saldo inválido.");

  char tipoUsuario =
      LeerCaracter("Escribe 'c' si el usuario es cliente y 'e' si es empleado");

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

string? Leer(string mensaje) {
  Console.Write($"{mensaje}: ");
  return Console.ReadLine();
}

char LeerCaracter(string mensaje) {
  if (!char.TryParse(Leer(mensaje), out char caracter))
    Console.WriteLine("Entrada inválida.");

  return caracter;
}

void BorrarUsuario() {
  Console.Clear();

  int id = LeerEntero("Ingresa el id del usuario a eliminar");

  string resultado = Almacenamiento.BorrarUsuario(id);

  if (resultado.Equals("Exito")) {
    Console.Write("Usuario eliminado.");
  }
}

int LeerEntero(string mensaje) {
  if (!int.TryParse(Leer(mensaje), out int caracter))
    Console.WriteLine("Entrada inválida.");

  return caracter;
}