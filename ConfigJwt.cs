using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PracticaAPI
{
    public class ConfigJwt
    {
        //llave secreta: con ella el servidor firma los tokens y luego comprueba que nadie los altero
        //Debe tener 32 caracteres o mas. En un proyecto real no va en el codigo(se usan user-secrets)
        public static readonly SymmetricSecurityKey Llave = 
        new(Encoding.UTF8.GetBytes("Llave-secreta-de-la-practica-maui-cambiala"));
    }
}
