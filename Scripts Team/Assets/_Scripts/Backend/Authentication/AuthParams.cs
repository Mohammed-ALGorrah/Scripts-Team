using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Backend.Authentication
{
    public class RegisterParams
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginParams
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

}