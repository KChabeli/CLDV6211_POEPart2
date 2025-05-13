namespace EventEaseBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        NumberOfTickets = c.Int(nullable: false),
                        BookingDate = c.DateTime(),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 100),
                        EventDate = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 255),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.String(nullable: false, maxLength: 200),
                        Capacity = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.VenueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Bookings", "EventId", "dbo.Events");
            DropIndex("dbo.Events", new[] { "VenueId" });
            DropIndex("dbo.Bookings", new[] { "EventId" });
            DropTable("dbo.Venues");
            DropTable("dbo.Events");
            DropTable("dbo.Bookings");
        }
    }
}
