﻿// <auto-generated />
using BTG.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTG.Infrastructure.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20250111235753_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BTG.Domain.Entities.ClienteEntity", b =>
                {
                    b.Property<int>("CodigoCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCliente"));

                    b.HasKey("CodigoCliente");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("BTG.Domain.Entities.DetalhesPedidoEntity", b =>
                {
                    b.Property<int>("CodigoPedido")
                        .HasColumnType("int");

                    b.Property<int>("CodigoProduto")
                        .HasColumnType("int");

                    b.HasKey("CodigoPedido", "CodigoProduto");

                    b.HasIndex("CodigoProduto");

                    b.ToTable("DetalhesPedido", (string)null);
                });

            modelBuilder.Entity("BTG.Domain.Entities.PedidoEntity", b =>
                {
                    b.Property<int>("CodigoPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoPedido"));

                    b.Property<int>("CodigoCliente")
                        .HasColumnType("int");

                    b.HasKey("CodigoPedido");

                    b.HasIndex("CodigoCliente");

                    b.ToTable("Pedido", (string)null);
                });

            modelBuilder.Entity("BTG.Domain.Entities.ProdutoEntity", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoProduto"));

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("CodigoProduto");

                    b.ToTable("Produto", (string)null);
                });

            modelBuilder.Entity("BTG.Domain.Entities.DetalhesPedidoEntity", b =>
                {
                    b.HasOne("BTG.Domain.Entities.PedidoEntity", "Pedido")
                        .WithMany("DetalhesPedidos")
                        .HasForeignKey("CodigoPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BTG.Domain.Entities.ProdutoEntity", "Produto")
                        .WithMany("DetalhesPedidos")
                        .HasForeignKey("CodigoProduto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("BTG.Domain.Entities.PedidoEntity", b =>
                {
                    b.HasOne("BTG.Domain.Entities.ClienteEntity", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("CodigoCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BTG.Domain.Entities.ClienteEntity", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("BTG.Domain.Entities.PedidoEntity", b =>
                {
                    b.Navigation("DetalhesPedidos");
                });

            modelBuilder.Entity("BTG.Domain.Entities.ProdutoEntity", b =>
                {
                    b.Navigation("DetalhesPedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
