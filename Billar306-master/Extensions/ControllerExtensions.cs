using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Billar306.API.Extensions
{
    public static class ControllerExtensions
    {
        public static int UsuarioIdActual(this ControllerBase controller)
            => int.Parse(controller.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}