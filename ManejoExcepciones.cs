using System;
using System.Collections.Generic;

class ManejoExcepciones
{
    static void Main()
    {
        Console.WriteLine("=== Ejercicio 1: División segura ===");
        Dividir();

        Console.WriteLine("\n=== Ejercicio 2: Índice de lista seguro ===");
        AccederLista();

        Console.WriteLine("\n=== Ejercicio 3: Lectura segura de número ===");
        double numero = LeerNumero();
        Console.WriteLine($"Número ingresado: {numero}");
    }

    // 1. División segura
    static void Dividir()
    {
        try
        {
            Console.Write("Ingresa el dividendo: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingresa el divisor: ");
            int b = Convert.ToInt32(Console.ReadLine());

            if (b == 0)
            {
                Console.WriteLine("❌ Error: No se puede dividir por cero.");
                return;
            }

            Console.WriteLine($"Resultado: {a} / {b} = {(double)a / b:F2}");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Error: Debes ingresar números enteros.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error inesperado: {ex.Message}");
        }
    }

    // 2. Índice de lista seguro
    static void AccederLista()
    {
        var nombres = new List<string> { "Ana", "Pedro", "Lucía" };

        try
        {
            Console.Write("Ingresa un índice (0-2): ");
            int indice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Nombre: {nombres[indice]}");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("❌ Error: El índice está fuera de rango.");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Error: Debes ingresar un número entero.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error inesperado: {ex.Message}");
        }
    }

    // 3. Lectura segura de número
    static double LeerNumero()
    {
        while (true)
        {
            try
            {
                Console.Write("Ingresa un número: ");
                return double.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("❌ Error: Eso no es un número. Inténtalo de nuevo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}. Inténtalo de nuevo.");
            }
        }
    }
}
