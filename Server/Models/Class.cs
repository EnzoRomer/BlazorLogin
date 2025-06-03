namespace BlazorLogin.Server.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
    }
}
