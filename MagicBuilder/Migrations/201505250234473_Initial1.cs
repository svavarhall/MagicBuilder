namespace MagicBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        MultiverseId = c.String(),
                        Deck_DeckID = c.Int(),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.Decks", t => t.Deck_DeckID)
                .Index(t => t.Deck_DeckID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "Deck_DeckID", "dbo.Decks");
            DropIndex("dbo.Cards", new[] { "Deck_DeckID" });
            DropTable("dbo.Cards");
        }
    }
}
