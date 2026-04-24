namespace HistoriaUsuario2.Models;

public class Paciente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public Mascota? Mascota { get; set; }
}