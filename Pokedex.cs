using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Pokemon
{
    internal class Pokedex
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public static List<Pokedex> pokedexList = new List<Pokedex>();

        public static void AgregarPokemon()
        {
            while (true)
            {
                Pokedex pokemon = new Pokedex();

                Console.WriteLine("Ingrese el ID del Pokémon:");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int idValidado) || idValidado <=0 )
                {
                    Console.WriteLine("El ID debe ser un número positivo. Por favor, intente nuevamente.");
                    continue;
                }

                pokemon.Id = idValidado;

                var existe = pokedexList.Any(e => e.Id == pokemon.Id);

                if (existe)
                {
                    Console.WriteLine("El ID ya existe. Por favor, ingrese un ID único.");
                    continue;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Ingrese el nombre del Pokémon:");
                        string nombreP = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nombreP) || nombreP.Any(char.IsDigit) )
                        {
                            Console.WriteLine("El nombre no puede estar vacío. Por favor, intente nuevamente.");
                            continue;
                        }
                        else
                        {
                            pokemon.Nombre = nombreP;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Ingrese el tipo del Pokémon:");
                        string tipoP = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(tipoP) || tipoP.Any(char.IsDigit))
                        {
                            Console.WriteLine("El tipo no puede estar vacío. Por favor, intente nuevamente.");
                            continue;
                        }
                        else
                        {
                            pokemon.Tipo = tipoP;
                            break;
                        }
                    }
                    pokedexList.Add(pokemon);
                    GuardarEnArchivo();
                    break;
                }
            }
            Console.WriteLine("Pokémon agregado exitosamente.");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public static void MostrarDex()
        {
            foreach(var pokemon in pokedexList)
            {
                Console.WriteLine($"ID: {pokemon.Id}, Nombre: {pokemon.Nombre}, Tipo: {pokemon.Tipo}");
            }
        }
        public static void ModificarPokemon()
        {
            Console.WriteLine("Ingrese el ID del Pokémon que desea modificar:");
            string inputId = Console.ReadLine();

            // Validamos el ID igual que en Agregar
            if (!int.TryParse(inputId, out int idABuscar))
            {
                Console.WriteLine("ID inválido. Debe ser un número.");
                return;
            }

            // Buscamos si existe en la lista
            var pokemon = pokedexList.FirstOrDefault(p => p.Id == idABuscar);

            if (pokemon == null)
            {
                Console.WriteLine("No se encontró ningún Pokémon con ese ID.");
                return;
            }

            Console.WriteLine($"Pokémon encontrado: {pokemon.Nombre} (Tipo: {pokemon.Tipo})");

            // Bucle para el NUEVO NOMBRE
            while (true)
            {
                Console.WriteLine("Ingrese el nuevo nombre (o presione Enter para mantener el actual):");
                string nuevoNombre = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nuevoNombre)) break; // Si no escribe nada, no cambia

                if (nuevoNombre.Any(char.IsDigit))
                {
                    Console.WriteLine("El nombre no puede tener números.");
                    continue;
                }

                pokemon.Nombre = nuevoNombre;
                break;
            }

            // Bucle para el NUEVO TIPO
            while (true)
            {
                Console.WriteLine("Ingrese el nuevo tipo (o presione Enter para mantener el actual):");
                string nuevoTipo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nuevoTipo)) break;

                if (nuevoTipo.Any(char.IsDigit))
                {
                    Console.WriteLine("El tipo no puede tener números.");
                    continue;
                }

                pokemon.Tipo = nuevoTipo;
                break;
            }

            // PASO CLAVE: Guardamos los cambios en el archivo
            GuardarEnArchivo();
            Console.WriteLine("¡Pokémon actualizado correctamente!");
        }

        static string rutaArchivo = "pokedex.json";

        public static void GuardarEnArchivo()
        {
            // Convertimos la lista completa a una cadena de texto JSON
            // El "WriteIndented" es para que el archivo se vea bonito (con espacios y saltos)
            string jsonString = JsonSerializer.Serialize(pokedexList, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(rutaArchivo, jsonString);
        }

        public static void CargarDesdeArchivo()
        {
            if (!File.Exists(rutaArchivo)) return;

            string jsonString = File.ReadAllText(rutaArchivo);

            // Convertimos el texto de vuelta a nuestra lista
            pokedexList = JsonSerializer.Deserialize<List<Pokedex>>(jsonString);
        }

        public static void BuscarPokemon()
        {
            Console.WriteLine("Ingrese el nombre del Pokémon a buscar:");
            string busqueda = Console.ReadLine().ToLower();

            // Buscamos coincidencias (usamos LINQ que ya sabes usar)
            var resultado = pokedexList.Where(p => p.Nombre.ToLower().Contains(busqueda)).ToList();

            if (resultado.Count > 0)
            {
                foreach (var p in resultado)
                {
                    Console.WriteLine($"Encontrado -> ID: {p.Id}, Nombre: {p.Nombre}, Tipo: {p.Tipo}");
                }
            }
            else
            {
                Console.WriteLine("No se encontró ningún Pokémon con ese nombre.");
            }
            Console.ReadKey(); // Pausa para ver el resultado
        }
    }
}
