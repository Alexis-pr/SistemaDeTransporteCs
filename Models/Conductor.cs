namespace sistemaDeTransporte.Models;

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