﻿using Microsoft.EntityFrameworkCore;
using SkiAppServer.Models;

namespace SkiAppServer.Models
{
    public partial class SkiDBContext : DbContext
    {
        public Visitor? GetVisitor(string password)
        {
            return this.Visitors.Where(u => u.Pass == password).FirstOrDefault();
        }
    }
}
