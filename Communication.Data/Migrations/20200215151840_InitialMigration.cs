using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Communication.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ChannelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ChannelId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMembers_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersMessages_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersMessages_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsInRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsInRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionsInRoles_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionsInRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelMembersRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChannelMemberId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMembersRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMembersRoles_ChannelMembers_ChannelMemberId",
                        column: x => x.ChannelMemberId,
                        principalTable: "ChannelMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChannelMembersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ChannelId = table.Column<Guid>(nullable: false),
                    CreatedByMemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_ChannelMembers_CreatedByMemberId",
                        column: x => x.CreatedByMemberId,
                        principalTable: "ChannelMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomRolesAccesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRolesAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomRolesAccesses_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomRolesAccesses_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomsMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChannelMemberId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    RoomId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomsMessages_ChannelMembers_ChannelMemberId",
                        column: x => x.ChannelMemberId,
                        principalTable: "ChannelMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomsMessages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomsMessages_Rooms_RoomId1",
                        column: x => x.RoomId1,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembers_ChannelId",
                table: "ChannelMembers",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembers_UserId",
                table: "ChannelMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembersRoles_ChannelMemberId",
                table: "ChannelMembersRoles",
                column: "ChannelMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembersRoles_RoleId",
                table: "ChannelMembersRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsInRoles_PermissionId",
                table: "PermissionsInRoles",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsInRoles_RoleId",
                table: "PermissionsInRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ChannelId",
                table: "Roles",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRolesAccesses_RoleId",
                table: "RoomRolesAccesses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRolesAccesses_RoomId",
                table: "RoomRolesAccesses",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ChannelId",
                table: "Rooms",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatedByMemberId",
                table: "Rooms",
                column: "CreatedByMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsMessages_ChannelMemberId",
                table: "RoomsMessages",
                column: "ChannelMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsMessages_RoomId",
                table: "RoomsMessages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsMessages_RoomId1",
                table: "RoomsMessages",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMessages_FromUserId",
                table: "UsersMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMessages_ToUserId",
                table: "UsersMessages",
                column: "ToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMembersRoles");

            migrationBuilder.DropTable(
                name: "PermissionsInRoles");

            migrationBuilder.DropTable(
                name: "RoomRolesAccesses");

            migrationBuilder.DropTable(
                name: "RoomsMessages");

            migrationBuilder.DropTable(
                name: "UsersMessages");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "ChannelMembers");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
