using PlanoriaCapstone.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string correo, string password);
    }
}
