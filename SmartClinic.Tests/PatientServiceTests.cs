using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Data;
using SmartClinic.API.Models;
using SmartClinic.API.Services;
using Xunit;

namespace SmartClinic.Tests;

public class PatientServiceTests
{
    private SmartClinicDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<SmartClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new SmartClinicDbContext(options);
    }

    [Fact]
    public async Task AddPatient_ShouldAddPatient()
    {
        var context = GetDbContext();
        var service = new PatientService(context);

        var patient = new Patient
        {
            FullName = "Test Patient",
            DateOfBirth = DateTime.Now
        };

        var result = await service.AddPatientAsync(patient);

        Assert.NotNull(result);
        Assert.Equal(1, context.Patients.Count());
    }

    [Fact]
    public async Task GetAllPatients_ShouldReturnList()
    {
        var context = GetDbContext();
        var service = new PatientService(context);

        context.Patients.Add(new Patient { FullName = "P1", DateOfBirth = DateTime.Now });
        context.Patients.Add(new Patient { FullName = "P2", DateOfBirth = DateTime.Now });
        await context.SaveChangesAsync();

        var result = await service.GetAllPatientsAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetPatientById_ShouldReturnPatient()
    {
        var context = GetDbContext();
        var service = new PatientService(context);

        var patient = new Patient { FullName = "P1", DateOfBirth = DateTime.Now };
        context.Patients.Add(patient);
        await context.SaveChangesAsync();

        var result = await service.GetPatientByIdAsync(patient.PatientId);

        Assert.NotNull(result);
        Assert.Equal("P1", result.FullName);
    }

    [Fact]
    public async Task DeletePatient_ShouldRemovePatient()
    {
        var context = GetDbContext();
        var service = new PatientService(context);

        var patient = new Patient { FullName = "P1", DateOfBirth = DateTime.Now };
        context.Patients.Add(patient);
        await context.SaveChangesAsync();

        var deleted = await service.DeletePatientAsync(patient.PatientId);

        Assert.True(deleted);
        Assert.Empty(context.Patients);
    }

    [Fact]
    public async Task DeletePatient_NotFound_ShouldReturnFalse()
    {
        var context = GetDbContext();
        var service = new PatientService(context);

        var result = await service.DeletePatientAsync(999);

        Assert.False(result);
    }
}