using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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

        static string rutaArchivo = "pokedex.txt";

        public static void GuardarEnArchivo()
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (var p in pokedexList)
                {
                    // Escribimos una línea por cada Pokémon: "1,Pikachu,Eléctrico"
                    sw.WriteLine($"{p.Id},{p.Nombre},{p.Tipo}");
                }
            }
        }

        public static void CargarDesdeArchivo()
        {
            if (!File.Exists(rutaArchivo)) return; // Si no hay archivo, no hacemos nada

            pokedexList.Clear(); // Limpiamos la lista actual
            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(','); // Separamos por la coma
                if (datos.Length == 3)
                {
                    pokedexList.Add(new Pokedex
                    {
                        Id = int.Parse(datos[0]),
                        Nombre = datos[1],
                        Tipo = datos[2]
                    });
                }
            }
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
