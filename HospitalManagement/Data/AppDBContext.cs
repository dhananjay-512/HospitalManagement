using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Disease>? Diseases { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
    }

}
