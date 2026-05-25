create proc SP_Material_GetAll
as
Select * from Material

create proc SP_Material_CreateNew
@_MaterialCode varchar(20),
@_MaterialColor nvarchar(200),
@_CustomerColor nvarchar(200),
@_StandardColor nvarchar(200),
@_Description nvarchar(500),
@_ErrCode int output
as
Insert into Material(MaterialCode,MaterialColor,CustomerColor,StandardColor,Description)
values(@_MaterialCode,@_MaterialColor,@_CustomerColor,@_StandardColor,@_Description)
set @_ErrCode = 0


create proc SP_Material_Update_ById
@_Id int,
@_MaterialCode varchar(20),
@_MaterialColor nvarchar(200),
@_CustomerColor nvarchar(200),
@_StandardColor nvarchar(200),
@_Description nvarchar(500),
@_ErrCode int output
as
if not exists (select * from Material where Id=@_Id)
	set @_ErrCode = 1
else
	begin
		Update Material
		set Material.MaterialCode=@_MaterialCode,MaterialColor=@_MaterialColor, CustomerColor=@_CustomerColor,StandardColor=@_StandardColor,Description=@_Description
		set @_ErrCode = 0
	end

create proc SP_Material_Del_ById 
@_Id int,
@_ErrCode int output
as
if not exists (select * from Material where Id=@_Id)
	set @_ErrCode = 1
else
	begin
		delete Material
		where Id=@_Id
		set @_ErrCode = 0
	end

alter proc SP_Material_CheckCode_ByCode
@_MaterialCode varchar(10),
@_Response int output
as
if exists (select * from Material where MaterialCode=@_MaterialCode)
	set @_Response = 1
else
	set @_Response = 0
