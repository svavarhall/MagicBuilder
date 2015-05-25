namespace MagicBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        DeckID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeckID);
            
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

        }
    }
}
