﻿using System;
using System.Linq;
using Umbraco.Core.Configuration;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Umbraco.Core.Persistence.Migrations.Upgrades.TargetVersionSevenThreeZero
{
    [Migration("7.3.0", 3, Constants.System.UmbracoMigrationName)]
    public class RemoveStylesheetDataAndTables : MigrationBase
    {
        public RemoveStylesheetDataAndTables(IMigrationContext context)
            : base(context)
        { }

        public override void Up()
        {
            //Clear all stylesheet data if the tables exist
            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();
            if (tables.InvariantContains("cmsStylesheetProperty"))
            {
                Delete.FromTable("cmsStylesheetProperty").AllRows();
                Delete.FromTable("umbracoNode").Row(new { nodeObjectType = Constants.ObjectTypes.StylesheetProperty });

                Delete.Table("cmsStylesheetProperty");
            }
            if (tables.InvariantContains("cmsStylesheet"))
            {
                Delete.FromTable("cmsStylesheet").AllRows();
                Delete.FromTable("umbracoNode").Row(new { nodeObjectType = Constants.ObjectTypes.Stylesheet });

                Delete.Table("cmsStylesheet");
            }
        }

        public override void Down()
        {
            throw new NotSupportedException("Cannot downgrade from 7.3 as there are database table deletions");
        }
    }
}
