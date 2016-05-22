using System;
using System.Data.Entity.ModelConfiguration;
using SportsStore.Models.Domain;


namespace SportsStore.Models.DAL.Mapper
{
    public class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            ToTable("Product");
            HasKey(t => t.ProductId);
            Property(t => t.ImageData).HasColumnType("image");
            Property(t => t.ImageMimeType).HasColumnType("varchar").HasMaxLength(50);
            Property(t=>t.Name).IsRequired().HasMaxLength(100);
        }
    }
}