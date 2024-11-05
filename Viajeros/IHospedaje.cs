﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Viajeros.Autenticacion;

namespace Viajeros
{
    internal interface IHospedaje
    {
        Task<string> Login(string username, string password);
        Task<bool> EnviarParteViajeros(ParteViajeros parte);
    }
}