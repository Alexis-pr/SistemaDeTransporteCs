// See https://aka.ms/new-console-template for more information


using System.Runtime.InteropServices.JavaScript;

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
    List<dynamic> conductores = new List<dynamic>();
    List<dynamic> vehiculos = new List<dynamic>();
    List<dynamic> servicios = new List<dynamic>();
    
    public void registrarConductor()
    {
        Console.WriteLine("Ingrese la identificación dle transportista: ");
        string id = Console.ReadLine();
        
       Console.WriteLine("Ingrese el nombre de transporte");
       string nombre = Console.ReadLine();
       
       Console.WriteLine("Registra la licencia: ");
       string licencia = Console.ReadLine();
       
       string estado = "Disponible";
      
       
       var conductor = new Conductor(id, nombre, licencia,estado);
       
       
       
       conductores.Add(conductor);
       Console.WriteLine($"Se ha registrado correctamente: {conductor.Id} -- {conductor.FullName} -- {conductor.Licencia} -- {conductor.Estado}  ");
       
    }

    public void registrarVehiculo()
    {
        Console.WriteLine("Ingresa la placa:");
        string placa = Console.ReadLine();
        
        Console.WriteLine("Ingrese el tipo:");
        string tipo = Console.ReadLine();
        
        Console.WriteLine("Ingrese el capacidad:");
        int capacidad = int.Parse(Console.ReadLine());
        
        validarNum(capacidad);
        
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
        validarNum(distancia);
        
        Console.WriteLine("Ingrese el estado:");
        string estado = Console.ReadLine();
        
        Console.WriteLine("Ingrese el coste del servicio:");
        int coste = 0;
        
        var services = new Servicio(id, origen, destino, distancia, estado,coste);
        
        servicios.Add(services);
        Console.WriteLine($"se ha registrado el valor de forma correcta {services.Id} -- {services.Origen} -- {services.Destino} -- {services.Distancia} -- {services.Estado} -- {services.Coste} ");
        
    }

    public int validarNum(int a)
    {
        while (a < 0)
        {
            Console.WriteLine("El valor debe ser mayor a cero");
            a = int.Parse(Console.ReadLine());
        }
        return a;
    }

    private string validarString(string a)
    {
        string valor;
        do
        {
            Console.WriteLine("Ingrese el valor:");
            valor = Console.ReadLine();
            if (string.IsNullOrEmpty(valor))
                Console.WriteLine("El valor es obligatorio");
        } while (string.IsNullOrWhiteSpace(valor));
        return valor;
    }

    private void valirExistente()
    {
        foreach (var VARIABLE in COLLECTION)
        {
            
        }
    }
    
    
    
}

    


public class Conductor
{
    public   string Id { get; set; }
    public  string FullName { get; set; }
    public  string Licencia { get; set; }
    public  string Estado { get; set; }
    
    public Conductor(string id, string fullName, string licencia, string estado)
    {
        Id = id;
        FullName = fullName;
        Licencia = licencia;
        Estado = estado;
    }
    
    
}

public class Vehiculo
{
    
    public  string Placa { get; set; }
    public  string Tipo { get; set; } 
    public  int Capacidad { get; set; }
    public string Estado { get; set; }

    public Vehiculo(string placa, string tipo, int capacidad, string estado)
    {
        Placa = placa;
        Tipo = tipo;
        Capacidad = capacidad;
        Estado = estado;
    }
    
}

public class Servicio
{
    public  string Id { get; set; }
    public  string Origen { get; set; }
    public string  Destino { get; set; }
    public  int Distancia { get; set; }
    public  string Estado { get; set; }
    public int Coste { get; set; }

    public Servicio(string id, string origen, string destino, int distancia, string estado, int coste)
    {
        Id = id;
        Origen = origen;
        Destino = destino;
        Distancia = distancia;
        Estado = estado;
        Coste = coste;
    }
}

