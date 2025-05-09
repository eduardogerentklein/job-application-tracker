using System.Reflection;
using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Database;
using Web.Api;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(JobApplication).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IServiceBase).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly WebApiAssembly = typeof(Program).Assembly;
}