namespace MagicBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MTGCards : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Cards");
            DropColumn("dbo.Cards", "CardID");
            DropColumn("dbo.Cards", "MultiverseId");
            AddColumn("dbo.Cards", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Cards", "RelatedCardId", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "SetNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "Name", c => c.String());
            AddColumn("dbo.Cards", "Description", c => c.String());
            AddColumn("dbo.Cards", "Flavor", c => c.String());
            AddColumn("dbo.Cards", "ManaCost", c => c.String());
            AddColumn("dbo.Cards", "ConvertedManaCost", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "CardSetName", c => c.String());
            AddColumn("dbo.Cards", "Type", c => c.String());
            AddColumn("dbo.Cards", "SubType", c => c.String());
            AddColumn("dbo.Cards", "Power", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "Toughness", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "Loyalty", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "Rarity", c => c.String());
            AddColumn("dbo.Cards", "Artist", c => c.String());
            AddColumn("dbo.Cards", "Token", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "CardSetId", c => c.String());
            AddColumn("dbo.Cards", "ReleasedAt", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Cards", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "MultiverseId", c => c.String());
            AddColumn("dbo.Cards", "CardID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Cards");
            DropColumn("dbo.Cards", "ReleasedAt");
            DropColumn("dbo.Cards", "CardSetId");
            DropColumn("dbo.Cards", "Token");
            DropColumn("dbo.Cards", "Artist");
            DropColumn("dbo.Cards", "Rarity");
            DropColumn("dbo.Cards", "Loyalty");
            DropColumn("dbo.Cards", "Toughness");
            DropColumn("dbo.Cards", "Power");
            DropColumn("dbo.Cards", "SubType");
            DropColumn("dbo.Cards", "Type");
            DropColumn("dbo.Cards", "CardSetName");
            DropColumn("dbo.Cards", "ConvertedManaCost");
            DropColumn("dbo.Cards", "ManaCost");
            DropColumn("dbo.Cards", "Flavor");
            DropColumn("dbo.Cards", "Description");
            DropColumn("dbo.Cards", "Name");
            DropColumn("dbo.Cards", "SetNumber");
            DropColumn("dbo.Cards", "RelatedCardId");
            DropColumn("dbo.Cards", "Id");
            AddPrimaryKey("dbo.Cards", "CardID");
        }
    }
}
