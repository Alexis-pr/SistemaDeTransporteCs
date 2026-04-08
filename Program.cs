// See https://aka.ms/new-console-template for more information


using System.Runtime.InteropServices.JavaScript;
using sistemaDeTransporte.Utils.validators;
using sistemaDeTransporte.Models;



bool active = true;
Transportadora app = new Transportadora();

while (active)
{
    
    Console.WriteLine(@$"Seleccione la opción deseada:
       1. Registrar conductor
       2. Registrar vehículo
       3. Registrar servicio de transporte
       4. Asignar conductor y vehículo a servicio
       5. Iniciar servicio
       6. Finalizar servicio
       7. Consultar servicios
       8. Consultar conductores y vehículos
       9. Reportes operativos
      10. Salir
     ");
    int option = int.Parse(Console.ReadLine());
    
    switch (option)
    {
        case 1:
            app.registrarConductor();
            break;
        case 2:
            app.registrarVehiculo();
            break;
        case 3:
            app.registrarServicio();
            break;
        case 4:
            app.asignarRecursos();
            break;
        case 5:
            
            break;
        case 6:
            
            break;
        case 7:
            break;
        case 8:
            break;
        case 9:
            break;
        
        case 10:
            active = false;
            break;
        default:
            Console.WriteLine("Invalid option");
          break;  
            
    }
}





class Transportadora
{
    List<Conductor> conductores = new List<Conductor>();
    List<Vehiculo> vehiculos = new List<Vehiculo>();
    List<Servicio> servicios = new List<Servicio>();
    
    Validators validator = new Validators();
    
    public void registrarConductor()
    {
        /*Console.WriteLine("Ingrese la identificación dle transportista: ");*/
        string id = validator.validarString("ngrese la identificación dle transportista: ");
        
       /*Console.WriteLine("Ingrese el nombre de transporte");*/
       string nombre = validator.validarString("Ingresar Nombre: ");
       
       /*Console.WriteLine("Registra la licencia: ");*/
       string licencia = validator.validarString("Registrar la licencia: ");
       
       string estado = "Disponible";
      
       var conductor = new Conductor(id, nombre, licencia,estado);
       conductores.Add(conductor);
       
       Console.WriteLine($@"  
            Se ha registrado correctamente: {conductor.Id} -- {conductor.FullName} -- {conductor.Licencia} -- {conductor.Estado}
       ");
       
    }

    public void registrarVehiculo()
    {
        Console.WriteLine("Ingresa la placa:");
        string placa = Console.ReadLine();
        
        Console.WriteLine("Ingrese el tipo:");
        string tipo = Console.ReadLine();
        
        Console.WriteLine("Ingrese el capacidad:");
        int capacidad = int.Parse(Console.ReadLine());
        
        validator.validarNum(capacidad);
        
        Console.WriteLine("Ingrese el estado:  (disponible / en servicio / fuera de operación) ");
        string estado = Console.ReadLine();
        
        var carros = new Vehiculo(placa, tipo, capacidad, estado);
        vehiculos.Add(carros);
        Console.WriteLine($"Se ha registrado correctamente: {carros.Placa} -- {carros.Tipo} -- {carros.Capacidad} --{carros.Estado}  ");
        }
    
    
    public void registrarServicio()
    {
        Console.WriteLine("Ingrese el ID del servicio:");  
        string id = Console.ReadLine();
        
        Console.WriteLine("Ingrese el origen del viaje:");
        string origen = Console.ReadLine();
        
        Console.WriteLine("Ingrese el destino del viaje:");
        string destino = Console.ReadLine();
        
        Console.WriteLine("Ingrese la Distancia del viaje:");
        int distancia = int.Parse(Console.ReadLine());
        validator.validarNum(distancia);
        
        Console.WriteLine("Ingrese el estado:");
        string estado = Console.ReadLine();
        
        Console.WriteLine("Ingrese el coste del servicio:");
        int coste = 0;
        
        var services = new Servicio(id, origen, destino, distancia, estado,coste);
        
        servicios.Add(services);
        Console.WriteLine($"se ha registrado el valor de forma correcta {services.Id} -- {services.Origen} -- {services.Destino} -- {services.Distancia} -- {services.Estado} -- {services.Coste} ");
        
    }
    
        public void asignarRecursos()
    {
        // 1. Pedir el ID del servicio
        Console.WriteLine("Ingrese el ID del servicio:");
        string idServicio = Console.ReadLine();

        // 2. Buscar el servicio en la lista
        Servicio servicio = null;
        foreach (var s in servicios)
        {
            if (s.Id == idServicio)
            {
                servicio = s;
                break;
            }
        }

        // 3. ¿Existe?
        if (servicio == null)
        {
            Console.WriteLine("Servicio no encontrado.");
            return; // sale del método
        }

        // 4. ¿Ya tiene asignación? (restricción de duplicados)
        if (servicio.ConductorAsignado != null || servicio.VehiculoAsignado != null)
        {
            Console.WriteLine("Este servicio ya tiene recursos asignados.");
            return;
        }

        // 5. Mostrar solo conductores disponibles
        Console.WriteLine("Conductores disponibles:");
        foreach (var c in conductores)
        {
            if (c.Estado == "Disponible")
            {
                Console.WriteLine($"  {c.Id} -- {c.FullName}");
            }
        }

        // 6. El usuario elige un conductor por ID
        Console.WriteLine("Ingrese el ID del conductor:");
        string idConductor = Console.ReadLine();

        Conductor conductor = null;
        foreach (var c in conductores)
        {
            if (c.Id == idConductor && c.Estado == "Disponible")
            {
                conductor = c;
                break;
            }
        }

        if (conductor == null)
        {
            Console.WriteLine("Conductor no encontrado o no disponible.");
            return;
        }

        // 7. Mostrar solo vehículos disponibles
        Console.WriteLine("Vehículos disponibles:");
        foreach (var v in vehiculos)
        {
            if (v.Estado == "Disponible")
            {
                Console.WriteLine($"  {v.Placa} -- {v.Tipo}");
            }
        }

        // 8. El usuario elige un vehículo por placa
        
        Console.WriteLine("Ingrese la placa del vehículo:");
        string placa = Console.ReadLine();

        Vehiculo vehiculo = null;
        foreach (var v in vehiculos)
        {
            if (v.Placa == placa && v.Estado == "Disponible")
            {
                vehiculo = v;
                break;
            }
        }

        if (vehiculo == null)
        {
            Console.WriteLine("Vehículo no encontrado o no disponible.");
            return;
        }

        // 9. Hacer la asignación
        servicio.ConductorAsignado = conductor;
        servicio.VehiculoAsignado = vehiculo;

        // 10. Cambiar los estados
        conductor.Estado = "En servicio";
       
        servicio.Estado = "Asignado";

        Console.WriteLine($"Asignación exitosa: Conductor {conductor.FullName} y vehículo {vehiculo.Placa} asignados al servicio {servicio.Id}");
    }


    public void iniciarServicio()
    {
        
    }

   
  
 
    
}

    


