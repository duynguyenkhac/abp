﻿using System;
using Volo.Abp.Data;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.IdentityServer.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Volo.Abp.IdentityServer
{

    [DependsOn(
        typeof(AbpIdentityServerTestBaseModule),
        typeof(AbpIdentityServerMongoDbModule),
        typeof(AbpIdentityMongoDbModule)
    )]
    public class AbpIdentityServerMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/')  +
                                   "Db_" +
                                   Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });   
        }
    }
}
