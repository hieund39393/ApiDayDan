﻿using Authentication.Application.Model.Module;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.Repositories;
using EVN.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Application.Queries.ModuleQuery
{
    public interface IModuleQuery
    {
        Task<List<ModuleResponse>> GetListModule();
    }
    public class ModuleQuery : IModuleQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExOneDbContext _context;

        public ModuleQuery(IUnitOfWork unitOfWork, ExOneDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<List<ModuleResponse>> GetListModule()
        {

            var query = _unitOfWork.ModuleRepository.GetQuery()
           .Select(x => new ModuleResponse()
           {
               Icon = x.Icon,
           }).ToList();




            return query;
        }
    }
}
