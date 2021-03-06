﻿using JokeApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokeApi.Interfaces
{
    public interface IJokeFileReader
    {
        Task<IEnumerable<Joke>> GetJokesAsync();
    }
}
