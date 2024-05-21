USE [mchn]

CREATE TABLE [sch_mchn].[T_FAMILY]
(	
	[FAMILY_ID] UNIQUEIDENTIFIER default NEWID(),
	[NAME] [nvarchar](200) NULL,
	[CODE] [nvarchar](5) NOT NULL,
	[SUB_CODE] [nvarchar](5),
	[SUB_NAME] [nvarchar](200) NULL,
	CONSTRAINT [PK_FAMILYID] PRIMARY KEY ([FAMILY_ID])  
);


CREATE TABLE [sch_mchn].[T_MACHINE_SPECIFICATION](
	[MACHINE_ID] UNIQUEIDENTIFIER default NEWID(),
	[CODE] [nvarchar](5) NOT NULL,
	[NAME] [nvarchar](100) NOT NULL,
	[LABEL] [nvarchar](100) NULL,
	[DESCRIPTION] [nvarchar](2000) NULL,
	[KEYWORDS] [nvarchar] (300) NULL,
	[SCORE] [nvarchar](10) NULL,
	[START_DATETIME_SUBSCRIPTION_PERIOD] [datetime2](7) NULL,
	[END_DATETIME_SUBSCRIPTION_PERIOD] [datetime2](7) NULL,
	[IS_DELEGATED] [bit] NULL,
	[IS_OUT_OF_MACHINE_INSURANCE] [bit] NULL,
	[IS_UNREFERENCED] [bit] NULL,
	[IS_EXCLUDED] [bit] NULL,
	[AGE_LIMIT_ALLOWED] [int] NULL,
	[ALL_PLACE_CORVERED] [nvarchar](15) NULL,
	[EXTENDED_FLEET_COEVERAGE_ALLOWED_PERCENTAGE] [int] NULL,
	[PRODUCT] [nvarchar](20) NULL,
	[MACHINE_RATE] [decimal](18, 0) NULL,	
	CONSTRAINT [PK_MACHINE_ID] PRIMARY KEY ([MACHINE_ID])  
);

CREATE TABLE [sch_mchn].[T_PRICING_RATE]
(
	[PRICING_RATE_ID] UNIQUEIDENTIFIER default NEWID(),
	[CODE] NVARCHAR(3) NULL,
	[RATE] decimal NULL,
	[FK_MACHINE_ID] UNIQUEIDENTIFIER NOT NULL,	
	CONSTRAINT [PK_PRICING_RATE_ID] PRIMARY KEY ([PRICING_RATE_ID]),  
    CONSTRAINT [C_T_PRICING_RATE_MACHINE_SPECIFICATION_FK] FOREIGN KEY ([FK_MACHINE_ID]) REFERENCES [sch_mchn].[T_MACHINE_SPECIFICATION] ([MACHINE_ID]) ON DELETE NO ACTION
);



CREATE TABLE [sch_mchn].[T_FAMILYT_MACHINE_SPECIFICATION] (
    [T_FAMILYFAMILY_ID] UNIQUEIDENTIFIER NOT NULL,
    [T_MACHINE_SPECIFICATIONMACHINE_ID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_T_FAMILYT_MACHINE_SPECIFICATION] PRIMARY KEY ([T_FAMILYFAMILY_ID],[T_MACHINE_SPECIFICATIONMACHINE_ID]),    
    CONSTRAINT [FK_T_FAMILYT_MACHINE_SPECIFICATION_T_FAMILY_FAMILY_ID] FOREIGN KEY ([T_FAMILYFAMILY_ID]) REFERENCES [sch_mchn].[T_FAMILY] ([FAMILY_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_T_FAMILYT_MACHINE_SPECIFICATION_T_MACHINE_SPECIFICATION_MACHINE_ID] FOREIGN KEY ([T_MACHINE_SPECIFICATIONMACHINE_ID]) REFERENCES [sch_mchn].[T_MACHINE_SPECIFICATION] ([MACHINE_ID]) ON DELETE CASCADE
);

CREATE TABLE [sch_mchn].[T_EDITION_CLAUSE]
(
	[CLAUSE_ID] UNIQUEIDENTIFIER default NEWID(),
	[CODE] [nvarchar](4) NOT NULL,
	[LABEL] [nvarchar](80) NOT NULL,
	[VOL] bit NULL,
	[CONTENT] [nvarchar](2000) NULL,
	CONSTRAINT [PK_CLAUSE_ID] PRIMARY KEY ([CLAUSE_ID])
);

CREATE TABLE [sch_mchn].[T_MACHINE_EDITION_CLAUSE]
(
	[MACHINE_CLAUSE_ID] UNIQUEIDENTIFIER default NEWID(),
	[CODE] [nvarchar](4) NOT NULL,
	[LABEL] [nvarchar](80) NOT NULL,
	[VOL] bit NULL,
	[CONTENT] [nvarchar](2000) NULL,
	[FK_MACHINE_ID] UNIQUEIDENTIFIER NOT NULL,    
	CONSTRAINT [PK_MACHINE_CLAUSE_ID] PRIMARY KEY ([MACHINE_CLAUSE_ID]),  
    CONSTRAINT [C_T_EDITION_CLAUSE_MACHINE_SPECIFICATION_FK] FOREIGN KEY ([FK_MACHINE_ID]) REFERENCES [sch_mchn].[T_MACHINE_SPECIFICATION] ([MACHINE_ID]) ON DELETE NO ACTION
);
