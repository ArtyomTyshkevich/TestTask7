using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Resources_ResourceId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Units_UnitId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_IncomingDocuments_IncomingDocumentId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_Resources_ResourceId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_Units_UnitId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingDocuments_Clients_ClientId",
                table: "OutgoingDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingDocuments_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingResources_Resources_ResourceId",
                table: "OutgoingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingResources_Units_UnitId",
                table: "OutgoingResources");

            migrationBuilder.DropIndex(
                name: "IX_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Balances_ResourceId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "OutgoingDocumentId",
                table: "OutgoingDocuments");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Units",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "OutgoingDocumentId",
                table: "OutgoingResources",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "OutgoingDocuments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "IncomingDocuments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Name",
                table: "Resources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingResources_OutgoingDocumentId",
                table: "OutgoingResources",
                column: "OutgoingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingDocuments_Number",
                table: "OutgoingDocuments",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomingDocuments_Number",
                table: "IncomingDocuments",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_ResourceId_UnitId",
                table: "Balances",
                columns: new[] { "ResourceId", "UnitId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Resources_ResourceId",
                table: "Balances",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Units_UnitId",
                table: "Balances",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_IncomingDocuments_IncomingDocumentId",
                table: "IncomingResources",
                column: "IncomingDocumentId",
                principalTable: "IncomingDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_Resources_ResourceId",
                table: "IncomingResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_Units_UnitId",
                table: "IncomingResources",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingDocuments_Clients_ClientId",
                table: "OutgoingDocuments",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingResources_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingResources",
                column: "OutgoingDocumentId",
                principalTable: "OutgoingDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingResources_Resources_ResourceId",
                table: "OutgoingResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingResources_Units_UnitId",
                table: "OutgoingResources",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Resources_ResourceId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Units_UnitId",
                table: "Balances");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_IncomingDocuments_IncomingDocumentId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_Resources_ResourceId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingResources_Units_UnitId",
                table: "IncomingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingDocuments_Clients_ClientId",
                table: "OutgoingDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingResources_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingResources_Resources_ResourceId",
                table: "OutgoingResources");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingResources_Units_UnitId",
                table: "OutgoingResources");

            migrationBuilder.DropIndex(
                name: "IX_Units_Name",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Resources_Name",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_OutgoingResources_OutgoingDocumentId",
                table: "OutgoingResources");

            migrationBuilder.DropIndex(
                name: "IX_OutgoingDocuments_Number",
                table: "OutgoingDocuments");

            migrationBuilder.DropIndex(
                name: "IX_IncomingDocuments_Number",
                table: "IncomingDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Name",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Balances_ResourceId_UnitId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "OutgoingDocumentId",
                table: "OutgoingResources");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "OutgoingDocuments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "OutgoingDocumentId",
                table: "OutgoingDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "IncomingDocuments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingDocuments",
                column: "OutgoingDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Balances_ResourceId",
                table: "Balances",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Resources_ResourceId",
                table: "Balances",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Units_UnitId",
                table: "Balances",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_IncomingDocuments_IncomingDocumentId",
                table: "IncomingResources",
                column: "IncomingDocumentId",
                principalTable: "IncomingDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_Resources_ResourceId",
                table: "IncomingResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingResources_Units_UnitId",
                table: "IncomingResources",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingDocuments_Clients_ClientId",
                table: "OutgoingDocuments",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingDocuments_OutgoingDocuments_OutgoingDocumentId",
                table: "OutgoingDocuments",
                column: "OutgoingDocumentId",
                principalTable: "OutgoingDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingResources_Resources_ResourceId",
                table: "OutgoingResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingResources_Units_UnitId",
                table: "OutgoingResources",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
