using HistoriaUsuario2.Models;
using System.Linq;

// Creación de la lista de pacientes de tipo Pacientes, esta traerá dentro de si para cada paciente su mascota 
List<Paciente> ListaPacientes = new List<Paciente>
{
    // Para no escribir Add. para cada paciente, usamos las llaves, esto permite declarar cada elemento de forma más visual
    // Además se pasa el dato así: Id = 1, ya que las clases non les generé constructor, de manera que hay que ser implicitos, 
    // Fue mejor así para que se viera claro que dato pertenece a que campo
    new Paciente
    {
        Id = 1, Nombre = "Ana García", Edad = 30,
        Mascota = new Mascota
            { Id = 101, Nombre = "Luna", Especie = "Perro", Raza = "Labrador", Edad = 5, Sintoma = "Dolor de pata" }
    },
    new Paciente
    {
        Id = 2, Nombre = "Luis Pérez", Edad = 45,
        Mascota = new Mascota
            { Id = 102, Nombre = "Michi", Especie = "Gato", Raza = "Persa", Edad = 2, Sintoma = "No quiere comer" }
    },
    new Paciente
    {
        Id = 3, Nombre = "Carla Ruiz", Edad = 25,
        Mascota = new Mascota
            { Id = 103, Nombre = "Rex", Especie = "Perro", Raza = "Pastor Alemán", Edad = 8, Sintoma = "Vacunación" }
    },
    new Paciente
    {
        Id = 4, Nombre = "Juan Mora", Edad = 50,
        Mascota = new Mascota
            { Id = 104, Nombre = "Pipo", Especie = "Loro", Raza = "Cabeza Azul", Edad = 1, Sintoma = "Plumas caídas" }
    },
    new Paciente
    {
        Id = 5, Nombre = "Marta Soto", Edad = 28,
        Mascota = new Mascota
            { Id = 105, Nombre = "Bruno", Especie = "Perro", Raza = "Golden", Edad = 3, Sintoma = "Tos" }
    }
};

// Usamos el ID del paciente como llave (Key) y el objeto Paciente como valor (Value)
Dictionary<int, Paciente> diccionarioPacientes = ListaPacientes.ToDictionary(p => p.Id);

void VerInfoPacientes()
{
    foreach (var paciente in diccionarioPacientes.Values)
    {
        Console.WriteLine(@$"
Paciente: {paciente.Nombre} | Edad: {paciente.Edad} 
Mascota: {paciente.Mascota.Nombre} | Especie: {paciente.Mascota.Especie} | Raza: {paciente.Mascota.Raza} | Edad: {paciente.Mascota.Edad} | Sintoma: {paciente.Mascota.Sintoma}
");
    }
}

void UsarWhere()
{
// 1. Filtrar: Solo perros
// 2. Ordenar: Por edad de la mascota de menor a mayor
// 3. Proyectar: Solo queremos el nombre del dueño y el nombre de su mascota
    var consultaPerros = ListaPacientes
        .Where(p => p.Mascota.Especie == "Perro") // Filtro
        .OrderBy(p => p.Mascota.Edad) // Orden
        .Select(p => new
        {
            // Proyección (creamos un objeto anónimo)
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

// Agrupamos a los pacientes según la especie de su mascota
var gruposPorEspecie = ListaPacientes.GroupBy(p => p.Mascota.Especie);

Console.WriteLine("--- Conteo por Especies ---");
foreach (var grupo in gruposPorEspecie)
{
    // grupo.Key es la especie (Perro, Gato, etc.)
    // grupo.Count() nos dice cuántos hay en ese grupo
    Console.WriteLine($"Especie: {grupo.Key} | Cantidad: {grupo.Count()}");
}

// 1. ¿Hay alguna mascota que sea un 'Loro'?
bool hayLoros = ListaPacientes.Any(p => p.Mascota.Especie == "Loro");
Console.WriteLine($"¿Tenemos loros en la clínica?: {(hayLoros ? "Sí" : "No")}");

// 2. Buscar el primer paciente que tenga un gato (o null si no hay)
var primerGato = ListaPacientes.FirstOrDefault(p => p.Mascota.Especie == "Gato");

if (primerGato != null)
{
    Console.WriteLine($"El primer gato es de: {primerGato.Nombre}");
}

VerInfoPacientes();
UsarWhere();

var mascotaVieja = ListaPacientes.OrderByDescending(p => p.Mascota.Edad).FirstOrDefault();
Console.WriteLine($"\n{mascotaVieja.Mascota.Nombre} {mascotaVieja.Mascota.Edad}");

var pacientesMenores40 = from p in ListaPacientes
    where p.Edad < 40
    select p;
Console.WriteLine($"\nHay {pacientesMenores40.Count()} menores de 40 años\n");

var encadenamiento = ListaPacientes
    .Where(p => p.Mascota.Especie == "Perro")
    .OrderBy(p => p.Mascota.Nombre)
    .Select(p => new
    {
        NombreDueno = p.Nombre.ToUpper(),
        NombreMascota = p.Mascota.Nombre
    });
Console.WriteLine("--- Dueños ordenados por nombre del Perro ---");
foreach (var item in encadenamiento)
{
    Console.WriteLine($"Dueño: {item.NombreDueno} | Mascota: {item.NombreMascota}");
}