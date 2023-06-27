using ConsoleApp1.Models;
using ConsoleTables;

class Program
{
    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("----- Menú Principal -----");
            Console.WriteLine("1. Agregar cliente");
            Console.WriteLine("2. Consultar clientes");
            Console.WriteLine("3. Consultar cliente por Id");
            Console.WriteLine("4. Modificar cliente");
            Console.WriteLine("5. Eliminar cliente");
            Console.WriteLine("0. Salir");
            Console.Write("Ingrese una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarCliente();
                    break;
                case "2":
                    ConsultarClientes();
                    break;
                case "3":
                    ConsultarCliente();
                    break;
                case "4":
                    ModificarCliente();
                    break;
                case "5":
                    EliminarCliente();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    // Agregar cliente
    public static void AgregarCliente()
    {
        Console.WriteLine("----- Agregar Cliente -----");

        Cliente cliente = new Cliente();

        Console.Write("Nombre: ");
        cliente.Nombre = Console.ReadLine();

        Console.Write("Apellido: ");
        cliente.Apellido = Console.ReadLine();

        Console.Write("Dirección: ");
        cliente.Direccion = Console.ReadLine();

        Console.Write("Teléfono: ");
        cliente.Telefono = Console.ReadLine();

        Console.Write("Fecha de Nacimiento (yyyy-mm-dd): ");
        cliente.FechaNacimiento = DateTime.Parse(Console.ReadLine());

        Console.Write("Estado: ");
        cliente.Estado = Console.ReadLine()[0];

        using (var context = new AppDbContext())
        {
            context.Clientes.Add(cliente);
            context.SaveChanges();

            Console.WriteLine("Cliente agregado correctamente.");
        }
    }

    // Consultar clientes
    public static void ConsultarClientes()
    {
        Console.WriteLine("----- Consultar Clientes -----");
        using (var context = new AppDbContext())
        {
            List<Cliente> clientes = context.Clientes.ToList();
            var table = new ConsoleTable("Id", "Nombre", "Apellido", "Dirección", "Teléfono", "Fecha de Nacimiento", "Estado");

            foreach (var cliente in clientes)
            {
                table.AddRow(cliente.Id, cliente.Nombre, cliente.Apellido, cliente.Direccion, cliente.Telefono, cliente.FechaNacimiento.ToShortDateString(), cliente.Estado);
                
            }
            Console.WriteLine();
            table.Write(Format.Alternative);
        }
    }

    // Consultar cliente por Id
    public static void ConsultarCliente()
    {
        Console.WriteLine("----- Consultar Cliente por Id -----");
        Console.Write("Ingrese el Id del cliente: ");
        int id = int.Parse(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Cliente cliente = context.Clientes.Find(id);

            if (cliente != null)
            {
                var table = new ConsoleTable("Id", "Nombre", "Apellido", "Dirección", "Teléfono", "Fecha de Nacimiento", "Estado");
                table.AddRow(cliente.Id, cliente.Nombre, cliente.Apellido, cliente.Direccion, cliente.Telefono, cliente.FechaNacimiento.ToShortDateString(), cliente.Estado);
                Console.WriteLine();
                table.Write(Format.Alternative);
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
    }

    // Modificar cliente
    public static void ModificarCliente()
    {
        Console.WriteLine("----- Modificar Cliente -----");
        Console.Write("Ingrese el Id del cliente a modificar: ");
        int id = int.Parse(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Cliente cliente = context.Clientes.Find(id);

            if (cliente != null)
            {
                var table = new ConsoleTable("Id", "Nombre", "Apellido", "Dirección", "Teléfono", "Fecha de Nacimiento", "Estado");
                table.AddRow(cliente.Id, cliente.Nombre, cliente.Apellido, cliente.Direccion, cliente.Telefono, cliente.FechaNacimiento.ToShortDateString(), cliente.Estado);
                Console.WriteLine();
                table.Write(Format.Alternative);


                Console.Write("Nuevo nombre: ");
                string nuevoNombre = Console.ReadLine();
                cliente.Nombre = string.IsNullOrEmpty(nuevoNombre) ? cliente.Nombre : nuevoNombre;

                Console.Write("Nuevo apellido: ");
                string nuevoApellido = Console.ReadLine();
                cliente.Apellido = string.IsNullOrEmpty(nuevoApellido) ? cliente.Apellido : nuevoApellido;

                Console.Write("Nueva dirección: ");
                string nuevaDireccion = Console.ReadLine();
                cliente.Direccion = string.IsNullOrEmpty(nuevaDireccion) ? cliente.Direccion : nuevaDireccion;

                Console.Write("Nuevo teléfono: ");
                string nuevoTelefono = Console.ReadLine();
                cliente.Telefono = string.IsNullOrEmpty(nuevoTelefono) ? cliente.Telefono : nuevoTelefono;

                Console.Write("Nueva fecha de nacimiento (yyyy-mm-dd): ");
                string nuevaFechaNacimientoStr = Console.ReadLine();
                DateTime nuevaFechaNacimiento;
                if (DateTime.TryParse(nuevaFechaNacimientoStr, out nuevaFechaNacimiento))
                {
                    cliente.FechaNacimiento = nuevaFechaNacimiento;
                }

                Console.Write("Nuevo estado: ");
                string nuevoEstado = Console.ReadLine();
                cliente.Estado = string.IsNullOrEmpty(nuevoEstado) ? cliente.Estado : nuevoEstado[0];

                context.SaveChanges();

                Console.WriteLine("Cliente modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
    }


    // Eliminar cliente
    public static void EliminarCliente()
    {
        Console.WriteLine("----- Eliminar Cliente -----");
        Console.Write("Ingrese el Id del cliente a eliminar: ");
        int id = int.Parse(Console.ReadLine());

        using (var context = new AppDbContext())
        {
            Cliente cliente = context.Clientes.Find(id);

            if (cliente != null)
            {
                context.Clientes.Remove(cliente);
                context.SaveChanges();

                Console.WriteLine("Cliente eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
    }
}
