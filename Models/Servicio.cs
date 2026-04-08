namespace sistemaDeTransporte.Models;

public class Servicio
{
    public  string Id { get; set; }
    public  string Origen { get; set; }
    public string  Destino { get; set; }
    public  int Distancia { get; set; }
    public  string Estado { get; set; }
    public int Coste { get; set; }
    
    public Conductor ConductorAsignado { get; set; } = null;
    public Vehiculo VehiculoAsignado { get; set; } = null;
    
    
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
