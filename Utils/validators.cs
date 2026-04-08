namespace sistemaDeTransporte.Utils.validators;

public class Validators
{
    public int validarNum(int a)
    {
        while (a < 0)
        {
            Console.WriteLine("El valor debe ser mayor a cero");
            a = int.Parse(Console.ReadLine());
        }
        return a;
    }

    public string validarString(string a)
    {
        string valor;
        do
        {
            Console.WriteLine(a);
            valor = Console.ReadLine();
            if (string.IsNullOrEmpty(valor))
                Console.WriteLine("El valor es obligatorio");
        } while (string.IsNullOrWhiteSpace(valor));
        return valor;
    }

}