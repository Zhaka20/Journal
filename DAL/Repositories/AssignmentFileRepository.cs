﻿using Journal.DAL.Repositories.Common;
using Journal.AbstractDAL.AbstractRepositories;
using Journal.DataModel.Models;
using Journal.DAL.Context;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Journal.DAL.Repositories
{
    public class AssignmentFileRepository : GenericRepository<AssignmentFile, int>, IAssignmentFileRepository, IDisposable
    {
        public AssignmentFileRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}