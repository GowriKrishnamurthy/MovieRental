namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailableToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: false));
            // Sql("update Movies set NumberAvailable = NumberInStock");

            // Update movies availability for the current rentals
            Sql(@"update Movies set NumberAvailable = NumberInStock - r.count from
                    Movies m
                    Inner join 
                    (
                        select Movie_Id, count(Id) as count from Rental where DateReturned IS NULL group by Movie_Id
                    )   r on r.Movie_Id = m.Id");
        }

        public override void Down()
        {
        }
    }
}
