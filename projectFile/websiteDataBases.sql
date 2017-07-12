if exists(select 1 from sysobjects where name='Admins')
drop table Admins
go
-- =============================================
-- Author:		 
-- Create date: 
-- Description:  ����Ա�б�
-- =============================================
CREATE TABLE Admins
(
Id int not null identity(1,1) constraint pk_Admins_id  primary key,
LoginName nvarchar(20) not null,
LoginPwd nvarchar(34) not null,
RealName nvarchar(20) not null,
RoleId int not null,
LastLoginTime datetime not null,
LastLoginIp nvarchar(15) not null,
ErrNum int not null,
[Enable] bit not null,
CreateTime datetime not null default(getdate())	--����ʱ��
)

--========================================================================
if exists(select 1 from sysobjects where name='Modules')
drop table Modules
go
-- =============================================
-- Author:		 
-- Create date: 
-- Description:  ģ��
-- =============================================
CREATE TABLE Modules
(
Id int not null identity(1,1) constraint pk_Modules_id  primary key,
ParentId int not null,
Title nvarchar(30) not null,
Modules_url nvarchar(100) not null,
OrderNo int not null,
Nullity bit not null,
CreateTime datetime not null default(getdate())	--����ʱ��
)
--========================================================================
if exists(select 1 from sysobjects where name='Roles')
drop table Roles
go
-- =============================================
-- Author:		 
-- Create date: 
-- Description:  ��ɫ
-- =============================================
CREATE TABLE Roles
(
Id int not null identity(1,1) constraint pk_Roles_id  primary key,
RoleName nvarchar(30) not null,
RoleDesc nvarchar(100),
Nullity bit not null,
CreateTime datetime not null default(getdate())	--����ʱ��
)

--========================================================================
if exists(select 1 from sysobjects where name='RolesPermission')
drop table RolesPermission
go
-- =============================================
-- Author:		 
-- Create date: 
-- Description:  ��ɫģ��Ȩ��
-- =============================================
CREATE TABLE RolesPermission
(
Id int not null identity(1,1) constraint pk_RolesPermission_id  primary key,
RoleId int not null,
ModuleId int not null,
ActionValueSum int not null,
CreateTime datetime not null default(getdate())	--����ʱ��
)
--========================================================================

SELECT * FROM DHWebSiteDB.dbo.Admins
SELECT * FROM DHWebSiteDB.dbo.Modules
SELECT * FROM DHWebSiteDB.dbo.Roles
SELECT * FROM DHWebSiteDB.dbo.RolesPermission