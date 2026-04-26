using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Data;
using SmartClinic.API.Models;
using SmartClinic.API.Services;
using Xunit;

namespace SmartClinic.Tests;

public class DoctorServiceTests
{
    private SmartClinicDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<SmartClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SmartClinicDbContext(options);
    }

    [Fact]
    public async Task AddDoctor_ShouldAddDoctor()
    {
        var context = GetDbContext();
        var service = new DoctorService(context);

        // لازم Department لأن في FK
        context.Departments.Add(new Department { Name = "Test Dept" });
        await context.SaveChangesAsync();

        var doctor = new Doctor
        {
            FullName = "Dr. Test",
            DepartmentId = context.Departments.First().DepartmentId
        };

        var result = await service.AddDoctorAsync(doctor);

        Assert.NotNull(result);
        Assert.Equal(1, context.Doctors.Count());
    }

    [Fact]
    public async Task GetAllDoctors_ShouldReturnDoctors()
    {
        var context = GetDbContext();
        var service = new DoctorService(context);

        context.Departments.Add(new Department { Name = "Dept" });
        await context.SaveChangesAsync();

        var deptId = context.Departments.First().DepartmentId;

        context.Doctors.Add(new Doctor { FullName = "D1", DepartmentId = deptId });
        context.Doctors.Add(new Doctor { FullName = "D2", DepartmentId = deptId });
        await context.SaveChangesAsync();

        var result = await service.GetAllDoctorsAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetDoctorById_ShouldReturnDoctor()
    {
        var context = GetDbContext();
        var service = new DoctorService(context);

        context.Departments.Add(new Department { Name = "Dept" });
        await context.SaveChangesAsync();

        var doctor = new Doctor
        {
            FullName = "Dr. One",
            DepartmentId = context.Departments.First().DepartmentId
        };

        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();

        var result = await service.GetDoctorByIdAsync(doctor.DoctorId);

        Assert.NotNull(result);
        Assert.Equal("Dr. One", result.FullName);
    }
}