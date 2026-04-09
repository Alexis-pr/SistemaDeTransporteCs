namespace sistemaDeTransporte.Services;


using  sistemaDeTransporte.Models;
using sistemaDeTransporte.Utils.validators;



public class Transportadora
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
    
    // ----------------------------------------------///
    public void asignarRecursos()
    {
        foreach (var s in servicios)
        {
            if (s.Estado == "Disponible") Console.WriteLine($"  {s.Id}");
        }
        // Parte 1
        Console.WriteLine("Ingrese el ID del servicio:");
        string idServicio = Console.ReadLine();

        Servicio servicio = null;
        foreach (var s in servicios)
        {
            if (s.Id == idServicio) { servicio = s; break; }
        }
        if (servicio == null) { Console.WriteLine("Servicio no encontrado."); return; }

        // Parte 2
        if (servicio.ConductorAsignado != null || servicio.VehiculoAsignado != null)
        {
            Console.WriteLine("Este servicio ya tiene recursos asignados.");
            return;
        }

        // Parte 3
        Console.WriteLine("Conductores disponibles:");
        foreach (var c in conductores)
        {
            if (c.Estado == "Disponible") Console.WriteLine($"  {c.Id} -- {c.FullName}");
        }
        Console.WriteLine("Ingrese el ID del conductor:");
        string idConductor = Console.ReadLine();

        Conductor conductor = null;
        foreach (var c in conductores)
        {
            if (c.Id == idConductor && c.Estado == "Disponible") { conductor = c; break; }
        }
        if (conductor == null) { Console.WriteLine("Conductor no encontrado o no disponible."); return; }

        // Parte 4
        Console.WriteLine("Vehículos disponibles:");
        foreach (var v in vehiculos)
        {
            if (v.Estado == "Disponible") Console.WriteLine($"  {v.Placa} -- {v.Tipo}");
        }
        Console.WriteLine("Ingrese la placa del vehículo:");
        string placa = Console.ReadLine();

        Vehiculo vehiculo = null;
        foreach (var v in vehiculos)
        {
            if (v.Placa == placa && v.Estado == "Disponible") { vehiculo = v; break; }
        }
        if (vehiculo == null) { Console.WriteLine("Vehículo no encontrado o no disponible."); return; }

        // Parte final
        servicio.ConductorAsignado = conductor;
        servicio.VehiculoAsignado = vehiculo;
        conductor.Estado = "En servicio";
        vehiculo.Estado = "En servicio";
        servicio.Estado = "Asignado";

        Console.WriteLine($"Asignación exitosa: Conductor {conductor.FullName} y vehículo {vehiculo.Placa} asignados al servicio {servicio.Id}");
    }

    public void iniciarServicio()
    {
        Console.WriteLine("Ingrese el id del servicio:");
        string idServicio =  Console.ReadLine();
        
        Servicio servicio = null;

        foreach (var s in servicios)
        {
            if (s.Id == idServicio)
            {
                servicio = s; 
                break;
            }
        }

        if (servicio == null)
        {
            Console.WriteLine("Servicio no encontrado."); 
            return;
        }
        
        
        if (servicio.ConductorAsignado == null || servicio.VehiculoAsignado == null)
        {
            Console.WriteLine("El servicio no tiene conductor o vehículo asignado.");
            return;
        }
        
        servicio.Estado = "En curso";
        Console.WriteLine($"El servicio {servicio.Id} ha iniciado.");
        
    }
    
    public void finalizarServicio()
    {
        // Parte 1 - Buscar el servicio
        Console.WriteLine("Ingrese el ID del servicio a finalizar:");
        string idServicio = Console.ReadLine();

        Servicio servicio = null;
        foreach (var s in servicios)
        {
            if (s.Id == idServicio)
            {
                servicio = s; 
                break; 
            }
        }

        if (servicio == null)
        {
            Console.WriteLine("Servicio no encontrado."); return;
        }
        // Parte 2 - Calcular costo base
        int costoBase = servicio.Distancia * 1500;
        
        // Parte 3 - Recargo por distancia
        double recargoPorDistancia = 0;
        
        if (servicio.Distancia <= 20)
        {
            recargoPorDistancia = 0;
        }
        else if (servicio.Distancia <= 50)
        {
            recargoPorDistancia = costoBase * 0.10;
        }
        else
        {
            recargoPorDistancia = costoBase * 0.20;
        }
        
        // Parte 4 - Recargo por tipo de vehículo
        double recargoPorVehiculo = 0;
        if (servicio.VehiculoAsignado.Tipo == "Moto")
        {
            recargoPorVehiculo = costoBase * 0.10;
        }
        else if (servicio.VehiculoAsignado.Tipo == "Carro")
        {
            recargoPorVehiculo = costoBase * 0.20;
        }
        // Parte 5 - Costo final
        servicio.Coste = (int)(costoBase + recargoPorDistancia + recargoPorVehiculo);

        // Parte 6 - Liberar conductor y vehículo
        servicio.ConductorAsignado.Estado = "Disponible";
        servicio.VehiculoAsignado.Estado = "Disponible";

        // Parte 7 - Cambiar estado del servicio
        servicio.Estado = "Finalizado";

        Console.WriteLine($@"
        Servicio {servicio.Id} finalizado.
        Distancia: {servicio.Distancia} km
        Costo base: {costoBase}
        Recargo distancia: {recargoPorDistancia}
        Recargo vehículo: {recargoPorVehiculo}
        Costo total: {servicio.Coste}
    ");
    }
    
    public void consultarServicios()
    {
        if (servicios.Count == 0)
        {
            Console.WriteLine("No hay servicios registrados.");
            return;
        }

        foreach (var s in servicios)
        {
            Console.WriteLine($@"
        -------------------------
        ID: {s.Id}
        Origen: {s.Origen}
        Destino: {s.Destino}
        Distancia: {s.Distancia} km
        Estado: {s.Estado}
        Costo: {s.Coste}
        Conductor: {(s.ConductorAsignado != null ? s.ConductorAsignado.FullName : "Sin asignar")}
        Vehículo: {(s.VehiculoAsignado != null ? s.VehiculoAsignado.Placa : "Sin asignar")}
        -------------------------
        ");
        }
    }
    
    public void consultarRecursos()
    {
        // Conductores
        Console.WriteLine("===== CONDUCTORES =====");
        if (conductores.Count == 0)
        {
            Console.WriteLine("No hay conductores registrados.");
        }
        else
        {
            foreach (var c in conductores)
            {
                Console.WriteLine($@"
        -------------------------
        ID: {c.Id}
        Nombre: {c.FullName}
        Licencia: {c.Licencia}
        Estado: {c.Estado}
        -------------------------
            ");
            }
        }

        // Vehículos
        Console.WriteLine("===== VEHÍCULOS =====");
        if (vehiculos.Count == 0)
        {
            Console.WriteLine("No hay vehículos registrados.");
        }
        else
        {
            foreach (var v in vehiculos)
            {
                Console.WriteLine($@"
        -------------------------
        Placa: {v.Placa}
        Tipo: {v.Tipo}
        Capacidad: {v.Capacidad}
        Estado: {v.Estado}
        -------------------------
            ");
            }
        }
    }
    
    public void reportesOperativos()
    {
        // Parte 1 - Total de servicios realizados
        int serviciosFinalizados = 0;
        foreach (var s in servicios)
        {
            if (s.Estado == "Finalizado")
                serviciosFinalizados++;
        }

        // Parte 2 - Total de ingresos generados
        int totalIngresos = 0;
        foreach (var s in servicios)
        {
            if (s.Estado == "Finalizado")
                totalIngresos += s.Coste;
        }

        // Parte 3 - Servicios en curso
        int serviciosEnCurso = 0;
        foreach (var s in servicios)
        {
            if (s.Estado == "En curso")
                serviciosEnCurso++;
        }

        // Parte 4 - Conductores ocupados vs disponibles
        int conductoresOcupados = 0;
        int conductoresDisponibles = 0;
        foreach (var c in conductores)
        {
            if (c.Estado == "Disponible")
                conductoresDisponibles++;
            else
                conductoresOcupados++;
        }

        // Parte 5 - Vehículos ocupados vs disponibles
        int vehiculosOcupados = 0;
        int vehiculosDisponibles = 0;
        foreach (var v in vehiculos)
        {
            if (v.Estado == "Disponible")
                vehiculosDisponibles++;
            else
                vehiculosOcupados++;
        }

        // Mostrar reporte
        Console.WriteLine($@"
            ========= REPORTE OPERATIVO =========
            Servicios finalizados:     {serviciosFinalizados}
            Servicios en curso:        {serviciosEnCurso}
            Total ingresos generados:  {totalIngresos}

            Conductores disponibles:   {conductoresDisponibles}
            Conductores ocupados:      {conductoresOcupados}

            Vehículos disponibles:     {vehiculosDisponibles}
            Vehículos ocupados:        {vehiculosOcupados}
            =====================================
        ");
    }
    
    


}