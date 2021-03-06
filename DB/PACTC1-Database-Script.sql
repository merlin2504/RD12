SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnFeatures]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnFeatures](
	[ID] [int] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[FeatureXML] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FEATURES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnDataDictionary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnDataDictionary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[AppName] [nvarchar](50) NULL,
	[DBName] [nvarchar](50) NULL,
	[ModuleName] [nvarchar](50) NULL,
	[FeatureName] [nvarchar](50) NULL,
	[TableName] [nvarchar](50) NULL,
	[TableDesc] [nvarchar](500) NULL,
	[ColumnName] [nvarchar](50) NULL,
	[ColumnDesc] [nvarchar](500) NULL,
	[ColumnType] [nvarchar](50) NULL,
	[ColumnSize] [nvarchar](50) NULL,
	[ColumnDefault] [nvarchar](50) NULL,
	[IsMandatory] [int] NULL,
	[IsSystem] [int] NULL,
	[IsPrimaryKey] [int] NULL,
	[IsForeignKey] [int] NULL,
	[ForeignTableName] [nvarchar](50) NULL,
 CONSTRAINT [PK_AdmnDataDictionary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLables]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLables](
	[AppID] [int] NOT NULL,
	[FeatureID] [int] NOT NULL,
	[ScreenID] [int] NOT NULL,
	[LableKey] [nvarchar](100) NOT NULL,
	[English] [nvarchar](500) NOT NULL,
	[Arabic] [nvarchar](500) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutLists]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutLists](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LayoutXML] [nvarchar](max) NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[PrimaryKey] [nvarchar](50) NOT NULL,
	[WhereClause] [nvarchar](500) NULL,
 CONSTRAINT [PK_LAYOUTLIST] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnPreferences]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnPreferences](
	[ID] [int] NOT NULL,
	[ParamName] [nvarchar](200) NOT NULL,
	[ParamCaption] [nvarchar](200) NOT NULL,
	[ParamValue] [nvarchar](200) NOT NULL,
	[ControlType] [nvarchar](200) NOT NULL,
	[LookupID] [int] NULL CONSTRAINT [DF_AdmnPreferences_LookupID]  DEFAULT ((-100)),
	[TabOrder] [int] NOT NULL,
 CONSTRAINT [PK_AdmnPreferences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[FeaturesXML] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[GUID] [nvarchar](50) NULL,
 CONSTRAINT [PK_AdmnRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnUsers](
	[ID] [int] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Status] [int] NOT NULL CONSTRAINT [DF_AdmnUsers_Status]  DEFAULT ((0)),
	[Email] [nvarchar](200) NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[PreferredLanguage] [nvarchar](50) NULL,
	[Address1] [nvarchar](500) NULL,
	[Address2] [nvarchar](500) NULL,
	[Address3] [nvarchar](500) NULL,
	[Phone] [nvarchar](200) NULL,
	[Fax] [nvarchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[GUID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnVersion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_VERSION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLookups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLookups](
	[ID] [int] NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_AdmnLookups_ParentID]  DEFAULT ((-1)),
	[FeatureID] [int] NOT NULL,
	[AppID] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DisplayName] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
	[ViewOrder] [int] NOT NULL,
	[VisibilityStatus] [int] NOT NULL CONSTRAINT [DF_AdmnLookups_VisibilityStatus]  DEFAULT ((1)),
 CONSTRAINT [PK_LOOKUPS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutViews]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutViews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AdmnLayoutViews] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutReports]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AdmnLayoutReports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutPrint]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutPrint](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_AdmnLayoutPrint] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutMap]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutMap](
	[ID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[LayoutID] [int] NOT NULL,
	[LayoutType] [int] NOT NULL,
	[AppID] [int] NOT NULL,
	[FeatureID] [int] NULL,
 CONSTRAINT [PK_AdmnLayoutMap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdmnLayoutScreens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdmnLayoutScreens](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FeatureID] [int] NOT NULL,
	[ScreenName] [nvarchar](200) NOT NULL,
	[ScreenXML] [nvarchar](max) NOT NULL,
	[GridXML] [nvarchar](max) NULL,
	[Settings] [nvarchar](max) NULL,
	[IsStatic] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[GUID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LAYOUTSCREEN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpHelpOnProcedure]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[SpHelpOnProcedure]
@objname nvarchar(776) = NULL
as
set nocount on
	select
		''Parameter_name'' = t1.name,
		''Type''=type_name(xusertype),
		''Length''=CASE WHEN type_name(xusertype)=''Text'' THEN 2147483647 ELSE convert(int,length) END,
		''Prec''=CASE WHEN type_name(xusertype)=''uniqueidentifier'' THEN xprec ELSE odbcPrec(t1.xtype,length,xprec) END,
		''Param_order'' = colid,
		''Out_parameter'' = isoutparam
	from syscolumns t1 WITH(NOLOCK),sysobjects t2 WITH(NOLOCK)
	where t1.id = t2.id and t2.name=@objname
return (0)
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccountTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPExecuteQuery]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[SPExecuteQuery]
(@Query nvarchar(max))
as

exec(@Query)

--exec SPExecuteQuery "Select ID,AccountType from AccountTypes order by ID"' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountStatus] [nchar](10) NULL,
 CONSTRAINT [PK_AccountStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Accounts](
	[NodeNo] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Alias] [nvarchar](255) NOT NULL,
	[ParentNo] [int] NULL CONSTRAINT [DF_Mast000_ParentNo]  DEFAULT ((0)),
	[Type1] [int] NOT NULL,
	[Group1] [int] NULL,
	[LevelNo] [int] NULL,
	[NodePosition] [int] NULL,
	[Status] [int] NULL,
	[Address1] [nvarchar](200) NULL,
	[Address2] [nvarchar](200) NULL,
	[Address3] [nvarchar](200) NULL,
	[Phoneno] [nvarchar](50) NULL,
	[Faxno] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Contactperson] [nvarchar](50) NULL,
	[CreditLimit] [float] NULL,
	[DebitLimit] [float] NULL,
	[CreditDays] [int] NULL,
	[DebitDays] [int] NULL,
	[SalesAccount] [int] NULL,
	[PurchaseAccount] [int] NULL,
	[CreatedDate] [float] NULL,
	[CreatedBy] [int] NULL,
	[ModDate] [float] NULL,
	[ModBy] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPGetScreenByFeatureID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SPGetScreenByFeatureID]
 @FeatureID int=NUll 
as

select ScreenXML from dbo.AdmnLayoutScreens  WITH(NOLOCK)
where FeatureID=@FeatureID

--exec SPGetScreenByFeatureID 1000' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPCreateAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SPCreateAccount]
(
		@NodePositionSeqno int=null,
		@Code varchar(200)= NULL,
      	@Name varchar(200)=NULL,  
		@Type int=null,		
--		@status int=null,    
--		@Address1 Varchar(200)=null,
--		@Address2 Varchar(200)=null,
--		@Address3 Varchar(200)=null,
@AliasNm Varchar(100)=null,
--		@ContactPerson Varchar(200)=null,
--		@CreditDays int=null,
--		@CreditLimit float=null,
--		@DebitDays int=null,
--		@DebitLimit float=null,
--		@Email varchar(200)=null,
--		@ParentNode int=null,
--		@PhoneNo varchar(100)=null,
--		@Fax varchar(200)=null,
--		@SAccount int=null,
--		@PAccount int=null,
--		@strCreatedBy int= NULL,		 
--		@ExtraFieldQuery VARCHAR(MAX)=NULL,
--		@TableName varchar(200)=null,
--		@ExtendedPrimaryColumn varchar(300)=null,
--		@key int=null,
--		@PrimaryColumn varchar(max)=null, 
--		@strGUID varchar(200)=null,
		@strNewGUID varchar(50) OUT
)
AS
BEGIN TRANSACTION
Begin Try
--Declaring variables
declare @iDimensionSeqNo int,@NodePositon int,@CURRENTNODEPOSITON INT,@ParentNo int,@IsGroup bit,@Level int
declare @SQL varchar(MAX),@iSeqNo varchar(50),@SQLUPDATE VARCHAR(MAX),@SQLUPDATEEXTENDED VARCHAR(MAX)
DECLARE @QUERY NVARCHAR(MAX)
DECLARE @ExtendedTableName VARCHAR(MAX)
DECLARE @strCode nvarchar(200),@strName nvarchar(200)
DECLARE @tempGroup NVARCHAR(200),@Node nVARCHAR(300)


SET @ParentNo=@NodePositionSeqno
SET @Level=@Level+1
SET @CURRENTNODEPOSITON=@NodePositon+1

Insert into Accounts(Code,Name,Alias,type1)
Values(@Code,@Name,@AliasNm,@Type)

	--Insert Procedure
--	Insert into Accounts(Code,Name,[ParentNo],[LevelNo],NODEPOSITION,status,Group1,Alias,Address1,Address2,Address3,Phoneno,Faxno,Email,Contactperson,CreditLimit,DebitLimit,CreditDays,DebitDays,SalesAccount,PurchaseAccount,[CreatedBy],type1,CreatedDate,ModDate) 
--	Values(@Code,@Name,@ParentNo,@Level, @CURRENTNODEPOSITON, @status,0,@AliasNm,@Address1,@Address2,@Address3,@PhoneNo,@Fax,@Email,@ContactPerson,
--		   @CreditLimit,@DebitLimit,@CreditDays,@DebitDays,@SAccount,@PAccount,@strCreatedBy, @Type, CONVERT(FLOAT,GETDATE()),CONVERT(FLOAT,GETDATE()))

	 set @iSeqNo=SCOPE_IDENTITY()

	--IF @ExtraFieldQuery IS NOT NULL AND @ExtraFieldQuery<>''''
	--SET @SQL=''UPDATE ''+@TableName+'' SET ''+@ExtraFieldQuery+'' WHERE ''+@PrimaryColumn+''=''+convert(varchar,@iSeqNo)

	--EXEC(@SQL)IF @@ERROR<>0 BEGIN ROLLBACK TRANSACTION RETURN -500 END


COMMIT TRANSACTION

RETURN @iSeqNo

End Try
Begin Catch
	ROLLBACK transaction
	RETURN -12345
End Catch




' 
END
