alter proc SP_Customer_GetAll
as
Select * from Customer where State=1

alter proc SP_Customer_CreateNew
@_Code nvarchar(10),
@_FullName nvarchar(200),
@_ErrCode int output
as
if exists (select * from Customer where Code=@_Code)
	set @_ErrCode = 1
else
	begin
		Insert into Customer(Code,FullName)
		values(@_Code,@_FullName)
		set @_ErrCode=0
	end

alter proc SP_Customer_Update_ById
@_Id int,
@_Code nvarchar(10),
@_FullName nvarchar(200),
@_ErrCode int output
as
if not exists (select * from Customer where Id=@_Id)
	set @_ErrCode = 1
else
	if not exists (select * from Customer where Code=@_Code)
		set @_ErrCode = 2
	else
		begin
			Update Customer
			set Customer.Code=@_Code,Customer.FullName= @_FullName
			set @_ErrCode = 0
		end

alter proc SP_Customer_Del_ById
@_Id int,
@_ErrCode int output
as
if not exists (select * from Customer where Id=@_Id)
	set @_ErrCode = 1
else
	begin
		delete Customer
		where Id=@_Id
		set @_ErrCode = 0
	end

alter proc SP_Customer_CheckCode_ByCode
@_Code nvarchar(10),
@_Response int output
as
if exists (select * from Customer where Code=@_Code)
	set @_Response = 1
else
	set @_Response = 0