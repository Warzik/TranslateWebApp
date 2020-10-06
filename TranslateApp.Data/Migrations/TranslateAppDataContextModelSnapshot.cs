﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TranslateApp.Data;

namespace TranslateApp.Data.Migrations
{
    [DbContext(typeof(TranslateAppDataContext))]
    partial class TranslateAppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TranslateApp.Data.Models.ProjectDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AutomaticallyTranslate");

                    b.Property<string>("Description");

                    b.Property<string>("FromLanguage");

                    b.Property<int>("NumberOfTranslations");

                    b.Property<string>("Title");

                    b.Property<string>("ToLanguage");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TranslateApp.Data.Models.TranslateDAL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId");

                    b.Property<string>("TextToTranslate");

                    b.Property<int>("TranslateApi");

                    b.Property<string>("TranslatedText");

                    b.HasKey("Id");

                    b.ToTable("Translate");
                });
#pragma warning restore 612, 618
        }
    }
}
