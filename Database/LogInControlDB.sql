USE [master]
GO
/****** Object:  Database [LogInControl]    Script Date: 07/08/2015 12:39:35 ******/
CREATE DATABASE [LogInControl] ON  PRIMARY 
( NAME = N'LogInControl', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\LogInControl.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LogInControl_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\LogInControl_1.ldf' , SIZE = 2304KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LogInControl] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LogInControl].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LogInControl] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [LogInControl] SET ANSI_NULLS OFF
GO
ALTER DATABASE [LogInControl] SET ANSI_PADDING OFF
GO
ALTER DATABASE [LogInControl] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [LogInControl] SET ARITHABORT OFF
GO
ALTER DATABASE [LogInControl] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [LogInControl] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [LogInControl] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [LogInControl] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [LogInControl] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [LogInControl] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [LogInControl] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [LogInControl] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [LogInControl] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [LogInControl] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [LogInControl] SET  DISABLE_BROKER
GO
ALTER DATABASE [LogInControl] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [LogInControl] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [LogInControl] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [LogInControl] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [LogInControl] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [LogInControl] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [LogInControl] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [LogInControl] SET  READ_WRITE
GO
ALTER DATABASE [LogInControl] SET RECOVERY FULL
GO
ALTER DATABASE [LogInControl] SET  MULTI_USER
GO
ALTER DATABASE [LogInControl] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [LogInControl] SET DB_CHAINING OFF
GO
USE [LogInControl]

/****** Object:  Table [dbo].[tblUsers]    Script Date: 07/08/2015 12:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Mobile] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Gender] [int] NULL,
	[Address] [varchar](500) NULL,
	[CountryId] [int] NULL,
	[City] [varchar](100) NULL,
	[ZipCode] [varchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsEmailVerified] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserLogins]    Script Date: 07/08/2015 12:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserLogins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[SecurityQuestion] [int] NULL,
	[Answer] [varchar](50) NULL,
	[PasswordWrongAttempts] [int] NULL,
	[LastPasswordWrong] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[RoleId] [int] NULL,
	[IsActive] [bit] NULL,
	[UserId] [int] NULL,
	[AccountLocked] [bit] NULL,
 CONSTRAINT [PK_tblUserLogins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSecurityQuestion]    Script Date: 07/08/2015 12:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSecurityQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionName] [varchar](100) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblSecurityQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRoles]    Script Date: 07/08/2015 12:39:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLoginLogoutHistory]    Script Date: 07/08/2015 12:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLoginLogoutHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedIp] [nvarchar](50) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedIp] [nvarchar](50) NULL,
	[UserName] [varchar](100) NULL,
 CONSTRAINT [PK_tblLoginLogoutHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLoginAttempts]    Script Date: 07/08/2015 12:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLoginAttempts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginAttempts] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedIp] [varchar](100) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedIp] [varchar](100) NULL,
 CONSTRAINT [PK_tblLoginAttempts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEmailLog]    Script Date: 07/08/2015 12:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEmailLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](100) NULL,
	[Body] [varchar](max) NULL,
	[To] [varchar](100) NULL,
	[From] [varchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblEmailLog_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCountry]    Script Date: 07/08/2015 12:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCountry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblCountry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuditLog]    Script Date: 07/08/2015 12:39:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuditLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[GudId] [nvarchar](500) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[PageName] [varchar](100) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblAuditLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_AddPageMappingDetails]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_AddPageMappingDetails]   
@UserId INT,  
@GudId varchar(500),  
@CreatedBy INT,  
@CreatedOn DATETIME,  
@UpdatedBy INT,  
@UpdatedOn DATETIME,  
@PageName varchar(100),  
@IsActive BIT  
AS  
BEGIN  

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 INSERT INTO [dbo].[tblPageMappings]
           ([UserId]
           ,[GudId]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[UpdatedBy]
           ,[UpdatedOn]
           ,[PageName]
           ,[IsActive])
     VALUES
           (@UserId
           ,@GudId
           ,@CreatedBy
           ,GETDATE()
           ,@UpdatedBy
           ,GETDATE()
           ,@PageName
           ,@IsActive)
           SELECT SCOPE_IDENTITY()  
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AddEmailLog]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_AddEmailLog]   
@Subject varchar(100),  
@Body varchar(500),  
@To varchar(100),  
@From varchar(100),  
@CreatedBy INT,  
@CreatedOn Datetime
AS  
BEGIN  
 DECLARE @UserId INT  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 INSERT INTO [dbo].[tblEmailLog]
           ([Subject]
           ,[Body]
           ,[To]
           ,[From]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@Subject
           ,@Body
           ,@To
           ,@From
           ,@CreatedBy
           ,GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AddAuditLog]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:  <Author,,Name>    
-- Create date: <Create Date,,>    
-- Description: <Description,,>    
-- =============================================    
CREATE PROCEDURE [dbo].[usp_AddAuditLog]     
@UserId INT,    
@GudId varchar(500),    
@CreatedBy INT,    
@CreatedOn DATETIME,    
@UpdatedBy INT,    
@UpdatedOn DATETIME,    
@PageName varchar(100),    
@IsActive BIT    
AS    
BEGIN    
  
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
 INSERT INTO [dbo].[tblAuditLog]  
           ([UserId]  
           ,[GudId]  
           ,[CreatedBy]  
           ,[CreatedOn]  
           ,[UpdatedBy]  
           ,[UpdatedOn]  
           ,[PageName]  
           ,[IsActive])  
     VALUES  
           (@UserId  
           ,@GudId  
           ,@CreatedBy  
           ,GETDATE()  
           ,@UpdatedBy  
           ,GETDATE()  
           ,@PageName  
           ,@IsActive)  
           SELECT SCOPE_IDENTITY()    
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateUserStatus]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_UpdateUserStatus]   
@GudId varchar(500),
@UserId INT
AS  
BEGIN  

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 UPDATE tblAuditLog
 SET IsActive=0
 WHERE GudId=@GudId
 
 UPDATE tblUsers
 SET IsEmailVerified= 1 WHERE Id=@UserId 
 
  
 UPDATE tblUserLogins
 SET IsActive= 1 WHERE UserId=@UserId 
 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateUserPassword]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateUserPassword]	
@Password varchar(100),
@UserId INT,
@UpdatedBy INT,
@UpdatedOn DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE tblUserLogins
	SET [Password]=@Password,
	[UpdatedBy]=@UpdatedBy,
	UpdatedOn=GETDATE()
	WHERE UserId=@UserId

END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePasswordWorngAttemptDetails]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePasswordWorngAttemptDetails]	
@AccountLocked BIT,
@PasswordWrongAttempts INT,
@UpdatedBy INT,
@UpdatedOn DATETIME,
@LastPasswordWrong DATETIME,
@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE tblUserLogins
	SET PasswordWrongAttempts=@PasswordWrongAttempts,
	LastPasswordWrong=@LastPasswordWrong,
	AccountLocked=@AccountLocked,
	UpdatedBy=@UpdatedBy,
	UpdatedOn=@UpdatedOn
	WHERE UserId=@UserId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePassword]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdatePassword]	
@Password varchar(100),
@UserId INT,
@UpdatedBy INT,
@GudId varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE tblUserLogins
	SET [Password]=@Password,
	[UpdatedBy]=@UpdatedBy,
	UpdatedOn=GETDATE()
	WHERE UserId=@UserId
	
 UPDATE tblAuditLog
 SET IsActive=0
 WHERE GudId=@GudId
	

END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateLoginAttempts]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_UpdateLoginAttempts]   
@LoginAttempts INT,  
@UpdatedBy INT,  
@UpdatedOn DATETIME,  
@UpdatedIp varchar(100)
AS  
BEGIN  

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 DECLARE @Id INT = 0
 SET @Id=  (SELECT COUNT(1) FROM [tblLoginAttempts])
 
 IF @Id = 0
 BEGIN
	INSERT INTO [dbo].[tblLoginAttempts]
           ([LoginAttempts]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[CreatedIp]
          )
     VALUES
           (@LoginAttempts
           ,@UpdatedBy
           ,GETDATE()
           ,@UpdatedIp
          )
 END
 ELSE
 BEGIN
	UPDATE [tblLoginAttempts]
	SET [LoginAttempts]=@LoginAttempts,
	 [UpdatedBy]=@UpdatedBy
	,[UpdatedOn]=GETDATE()
	,[UpdatedIp]=@UpdatedIp
	
 END
 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LogLogoutTime]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_LogLogoutTime]	
@UserId INT,
@LogoutTime DATETIME,
@UpdatedBy INT,
@UpdatedOn DATETIME,
@UpdatedIp NVARCHAR(50),
@UserName VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ID INT =0
	IF @UserId = 0
	BEGIN
		SET @ID = (SELECT TOP 1 ID FROM [tblLoginLogoutHistory] WHERE UserName =@UserName ORDER BY [CreatedOn] DESC  )
	END
	ELSE
	BEGIN
		SET @ID = (SELECT TOP 1 ID FROM [tblLoginLogoutHistory] WHERE UserId =@UserId ORDER BY [CreatedOn] DESC  )
	
	END
	
	UPDATE [tblLoginLogoutHistory]
	SET LogoutTime = GETDATE(),
	UpdatedBy=@UpdatedBy,
	UpdatedOn=  GETDATE(),
	UpdatedIp= @UpdatedIp
	WHERE Id= @ID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LogLoginTime]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_LogLoginTime]	
@UserId INT,
@LoginTime DATETIME,
@CreatedBy INT,
@CreatedOn DATETIME,
@CreatedIp NVARCHAR(50),
@UserName VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO [dbo].[tblLoginLogoutHistory]
           ([UserId]
           ,[LoginTime]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[CreatedIp]
           ,UserName
           )
     VALUES
           (@UserId
           ,GETDATE()
           ,@CreatedBy
           ,GETDATE()
           ,@CreatedIp
           ,@UserName
          )
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LockUserDetails]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_LockUserDetails]	
@AccountLocked BIT,
@PasswordWrongAttempts INT,
@UpdatedBy INT,
@UpdatedOn DATETIME,
@LastPasswordWrong DATETIME,
@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE tblUserLogins
	SET PasswordWrongAttempts=@PasswordWrongAttempts,
	LastPasswordWrong=GETDATE(),
	AccountLocked=1,
	UpdatedBy=@UpdatedBy,
	UpdatedOn=@UpdatedOn
	WHERE UserId=@UserId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetailsByUserName]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetailsByUserName]	
@UserName varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [tblUsers].[Id]
      ,[FirstName]
      ,[LastName]
      ,[Mobile]
      ,[Email]
      ,[Gender]
      ,[Address]
      ,[CountryId]
      ,[City]
      ,[ZipCode]
      ,[tblUsers].[CreatedBy]
      ,[tblUsers].[CreatedOn]
      ,[tblUsers].[UpdatedBy]
      ,[tblUsers].[UpdatedOn]
      ,[IsEmailVerified]
      ,UserName
      ,[Password]
      ,SecurityQuestion
      ,Answer
      ,PasswordWrongAttempts
      ,LastPasswordWrong
      ,RoleId
      ,IsActive
      ,UserId
      ,AccountLocked
  FROM [dbo].[tblUsers]
  INNER JOIN  tblUserLogins ON tblUserLogins.UserId= [tblUsers].ID
  WHERE tblUserLogins.UserName= @UserName

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetailsById]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetailsById]	
@userId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [tblUsers].[Id]
      ,[FirstName]
      ,[LastName]
      ,[Mobile]
      ,[Email]
      ,[Gender]
      ,[Address]
      ,[CountryId]
      ,[City]
      ,[ZipCode]
      ,[tblUsers].[CreatedBy]
      ,[tblUsers].[CreatedOn]
      ,[tblUsers].[UpdatedBy]
      ,[tblUsers].[UpdatedOn]
      ,[IsEmailVerified]
      ,UserName
      ,[Password]
      ,SecurityQuestion
      ,Answer
      ,PasswordWrongAttempts
      ,LastPasswordWrong
      ,RoleId
      ,IsActive
      ,UserId
      ,AccountLocked
  FROM [dbo].[tblUsers]
  INNER JOIN  tblUserLogins ON tblUserLogins.UserId= [tblUsers].ID
  WHERE [tblUsers].[Id]= @userId

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetailsByEmailId]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetailsByEmailId]	
@email varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [tblUsers].[Id]
      ,[FirstName]
      ,[LastName]
      ,[Mobile]
      ,[Email]
      ,[Gender]
      ,[Address]
      ,[CountryId]
      ,[City]
      ,[ZipCode]
      ,[tblUsers].[CreatedBy]
      ,[tblUsers].[CreatedOn]
      ,[tblUsers].[UpdatedBy]
      ,[tblUsers].[UpdatedOn]
      ,[IsEmailVerified]
      ,UserName
      ,[Password]
      ,SecurityQuestion
      ,Answer
      ,PasswordWrongAttempts
      ,LastPasswordWrong
      ,RoleId
      ,IsActive
      ,UserId
      ,AccountLocked
  FROM [dbo].[tblUsers]
  INNER JOIN  tblUserLogins ON tblUserLogins.UserId= [tblUsers].ID
  WHERE [tblUsers].[Email]= @email

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserDetails]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserDetails]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [tblUsers].[Id]
      ,[FirstName]
      ,[LastName]
      ,[Mobile]
      ,[Email]
      ,[Gender]
      ,[Address]
      ,[CountryId]
      ,[City]
      ,[ZipCode]
      ,[tblUsers].[CreatedBy]
      ,[tblUsers].[CreatedOn]
      ,[tblUsers].[UpdatedBy]
      ,[tblUsers].[UpdatedOn]
      ,[IsEmailVerified]
      ,UserName
      ,[Password]
      ,SecurityQuestion
      ,Answer
      ,PasswordWrongAttempts
      ,LastPasswordWrong
      ,RoleId
      ,IsActive
      ,UserId
      ,AccountLocked
  FROM [dbo].[tblUsers]
  INNER JOIN  tblUserLogins ON tblUserLogins.UserId= [tblUsers].ID  
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSearchUserDetailsByUserName]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetSearchUserDetailsByUserName]
@SearchUser varchar(100)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [tblUsers].[Id]
      ,[FirstName]
      ,[LastName]
      ,[Mobile]
      ,[Email]
      ,[Gender]
      ,[Address]
      ,[CountryId]
      ,[City]
      ,[ZipCode]
      ,[tblUsers].[CreatedBy]
      ,[tblUsers].[CreatedOn]
      ,[tblUsers].[UpdatedBy]
      ,[tblUsers].[UpdatedOn]
      ,[IsEmailVerified]
      ,UserName
      ,[Password]
      ,SecurityQuestion
      ,Answer
      ,PasswordWrongAttempts
      ,LastPasswordWrong
      ,RoleId
      ,IsActive
      ,UserId
      ,AccountLocked
  FROM [dbo].[tblUsers]
  INNER JOIN  tblUserLogins ON tblUserLogins.UserId= [tblUsers].ID  
  WHERE UserName  like '%'+@SearchUser+'%'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLoginHistory]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetLoginHistory]	
@SearchUser varchar(100)= NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT *
  FROM [dbo].tblLoginLogoutHistory 
  WHERE (@SearchUser IS NULL OR tblLoginLogoutHistory.UserName like '%'+@SearchUser+'%' )
  ORDER BY ID DESC

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetLoginAttempts]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_GetLoginAttempts]   

AS  
BEGIN  

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 SELECT * FROM [tblLoginAttempts]
 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAuditLogDetailsByGuid]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_GetAuditLogDetailsByGuid]   
@GudId varchar(500)
AS  
BEGIN  

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 SELECT [Id]
      ,[UserId]
      ,[GudId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[PageName]
      ,[IsActive]
  FROM [dbo].[tblAuditLog]
  WHERE [GudId]=@GudId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllSecurityQuestion]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllSecurityQuestion]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [Id]
      ,[QuestionName]
      ,[UpdatedBy]
      ,[UpdatedOn]
  FROM [dbo].[tblSecurityQuestion]
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllRoles]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllRoles]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [Id]
      ,[RoleName]
      ,[IsActive]
  FROM [dbo].[tblRoles]

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllCountry]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllCountry]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [Id]
      ,[CountryName]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
  FROM [dbo].[tblCountry]

END
GO
/****** Object:  StoredProcedure [dbo].[usp_ClearLoginAttempts]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_ClearLoginAttempts]	
@UserId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE tblUserLogins
	SET PasswordWrongAttempts=0,
	LastPasswordWrong=NULL
	WHERE UserId=@UserId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_AddUserDetails]    Script Date: 07/08/2015 12:39:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[usp_AddUserDetails]   
@FirstName varchar(100),  
@LastName varchar(100),  
@Mobile varchar(100),  
@Email varchar(100),  
@Gender INT,  
@Address varchar(500),  
@CountryId INT,  
@City varchar(100),  
@ZipCode varchar(100),  
@CreatedBy INT,  
@CreatedOn DATETIME,  
@UpdatedBy INT,  
@UpdatedOn DATETIME,  
@IsEmailVerified Bit,  
@UserName varchar(50),  
@Password varchar(50),  
@SecurityQuestion INT,  
@Answer varchar(50),  
@RoleId INT,  
@IsActive BIT  
AS  
BEGIN  
 DECLARE @UserId INT  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 INSERT INTO [dbo].[tblUsers]  
           ([FirstName]  
           ,[LastName]  
           ,[Mobile]  
           ,[Email]  
           ,[Gender]  
           ,[Address]  
           ,[CountryId]  
           ,[City]  
           ,[ZipCode]  
           ,[CreatedBy]  
           ,[CreatedOn]  
           ,[UpdatedBy]  
           ,[UpdatedOn]  
           ,[IsEmailVerified])  
     VALUES  
           (@FirstName  
           ,@LastName  
           ,@Mobile  
           ,@Email  
           ,@Gender  
           ,@Address  
           ,@CountryId  
           ,@City  
           ,@ZipCode  
           ,@CreatedBy  
           ,GETDATE()  
           ,@UpdatedBy  
           ,GETDATE()  
           ,0)  
             
    SET @UserId= (SELECT SCOPE_IDENTITY())  
      
    UPDATE [tblUsers]  
    SET [CreatedBy]= @UserId,@UpdatedBy= @UserId  
    WHERE ID=@UserId      
      
    INSERT INTO [dbo].[tblUserLogins]  
           ([UserName]  
           ,[Password]  
           ,[SecurityQuestion]  
           ,[Answer]  
           ,[PasswordWrongAttempts]  
           ,[LastPasswordWrong]  
           ,[CreatedBy]  
           ,[CreatedOn]  
           ,[UpdatedBy]  
           ,[UpdatedOn]  
           ,[RoleId]  
           ,[IsActive]  
           ,[UserId])  
     VALUES  
           (@UserName  
           ,@Password  
           ,@SecurityQuestion  
           ,@Answer  
           ,0  
           ,Null  
           ,@UserId  
           ,GETDATE()  
           ,@UserId  
           ,GETDATE()  
           ,@RoleId  
           ,0  
           ,@UserId)  
     
     SELECT @UserId
END
GO
/****** Object:  Default [DF_tblUsers_IsEmailVerified]    Script Date: 07/08/2015 12:39:36 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_IsEmailVerified]  DEFAULT ((0)) FOR [IsEmailVerified]
GO
/****** Object:  Default [DF_tblUserLogins_IsActive]    Script Date: 07/08/2015 12:39:36 ******/
ALTER TABLE [dbo].[tblUserLogins] ADD  CONSTRAINT [DF_tblUserLogins_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
