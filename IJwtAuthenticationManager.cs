﻿namespace Encuestas
{
    public interface IJwtAuthenticationManager
    {
       string Authenticate(string username, string password);
    }
}
