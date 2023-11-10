if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_devfwk_action_customdata_devfwk_action_config]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[devfwk_action_customdata] DROP CONSTRAINT FK_devfwk_action_customdata_devfwk_action_config
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_devfwk_action_role_devfwk_action_config]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[devfwk_action_role] DROP CONSTRAINT FK_devfwk_action_role_devfwk_action_config
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_action_config_ts]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[devfwk_action_config_ts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_message_ts]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[devfwk_message_ts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_parameter_ts]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[devfwk_parameter_ts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_action_customdata]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_action_customdata]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_action_integrationaction]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_action_integrationaction]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_action_role]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_action_role]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_event]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_event]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_message]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_message]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_create_transaction]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_create_transaction]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_delete_action]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_delete_action]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_delete_message]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_delete_message]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_delete_parameter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_delete_parameter]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_action_customdata]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_action_customdata]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_action_integrationaction]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_action_integrationaction]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_action_role]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_action_role]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_actions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_actions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_config_timestamps]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_config_timestamps]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_datetime]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_datetime]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_events]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_events]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_integration_actions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_integration_actions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_messages]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_messages]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_get_parameters]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_get_parameters]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_update_action]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_update_action]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_update_config_timestamps]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_update_config_timestamps]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_update_message]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_update_message]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_update_parameter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[devfwk_update_parameter]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_action_config]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_action_config]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_action_customdata]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_action_customdata]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_action_integrationaction]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_action_integrationaction]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_action_role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_action_role]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_config_timestamps]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_config_timestamps]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_event]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_event]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_message]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_message]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_parameter]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_parameter]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[devfwk_transaction]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[devfwk_transaction]
GO

CREATE TABLE [dbo].[devfwk_action_config] (
	[ac_code] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_description] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_available] [bit] NOT NULL ,
	[ac_handler] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_request] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_response] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_context] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_tx_mode] [int] NOT NULL ,
	[ac_log_header] [bit] NOT NULL ,
	[ac_log_request] [bit] NOT NULL ,
	[ac_log_response] [bit] NOT NULL ,
	[ac_compensate] [bit] NOT NULL ,
	[ac_async_proc] [bit] NOT NULL ,
	[ac_async_proc_xpath] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_request_schema] [varchar] (4096) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_response_schema] [varchar] (4096) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_timeout] [int] NOT NULL ,
	[ac_debug] [bit] NULL ,
	[ac_type] [varchar] (32) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_log_queue] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_application] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ac_reverse] [varchar] (254) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_action_customdata] (
	[ac_code] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_key] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_value] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_action_integrationaction] (
	[ac_code] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[integrationaction_code] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_action_role] (
	[ac_code] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ac_role] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_config_timestamps] (
	[entity] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[last_updated] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_event] (
	[ev_type] [varchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ev_message] [varchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ev_data] [varchar] (4096) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ev_user] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ev_computer] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ev_process] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ev_date] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_message] (
	[mg_id] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[mg_text] [varchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_parameter] (
	[pm_environment] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[pm_module] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[pm_sub_module] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[pm_key] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[pm_data_type] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[pm_str_value] [varchar] (2048) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[pm_dat_value] [datetime] NULL ,
	[pm_num_value] [numeric](18, 2) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[devfwk_transaction] (
	[tn_id] [uniqueidentifier] NOT NULL ,
	[ac_code] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[us_code] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tn_date] [datetime] NOT NULL ,
	[tn_error] [varchar] (8000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tn_step] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[tn_checkpoint] [varchar] (8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[devfwk_action_config] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_action_config] PRIMARY KEY  CLUSTERED 
	(
		[ac_code]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_action_customdata] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_action_custom_data] PRIMARY KEY  CLUSTERED 
	(
		[ac_code],
		[ac_key]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_action_integrationaction] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_action_integrationaction}] PRIMARY KEY  CLUSTERED 
	(
		[ac_code],
		[integrationaction_code]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_action_role] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_action_role] PRIMARY KEY  CLUSTERED 
	(
		[ac_code],
		[ac_role]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_config_timestamps] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_config_timestamps] PRIMARY KEY  CLUSTERED 
	(
		[entity]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_message] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_message] PRIMARY KEY  CLUSTERED 
	(
		[mg_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[devfwk_parameter] WITH NOCHECK ADD 
	CONSTRAINT [PK_devfwk_parameter] PRIMARY KEY  CLUSTERED 
	(
		[pm_key],
		[pm_environment],
		[pm_module],
		[pm_sub_module]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_devfwk_event] ON [dbo].[devfwk_event]([ev_date] DESC ) ON [PRIMARY]
GO

ALTER TABLE [dbo].[devfwk_parameter] ADD 
	CONSTRAINT [IX_devfwk_parameter] UNIQUE  NONCLUSTERED 
	(
		[pm_key],
		[pm_environment],
		[pm_module],
		[pm_sub_module]
	)  ON [PRIMARY] ,
	CONSTRAINT [CK_devfwk_data_type] CHECK (([pm_data_type] = 'T' or [pm_data_type] = 'N' or [pm_data_type] = 'D' or [pm_data_type] = 'X') and ([pm_data_type] = 'T' and [pm_str_value] is not null or [pm_data_type] = 'N' and [pm_num_value] is not null or [pm_data_type] = 'D' and [pm_dat_value] is not null or [pm_data_type] = 'X' and [pm_str_value] is not null))
GO

ALTER TABLE [dbo].[devfwk_action_customdata] ADD 
	CONSTRAINT [FK_devfwk_action_customdata_devfwk_action_config] FOREIGN KEY 
	(
		[ac_code]
	) REFERENCES [dbo].[devfwk_action_config] (
		[ac_code]
	)
GO

ALTER TABLE [dbo].[devfwk_action_role] ADD 
	CONSTRAINT [FK_devfwk_action_role_devfwk_action_config] FOREIGN KEY 
	(
		[ac_code]
	) REFERENCES [dbo].[devfwk_action_config] (
		[ac_code]
	)
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.devfwk_create_action_customdata
(
@ac_code VARCHAR(200), 
@ac_key VARCHAR(50),
@ac_value VARCHAR(255)
)
AS
INSERT INTO devfwk_action_customdata ( ac_code, ac_key, ac_value )
VALUES ( @ac_code, @ac_key, @ac_value )

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.devfwk_create_action_integrationaction
(
@ac_code VARCHAR(200), 
@integrationaction_code VARCHAR(200)
)
AS
INSERT INTO devfwk_action_integrationaction ( ac_code, integrationaction_code )
VALUES ( @ac_code, @integrationaction_code )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE dbo.devfwk_create_action_role 
(
@ac_code VARCHAR(200), 
@ac_role VARCHAR(30)
)
AS
INSERT INTO devfwk_action_role ( ac_code, ac_role )
VALUES ( @ac_code, @ac_role )

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[devfwk_create_event] 
( @type VARCHAR(16), 
@message VARCHAR(1024), 
@data VARCHAR(4096), 
@user VARCHAR(64), 
@computer VARCHAR(64), 
@process VARCHAR(64), 
@date datetime )
AS
INSERT INTO devfwk_event ( ev_type, ev_message, ev_data, ev_user, ev_computer, ev_process, ev_date ) 
VALUES ( @type, @message, @data, @user, @computer, @process, @date )




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





CREATE PROCEDURE dbo.devfwk_create_message
(
@mg_id VARCHAR(50),
@mg_text VARCHAR(1024)
)
AS
IF( SELECT count(*) FROM devfwk_message WHERE ( mg_id = @mg_id ) ) = 1
BEGIN
	UPDATE devfwk_message SET mg_text = @mg_text
	WHERE mg_id = @mg_id
END
ELSE
BEGIN
	INSERT INTO devfwk_message ( mg_id, mg_text ) VALUES ( @mg_id, @mg_text ) 
END








GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[devfwk_create_transaction]
(
@tn_id uniqueidentifier,
@ac_code VARCHAR(200),
@us_code VARCHAR(30),
@tn_step VARCHAR(10),
@tn_date DATETIME,
@tn_error VARCHAR(8000),
@tn_checkpoint VARCHAR(8000)
) AS

INSERT INTO devfwk_transaction ( tn_id,  ac_code,  us_code,  tn_step,
tn_date,  tn_error,  tn_checkpoint )
VALUES ( @tn_id, @ac_code, @us_code, @tn_step, @tn_date, @tn_error,
@tn_checkpoint )

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].[devfwk_delete_action] 
( @ac_code VARCHAR(200) )
AS
DELETE FROM devfwk_action_integrationaction WHERE @ac_code = ac_code
DELETE FROM devfwk_action_customdata WHERE @ac_code = ac_code
DELETE FROM devfwk_action_role WHERE @ac_code = ac_code
DELETE FROM devfwk_action_config WHERE @ac_code = ac_code
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[devfwk_delete_message] 
@mg_id VARCHAR(50)
AS
DELETE FROM devfwk_message WHERE @mg_id = mg_id




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[devfwk_delete_parameter] 
(
@pm_key VARCHAR(50), 
@pm_environment VARCHAR(50), 
@pm_module VARCHAR(50), 
@pm_sub_module VARCHAR(50)
)
AS
DELETE devfwk_parameter 
WHERE
( pm_environment = @pm_environment  ) AND 
(  pm_module = @pm_module ) AND 
( pm_sub_module =  @pm_sub_module ) AND 
@pm_key = pm_key




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE devfwk_get_action_customdata
AS

SELECT
  ac_code,
  ac_key,
  ac_value
FROM
  devfwk_action_customdata


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE devfwk_get_action_integrationaction
AS

SELECT
  ac_code,
  integrationaction_code
FROM
  devfwk_action_integrationaction
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE devfwk_get_action_role
AS

SELECT
  ac_code,
  ac_role
FROM
  devfwk_action_role

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE [dbo].[devfwk_get_actions] 
AS


SELECT 
  a.*
FROM 
  devfwk_action_config a
WHERE
   ac_type  is null OR (
   lower( ac_type ) != 'integrationactionasync'  AND
   lower( ac_type ) != 'integrationaction' )
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.devfwk_get_config_timestamps 
@WhenRun datetime = null output
AS

set @WhenRun = GETDATE()
select entity,last_updated from devfwk_config_timestamps

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE devfwk_get_datetime 
@datetime DateTime = null output

AS

select @datetime = GETDATE()

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE dbo.devfwk_get_events AS
SELECT * FROM devfwk_event ORDER BY ev_date DESC




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].[devfwk_get_integration_actions] 
AS

SELECT 
  a.*
FROM 
  devfwk_action_config a
WHERE
   lower( ac_type ) = 'integrationactionasync'  OR 
   lower( ac_type ) = 'integrationaction'
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[devfwk_get_messages] AS
SELECT * FROM devfwk_message




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO





CREATE PROCEDURE [dbo].[devfwk_get_parameters] AS
SELECT * FROM devfwk_parameter




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[devfwk_update_action] 
(
@ac_code VARCHAR(200),
@ac_code_new VARCHAR(50),
@ac_description VARCHAR(254),
@ac_available bit,
@ac_handler VARCHAR(254),
@ac_request VARCHAR(254),
@ac_response VARCHAR(254),
@ac_tx_mode int,
@ac_context VARCHAR(254),
@ac_log_header bit,
@ac_log_request bit,
@ac_log_response bit,
@ac_compensate bit,
@ac_async_proc bit,
@ac_async_proc_xpath VARCHAR(254),
@ac_request_schema VARCHAR(4096),
@ac_response_schema VARCHAR(4096),
@ac_timeout int,
@ac_debug bit,
@ac_type VARCHAR(32),
@ac_log_queue VARCHAR(254),
@clear_roles bit,
@ac_application VARCHAR(254),
@ac_reverse VARCHAR(254),
@clear_customdata bit,
@clear_integrationactions bit
)
AS
 
IF( SELECT count(*) FROM devfwk_action_config WHERE ac_code = @ac_code ) = 1
BEGIN

 IF( @clear_roles = 1 )  DELETE FROM devfwk_action_role WHERE ac_code = @ac_code
 IF( @clear_customdata = 1 )  DELETE FROM devfwk_action_customdata WHERE ac_code = @ac_code
 IF( @clear_integrationactions = 1 ) DELETE FROM devfwk_action_integrationaction WHERE ac_code = @ac_code

 UPDATE devfwk_action_config 
  SET 
   ac_code = @ac_code_new,
   ac_description = @ac_description, 
   ac_available = @ac_available, 
   ac_handler = @ac_handler, 
   ac_request = @ac_request, 
   ac_response = @ac_response, 
   ac_tx_mode = @ac_tx_mode, 
   ac_context = @ac_context, 
   ac_log_header = @ac_log_header, 
   ac_log_request = @ac_log_request, 
   ac_log_response = @ac_log_response, 
   ac_compensate = @ac_compensate, 
   ac_async_proc = @ac_async_proc, 
   ac_async_proc_xpath = @ac_async_proc_xpath,
   ac_request_schema = @ac_request_schema,
   ac_response_schema = @ac_response_schema,
   ac_timeout = @ac_timeout,
   ac_debug = @ac_debug,
   ac_type = @ac_type,
   ac_log_queue=@ac_log_queue,
   ac_application=@ac_application,
   ac_reverse=@ac_reverse
WHERE
  ( ac_code = @ac_code )
END
ELSE
BEGIN
 INSERT INTO devfwk_action_config ( ac_code, ac_description, ac_available, ac_handler, ac_request, ac_response, ac_tx_mode, ac_context, ac_log_header, ac_log_request, ac_log_response, ac_compensate, ac_async_proc, ac_async_proc_xpath, ac_request_schema, ac_response_schema, ac_timeout, ac_debug, ac_type, ac_log_queue, ac_application, ac_reverse) 
 VALUES ( @ac_code, @ac_description, @ac_available, @ac_handler, @ac_request, @ac_response, @ac_tx_mode, @ac_context, @ac_log_header, @ac_log_request, @ac_log_response, @ac_compensate, @ac_async_proc, @ac_async_proc_xpath, @ac_request_schema, @ac_response_schema, @ac_timeout, @ac_debug, @ac_type, @ac_log_queue, @ac_application, @ac_reverse)
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.devfwk_update_config_timestamps
	@entity varchar(50)
AS
IF( SELECT count(*) FROM devfwk_config_timestamps WHERE entity = @entity ) = 1
BEGIN
	update devfwk_config_timestamps
	set last_updated=GETDATE()
	where entity=@entity
END
ELSE
BEGIN
	insert into devfwk_config_timestamps
	values ( @entity, GETDATE() )
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO




CREATE PROCEDURE [dbo].[devfwk_update_message] 
(
@mg_id VARCHAR(50),
@mg_text VARCHAR(1024)
)
AS
IF (SELECT count(*) FROM devfwk_message WHERE ( mg_id = @mg_id ) ) = 1
BEGIN
	UPDATE devfwk_message SET mg_text = @mg_text
	WHERE mg_id = @mg_id
END
ELSE
BEGIN
	INSERT INTO devfwk_message ( mg_id, mg_text )
	VALUES ( @mg_id, @mg_text )
END




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[devfwk_update_parameter] 
(
@pm_key VARCHAR(50), 
@pm_environment VARCHAR(50), 
@pm_module VARCHAR(50), 
@pm_sub_module VARCHAR(50), 
@pm_data_type CHAR(1), 
@pm_str_value VARCHAR(2048),
@pm_dat_value DATETIME,
@pm_num_value NUMERIC(9,2)
)
AS
SET ANSI_NULLS OFF

DELETE devfwk_parameter WHERE  pm_environment = @pm_environment AND pm_module = @pm_module AND pm_sub_module = @pm_sub_module AND @pm_key = pm_key 

IF @pm_data_type = 'T' OR @pm_data_type = 'X'
BEGIN
	INSERT INTO devfwk_parameter ( pm_environment, pm_module, pm_sub_module, pm_key, pm_data_type, pm_str_value ) 
	VALUES ( @pm_environment, @pm_module, @pm_sub_module, @pm_key, @pm_data_type, @pm_str_value )
END

IF @pm_data_type = 'D' 
BEGIN
	INSERT INTO devfwk_parameter ( pm_environment, pm_module, pm_sub_module, pm_key, pm_data_type, pm_dat_value ) 
	VALUES ( @pm_environment, @pm_module, @pm_sub_module, @pm_key, @pm_data_type, @pm_dat_value )
END

IF @pm_data_type = 'N' 
BEGIN
	INSERT INTO devfwk_parameter ( pm_environment, pm_module, pm_sub_module, pm_key, pm_data_type, pm_num_value ) 
	VALUES ( @pm_environment, @pm_module, @pm_sub_module, @pm_key, @pm_data_type, @pm_num_value )
END	

SET ANSI_NULLS ON

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER devfwk_action_config_ts on dbo.devfwk_action_config
FOR INSERT, UPDATE, DELETE 
AS

IF EXISTS (SELECT * FROM devfwk_config_timestamps WHERE entity = 'action' )
  UPDATE devfwk_config_timestamps
  SET last_updated = GETDATE()
  WHERE entity = 'action'
ELSE
  INSERT INTO devfwk_config_timestamps (entity, last_updated)
  VALUES (  'action',  GETDATE() )


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER devfwk_message_ts on devfwk_message
FOR INSERT, UPDATE, DELETE 
AS

IF EXISTS (SELECT * FROM devfwk_config_timestamps WHERE entity = 'message' )
  UPDATE devfwk_config_timestamps
  SET last_updated = GETDATE()
  WHERE entity = 'message'
ELSE
  INSERT INTO devfwk_config_timestamps (entity, last_updated)
  VALUES (  'message',  GETDATE() )




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE TRIGGER devfwk_parameter_ts on devfwk_parameter
FOR INSERT, UPDATE, DELETE 
AS

IF EXISTS (SELECT * FROM devfwk_config_timestamps WHERE entity = 'parameter' )
  UPDATE devfwk_config_timestamps
  SET last_updated = GETDATE()
  WHERE entity = 'parameter'
ELSE
  INSERT INTO devfwk_config_timestamps (entity, last_updated)
  VALUES (  'parameter',  GETDATE() )



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

