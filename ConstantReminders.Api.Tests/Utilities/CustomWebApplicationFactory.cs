#region Copyright
// ---------------------------------------------------------------------------
// Copyright (c) 2025 Battleline Productions LLC. All rights reserved.
//
// Licensed under the Battleline Productions LLC license agreement.
// See LICENSE file in the project root for full license information.
//
// Author: Michael Cavanaugh
// Company: Battleline Productions LLC
// Date: 01/03/2025
// Solution Name: ConstantReminders-Api
// Project Name: ConstantReminders.Api.Tests
// File: CustomWebApplicationFactory.cs
// File Path: C:\git\codingyourcareer\ConstantReminders-Api\ConstantReminders.Api.Tests\Utilities\CustomWebApplicationFactory.cs
// ---------------------------------------------------------------------------
#endregion

using ConstantReminders.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConstantReminders.Api.Tests.Utilities;

public class CustomWebApplicationFactory<TEntryPoint>
    : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        //builder.ConfigureAppConfiguration((context, configBuilder) =>
        //{
        //    var testConfig = new Dictionary<string, string>
        //    {
        //        // The typical path for connection strings:
        //        { "ConnectionStrings:postgresdb", "Host=localhost;Port=5432;Database=TestDb;User ID=postgres;Password=Pass123;" },

        //        // One of the Aspire fallback paths:
        //        { "Aspire:Npgsql:EntityFrameworkCore:PostgreSQL:AppDbContext:ConnectionString", "Host=localhost;Port=5432;Database=TestDb;User ID=postgres;Password=Pass123;" },
        //    };

        //    configBuilder.AddInMemoryCollection(testConfig);
        //});

        builder.UseEnvironment("UnitTest");

        return base.CreateHost(builder);
    }
}