using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class GymlgDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.OrderNumber);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    SalaryCode = table.Column<string>(type: "Varchar(5)", nullable: false),
                    SalaryType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PricesApply = table.Column<decimal>(type: "Money", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => new { x.SalaryCode, x.SalaryType });
                    table.UniqueConstraint("AK_Salaries_SalaryCode", x => x.SalaryCode);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackages",
                columns: table => new
                {
                    PackageCode = table.Column<string>(type: "Varchar(5)", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    MemberQuantity = table.Column<int>(type: "int", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AdminUpdate = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackages", x => new { x.PackageCode, x.PackageName });
                    table.UniqueConstraint("AK_ServicePackages_PackageCode", x => x.PackageCode);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountCode = table.Column<string>(type: "Varchar(10)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "Char(10)", maxLength: 10, nullable: false),
                    IdNumber = table.Column<string>(type: "Char(12)", maxLength: 12, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    LivingAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateBy = table.Column<string>(type: "Varchar(10)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SalaryCode = table.Column<string>(type: "Varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => new { x.AccountCode, x.PhoneNumber, x.IdNumber });
                    table.UniqueConstraint("AK_Accounts_AccountCode", x => x.AccountCode);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Salaries_SalaryCode",
                        column: x => x.SalaryCode,
                        principalTable: "Salaries",
                        principalColumn: "SalaryCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "Varchar(6)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityOfStaffs = table.Column<int>(type: "int", nullable: false),
                    QuantityOfPTs = table.Column<int>(type: "int", nullable: false),
                    QuantityOfWorkers = table.Column<int>(type: "int", nullable: false),
                    AdminUpdate = table.Column<string>(type: "Varchar(10)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => new { x.BranchCode, x.Address });
                    table.UniqueConstraint("AK_Branches_BranchCode", x => x.BranchCode);
                    table.ForeignKey(
                        name: "FK_Branches_Accounts_AdminUpdate",
                        column: x => x.AdminUpdate,
                        principalTable: "Accounts",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                columns: table => new
                {
                    EmpSalCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<DateOnly>(type: "date", nullable: false),
                    WorkDays = table.Column<int>(type: "int", nullable: false),
                    PriceTotals = table.Column<decimal>(type: "Money", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    ProofImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AccountCode = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.EmpSalCode);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Accounts_AccountCode",
                        column: x => x.AccountCode,
                        principalTable: "Accounts",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingChecks",
                columns: table => new
                {
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOf = table.Column<string>(type: "Varchar(10)", nullable: false),
                    IsCheckIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingChecks", x => x.OrderNumber);
                    table.ForeignKey(
                        name: "FK_WorkingChecks_Accounts_CheckOf",
                        column: x => x.CheckOf,
                        principalTable: "Accounts",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerCode = table.Column<string>(type: "Varchar(12)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "Char(10)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    BannedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "Varchar(6)", nullable: false),
                    UpdateBy = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => new { x.CustomerCode, x.PhoneNumber });
                    table.UniqueConstraint("AK_Customers_CustomerCode", x => x.CustomerCode);
                    table.ForeignKey(
                        name: "FK_Customers_Branches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "Branches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipCode = table.Column<string>(type: "Varchar(10)", nullable: false),
                    BranchCode = table.Column<string>(type: "Varchar(6)", nullable: false),
                    EquipName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffUpdate = table.Column<string>(type: "Varchar(10)", nullable: true),
                    AdminUpdate = table.Column<string>(type: "Varchar(10)", nullable: false),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipCode);
                    table.ForeignKey(
                        name: "FK_Equipment_Branches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "Branches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomersVouchers",
                columns: table => new
                {
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "Varchar(12)", nullable: false),
                    PackageCode = table.Column<string>(type: "Varchar(5)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateBy = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersVouchers", x => x.OrderNumber);
                    table.ForeignKey(
                        name: "FK_CustomersVouchers_Customers_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "Customers",
                        principalColumn: "CustomerCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersVouchers_ServicePackages_PackageCode",
                        column: x => x.PackageCode,
                        principalTable: "ServicePackages",
                        principalColumn: "PackageCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<string>(type: "Varchar(12)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompensated = table.Column<bool>(type: "bit", nullable: false),
                    AdminNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "Varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineCode);
                    table.ForeignKey(
                        name: "FK_Fines_Customers_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "Customers",
                        principalColumn: "CustomerCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SalaryCode",
                table: "Accounts",
                column: "SalaryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_AdminUpdate",
                table: "Branches",
                column: "AdminUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchCode",
                table: "Customers",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersVouchers_CustomerCode",
                table: "CustomersVouchers",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersVouchers_PackageCode",
                table: "CustomersVouchers",
                column: "PackageCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_AccountCode",
                table: "EmployeeSalaries",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_BranchCode",
                table: "Equipment",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CustomerCode",
                table: "Fines",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingChecks_CheckOf",
                table: "WorkingChecks",
                column: "CheckOf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersVouchers");

            migrationBuilder.DropTable(
                name: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "WorkingChecks");

            migrationBuilder.DropTable(
                name: "ServicePackages");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}
