using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Producto
{
    public string Nombre { get; set; }
    public double Precio { get; set; }
    public int Stock { get; set; }

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Precio: ${Precio:F2}, Stock: {Stock}";
    }
}

class InventarioConJson
{
    static List<Producto> inventario = new List<Producto>();
    static readonly string archivoJson = "inventario.json";

    static void Main()
    {
        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n=== MENÚ INVENTARIO ===");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Buscar por nombre");
            Console.WriteLine("4. Valor total del inventario");
            Console.WriteLine("5. Exportar a JSON");
            Console.WriteLine("6. Importar desde JSON");
            Console.WriteLine("7. Salir");
            Console.Write("Elige una opción: ");

            switch (Console.ReadLine())
            {
                case "1": AgregarProducto(); break;
                case "2": ListarProductos(); break;
                case "3": BuscarPorNombre(); break;
                case "4": CalcularValorTotal(); break;
                case "5": ExportarJson(); break;
                case "6": ImportarJson(); break;
                case "7": salir = true; break;
                default: Console.WriteLine("Opción no válida."); break;
            }
        }
    }

    static void AgregarProducto()
    {
        try
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Precio: ");
            double precio = double.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            if (precio < 0 || stock < 0)
            {
                Console.WriteLine("❌ Precio y stock deben ser ≥ 0.");
                return;
            }

            inventario.Add(new Producto { Nombre = nombre, Precio = precio, Stock = stock });
            Console.WriteLine("✅ Producto agregado.");
        }
        catch (FormatException)
        {
            Console.WriteLine("❌ Error: Precio o stock deben ser números válidos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }

    static void ListarProductos()
    {
        if (inventario.Count == 0)
        {
            Console.WriteLine("📦 El inventario está vacío.");
            return;
        }

        Console.WriteLine("\n--- Productos ---");
        foreach (var p in inventario)
            Console.WriteLine(p);
    }

    static void BuscarPorNombre()
    {
        Console.Write("Nombre a buscar: ");
        string nombre = Console.ReadLine();
        var encontrado = inventario.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (encontrado != null)
            Console.WriteLine($"🔍 Encontrado: {encontrado}");
        else
            Console.WriteLine("❌ Producto no encontrado.");
    }

    static void CalcularValorTotal()
    {
        double total = inventario.Sum(p => p.Precio * p.Stock);
        Console.WriteLine($"💰 Valor total del inventario: ${total:F2}");
    }

    static void ExportarJson()
    {
        try
        {
            string json = JsonSerializer.Serialize(inventario, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivoJson, json);
            Console.WriteLine($"✅ Inventario exportado a '{archivoJson}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al exportar: {ex.Message}");
        }
    }

    static void ImportarJson()
    {
        try
        {
            if (!File.Exists(archivoJson))
            {
                Console.WriteLine($"❌ El archivo '{archivoJson}' no existe.");
                return;
            }

            string json = File.ReadAllText(archivoJson);
            var nuevoInventario = JsonSerializer.Deserialize<List<Producto>>(json);

            if (nuevoInventario != null)
            {
                inventario = nuevoInventario;
                Console.WriteLine("✅ Inventario importado correctamente.");
            }
            else
            {
                Console.WriteLine("❌ El archivo JSON está vacío o no es válido.");
            }
        }
        catch (JsonException jex)
        {
            Console.WriteLine($"❌ Error de formato JSON: {jex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al importar: {ex.Message}");
        }
    }
}
