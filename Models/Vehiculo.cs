namespace sistemaDeTransporte.Models;

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