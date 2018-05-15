namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0891d53a-2053-445f-a47f-65efb34b2315', N'CanManageMovies')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4119447a-4405-4a64-a780-583c559e1c30', N'guest@movierental.com', 0, N'AFSq5KvIyP9dgE22dnwXAWfWJEjBCGM2D8LrWAozcfY7AV3YmP03io5jJZtDu8+7EQ==', N'6fe4a828-623c-455c-8af8-6d071d28333c', NULL, 0, 0, NULL, 1, 0, N'guest@movierental.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fbde9056-7338-4082-bfb8-8fd867103cb5', N'admin@movierental.com', 0, N'AH7Jwaq7RSfbmT+hG/vSQXSpUWNmDcQXuIWiDbqt2WBTJl40JNNA0nVhkxa/LMgxvw==', N'377b5e8d-84d6-4ed5-9036-acb0e6a01559', NULL, 0, 0, NULL, 1, 0, N'admin@movierental.com')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fbde9056-7338-4082-bfb8-8fd867103cb5', N'0891d53a-2053-445f-a47f-65efb34b2315')
            ");
        }
        
        public override void Down()
        {

        }
    }
}
