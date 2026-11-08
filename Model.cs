namespace PracticaAPI
{
    public class Model
    {
        public class Usuario
        {
            public int Id { get; set; }
            public string Correo { get; set; } = "";
            public string ClaveHash { get; set; } = "";
            public string Nombre { get; set; } = "";
            public bool Disponible { get; set; } = true;

            public int AniosExperiencia { get; set; } = 0;
        }
        // DTO = son los datos que llegan o salen por internet (JSON)
        public record RegistroDto(string Nombre, string Correo, string Clave);
        public record LoginDto(string Correo, string Clave);
        public record PerfilDto(string Nombre, bool Disponible, int AniosExperiencia);

    }
}
