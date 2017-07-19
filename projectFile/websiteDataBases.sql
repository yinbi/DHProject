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
LoginName nvarchar(20) not null constraint UQ_Admins_LoginName unique,
LoginPwd nvarchar(34) not null,
RealName nvarchar(20) not null,
RoleId int not null constraint FK_Admins_RoleId FOREIGN KEY(RoleId) references Roles(Id),
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
--Modules_url nvarchar(100) not null,
Controller nvarchar(20) not null,
[Action] nvarchar(20) not null,
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


--INSERT INTO DHWebSiteDB.dbo.Admins(LoginName,LoginPwd,RealName,RoleId,LastLoginTime,LastLoginIp,ErrNum,[Enable])
--values('admin','1zYxu0CW5FsGf6ge2S/uBw==','ceshi',1,getdate(),'127.0.0.1',0,1)
--UPDATE DHWebSiteDB.dbo.Admins SET RoleId=1 WHERE Id=1

--INSERT INTO DHWebSiteDB.dbo.Admins(LoginName,LoginPwd,RealName,RoleId,LastLoginTime,LastLoginIp,ErrNum,[Enable])
--values('test','1zYxu0CW5FsGf6ge2S/uBw==','test001',2,getdate(),'127.0.0.1',0,1)

--INSERT INTO DHWebSiteDB.dbo.Roles(RoleName,RoleDesc,Nullity) VALUES('��������Ա','ӵ������Ȩ��',0)
--INSERT INTO DHWebSiteDB.dbo.Roles(RoleName,RoleDesc,Nullity) VALUES('��ͨ����Ա','ӵ����ͨ����Ȩ��',0)

SELECT * FROM DHWebSiteDB.dbo.Admins
SELECT * FROM DHWebSiteDB.dbo.Modules
SELECT * FROM DHWebSiteDB.dbo.Roles
SELECT * FROM DHWebSiteDB.dbo.RolesPermission

SELECT * FROM DHWebSiteDB.dbo.Admins
SELECT * FROM DHWebSiteDB.dbo.Roles
SELECT * FROM DHWebSiteDB.dbo.Modules

--INSERT INTO DHWebSiteDB.dbo.Modules(ParentId,Title,Controller,[Action],OrderNo,Nullity)
--VALUES(0,'�û�����','','',0,0)