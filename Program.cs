namespace Pokemon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pokedex.CargarDesdeArchivo();
            while (true)
            {
                Pokedex pokedex = new Pokedex();

                Console.WriteLine("--MENU PRINCIPAL---");
                Console.WriteLine("1. Agregar Pokemon");
                Console.WriteLine("2. Mostrar Pokedex");
                Console.WriteLine("3. Buscar Pokemon");
                Console.WriteLine("4. Salir");

                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Pokedex.AgregarPokemon();
                        break;
                    case "2":
                        Pokedex.MostrarDex();
                        break;
                    case "3":
                        Pokedex.BuscarPokemon();
                        break;
                    case "4":
                        Console.WriteLine("¡Hasta luego!");
                        return;
                }
            }
        }
    }
}