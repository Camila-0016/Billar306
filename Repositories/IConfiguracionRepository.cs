namespace Billar306.API.Repositories
{
    public interface IConfiguracionRepository
    {
        Task<string?> ObtenerValorAsync(string clave);
        Task<decimal> ObtenerDecimalAsync(string clave, decimal valorDefecto = 0);
        Task<int> ObtenerEnteroAsync(string clave, int valorDefecto = 0);
        Task ActualizarValorAsync(string clave, string nuevoValor);
        Task GuardarCambiosAsync();
    }
}