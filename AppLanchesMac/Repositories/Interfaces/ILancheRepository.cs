﻿using AppLanchesMac.Models;

namespace AppLanchesMac.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        
        Lanche GetLancheById(int id);
    
    }
}
