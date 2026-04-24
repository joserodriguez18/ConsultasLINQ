using HistoriaUsuario2.Models;
using System.Linq;

// --- 1. NUESTRA BASE DE DATOS EN MEMORIA ---
// Usamos llaves {} para llenar la lista de una sin tanto código repetitivo.
// Como no hay constructor, asignamos cada propiedad a mano para que sea súper claro qué es cada cosa.
List<Paciente> ListaPacientes = new List<Paciente>
{
    new Paciente
    {
        Id = 1, Nombre = "Ana García", Edad = 30,
        Mascota = new Mascota { Id = 101, Nombre = "Luna", Especie = "Perro", Raza = "Labrador", Edad = 5, Sintoma = "Dolor de pata" }
    },
    new Paciente
    {
        Id = 2, Nombre = "Luis Pérez", Edad = 45,
        Mascota = new Mascota { Id = 102, Nombre = "Michi", Especie = "Gato", Raza = "Persa", Edad = 2, Sintoma = "No quiere comer" }
    },
    new Paciente
    {
        Id = 3, Nombre = "Carla Ruiz", Edad = 25,
        Mascota = new Mascota { Id = 103, Nombre = "Rex", Especie = "Perro", Raza = "Pastor Alemán", Edad = 8, Sintoma = "Vacunación" }
    },
    new Paciente
    {
        Id = 4, Nombre = "Juan Mora", Edad = 50,
        Mascota = new Mascota { Id = 104, Nombre = "Pipo", Especie = "Loro", Raza = "Cabeza Azul", Edad = 1, Sintoma = "Plumas caídas" }
    },
    new Paciente
    {
        Id = 5, Nombre = "Marta Soto", Edad = 28,
        Mascota = new Mascota { Id = 105, Nombre = "Bruno", Especie = "Perro", Raza = "Golden", Edad = 3, Sintoma = "Tos" }
    }
};

// Pasamos la lista a un diccionario para que buscar por ID sea "volando"
Dictionary<int, Paciente> diccionarioPacientes = ListaPacientes.ToDictionary(p => p.Id);


// --- 2. NUESTRAS FUNCIONES AUXILIARES ---

// Esta función nos ayuda a imprimir a todos los pacientes de forma bonita
void VerInfoPacientes()
{
    Console.WriteLine("=== LISTA GENERAL DE PACIENTES ===");
    foreach (var paciente in diccionarioPacientes.Values)
    {
        Console.WriteLine(@$"
Paciente: {paciente.Nombre} | Edad: {paciente.Edad} 
Mascota: {paciente.Mascota.Nombre} | Especie: {paciente.Mascota.Especie} | Raza: {paciente.Mascota.Raza} | Edad: {paciente.Mascota.Edad} | Sintoma: {paciente.Mascota.Sintoma}
");
    }
}

// Aquí filtramos perros, los ordenamos por edad y armamos un objeto nuevo con solo lo necesario
void UsarWhere()
{
    var consultaPerros = ListaPacientes
        .Where(p => p.Mascota.Especie == "Perro") 
        .OrderBy(p => p.Mascota.Edad) 
        .Select(p => new
        {
            Dueno = p.Nombre,
            NombreMascota = p.Mascota.Nombre,
            EdadMascota = p.Mascota.Edad
        });

    Console.WriteLine("--- Perros ordenados por edad ---");
    foreach (var item in consultaPerros)
    {
        Console.WriteLine($"Dueño: {item.Dueno} | Perro: {item.NombreMascota} | Edad: {item.EdadMascota}");
    }
}


// --- 3. EJECUCIÓN DE LAS CONSULTAS Y REPORTES ---

// Reporte de cuántos animalitos hay de cada tipo
var gruposPorEspecie = ListaPacientes.GroupBy(p => p.Mascota.Especie);
Console.WriteLine("--- Conteo por Especies ---");
foreach (var grupo in gruposPorEspecie)
{
    Console.WriteLine($"Especie: {grupo.Key} | Cantidad: {grupo.Count()}");
}

// Chequeo rápido de si atendemos loros
bool hayLoros = ListaPacientes.Any(p => p.Mascota.Especie == "Loro");
Console.WriteLine($"\n¿Tenemos loros en la clínica?: {(hayLoros ? "Sí" : "No")}");

// Buscamos al primer michi que aparezca
var primerGato = ListaPacientes.FirstOrDefault(p => p.Mascota.Especie == "Gato");
if (primerGato != null)
{
    Console.WriteLine($"El primer gato es de: {primerGato.Nombre}\n");
}

// Llamamos a las funciones que definimos arriba
VerInfoPacientes();
UsarWhere();

// Buscamos al animal de más edad
var mascotaVieja = ListaPacientes.OrderByDescending(p => p.Mascota.Edad).FirstOrDefault();
if (mascotaVieja != null)
{
    Console.WriteLine($"\nLa mascota de más edad es: {mascotaVieja.Mascota.Nombre} con {mascotaVieja.Mascota.Edad} años");
}

// Usamos sintaxis de consulta (estilo SQL) para los menores de 40
var pacientesMenores40 = from p in ListaPacientes
    where p.Edad < 40
    select p;
Console.WriteLine($"\nHay {pacientesMenores40.Count()} pacientes menores de 40 años\n");

// Encadenamiento pro: Filtro + Orden + Mayúsculas
var encadenamiento = ListaPacientes
    .Where(p => p.Mascota.Especie == "Perro")
    .OrderBy(p => p.Mascota.Nombre)
    .Select(p => new
    {
        NombreDueno = p.Nombre.ToUpper(),
        NombreMascota = p.Mascota.Nombre
    });

Console.WriteLine("--- Dueños (en Mayúsculas) ordenados por nombre del Perro ---");
foreach (var item in encadenamiento)
{
    Console.WriteLine($"Dueño: {item.NombreDueno} | Mascota: {item.NombreMascota}");
}
