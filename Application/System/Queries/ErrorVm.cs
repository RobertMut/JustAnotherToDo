using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace JustAnotherToDo.Application.System.Queries
{
    public class ErrorVm
    {
        public ErrorVm()
        {
        }

        public ErrorVm(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }
}
